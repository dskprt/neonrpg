using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace neonrpg.IO.Impl {

    class Win32Console : NeonConsole {

        private uint written;

        private string[] Buffer { get; set; }
        private IntPtr Handle { get; set; }

        public Win32Console(int width, int height) : base(width, height) {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            Buffer = new string[120 * 30];
            Handle = GetStdHandle(STD_OUTPUT_HANDLE);

            uint mode;
            if (!GetConsoleMode(Handle, out mode)) throw new Win32Exception();

            mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            if (!SetConsoleMode(Handle, mode)) throw new Win32Exception();

            Array.Fill(Buffer, "");

            WriteConsole(Handle, "\u001b[?25l", (uint)"\u001b[?25l".Length, out written, IntPtr.Zero);
        }

        public override void Clear(Color color) {
            string str = color.AsAnsiBackground() + " ";

            Array.Fill(Buffer, str);
        }

        public override void Draw() {
            string buff = "\u001b[0;0H" + string.Join("", Buffer);

            WriteConsole(Handle, buff, (uint)buff.Length, out written, IntPtr.Zero);
        }

        public override void DrawChar(char c, int x, int y, Color foreground, Color background) {
            int index = (y * Width) + x;

            if (!(index >= 0 && index < Buffer.Length)) return;

            Buffer[index] = background.AsAnsiBackground() + foreground.AsAnsiForeground() + c;
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

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsole(
            IntPtr hConsoleOutput,
            string lpBuffer,
            uint nNumberOfCharsToWrite,
            out uint lpNumberOfCharsWritten,
            IntPtr lpReserved
        );
    }
}