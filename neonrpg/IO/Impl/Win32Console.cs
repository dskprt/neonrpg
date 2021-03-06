﻿using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace neonrpg.IO.Impl {

    class Win32Console : NeonConsole {

        private static readonly StringBuilder sb = new StringBuilder();
        private static readonly COORD zero = new COORD() {
            X = 0,
            Y = 0
        };

        private static uint written;

        private string[] Buffer { get; set; }
        private IntPtr Handle { get; set; }

        public Win32Console(int width, int height) : base(width, height) {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            Buffer = new string[121 * 31];
            Handle = GetStdHandle(STD_OUTPUT_HANDLE);

            uint mode;
            if (!GetConsoleMode(Handle, out mode)) throw new Win32Exception();

            mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            if (!SetConsoleMode(Handle, mode)) throw new Win32Exception();

            Array.Fill(Buffer, "");

            Console.Write("\u001b[?25l");
        }

        public override void Close() {
            Console.ResetColor();
            Console.Write("\u001b[2J");
            Console.Write("\u001b[?25h");
        }

        public override void Clear(Color color) {
            string str = color.AsAnsiBackground() + color.AsAnsiForeground() + " ";

            Array.Fill(Buffer, str);
        }

        public override void Draw() {
            string buff = string.Concat(Buffer);

            SetConsoleCursorPosition(Handle, zero);
            WriteConsoleW(Handle, buff, (uint)buff.Length, out written, IntPtr.Zero);
        }

        public override void DrawChar(char c, int x, int y, Color foreground, Color background) {
            if (x < 0 || y < 0) return;

            int index = (y * Width) + x;

            if (!(index >= 0 && index < Buffer.Length)) return;

            sb.Clear();

            if (background.Transparent) {
                string str = Buffer[index];
                // get the 2nd occurence of ESC
                int i = str.IndexOf('\u001b', 2); // we can hard code the start index since the structure never changes

                // retrieve only the background (which is first)
                str = str.Substring(0, i);
                sb.Append(str);
            } else {
                sb.Append(background.AsAnsiBackground());
            }

            if (foreground.Transparent) {
                string str = Buffer[index];
                // get the 2nd occurence of ESC
                int i = str.IndexOf('\u001b', 2); // we can hard code the start index since the structure never changes

                // retrieve only the foreground (which is second)
                str = str.Substring(i, str.Length - 2); // -2 since the last element is the character
                sb.Append(str);
            } else {
                sb.Append(foreground.AsAnsiForeground());
            }

            sb.Append(c);

            Buffer[index] = sb.ToString();
        }

        public override void DrawString(string str, int x, int y, Color foreground, Color background) {
            for (int i = 0; i < str.Length; i++) {
                DrawChar(str[i], x + i, y, foreground, background);
            }
        }

        public override void Fill(char c, int x, int y, int w, int h, Color color) {
            for (int y1 = y; y1 < (h + y); y1++) {
                for (int x1 = x; x1 < (w + x); x1++) {
                    DrawChar(c, x1, y1, color, color);
                }
            }
        }

        const int STD_OUTPUT_HANDLE = -11;
        const int ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(
            int nStdHandle
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleMode(
            IntPtr hConsoleHandle,
            out uint lpMode
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleMode(
            IntPtr hConsoleHandle,
            uint dwMode
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool WriteConsoleW(
            IntPtr hConsoleOutput,
            string lpBuffer,
            uint nNumberOfCharsToWrite,
            out uint lpNumberOfCharsWritten,
            IntPtr lpReserved
        );

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD {
            public short X;
            public short Y;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleCursorPosition(
            IntPtr hConsoleOutput,
            COORD dwCursorPosition
        );
    }
}