using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace neonrpg.IO.Impl {

    class Win32Console : NeonConsole {

        private CHAR_INFO EMPTY_CHAR = new CHAR_INFO() {
            Attributes = 0,
            AsciiChar = ' ',
            UnicodeChar = ' '
        };

        private CHAR_INFO[] Buffer { get; set; }
        private IntPtr Handle { get; set; }

        private COORD bufferSize;
        private COORD bufferCoord;
        private SMALL_RECT region;

        public Win32Console(int width, int height) : base(width, height) {
            Buffer = Array<CHAR_INFO>.Empty(width * height);
            Handle = GetStdHandle(STD_OUTPUT_HANDLE);

            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            uint mode;

            if (!GetConsoleMode(Handle, out mode)) throw new Win32Exception();

            mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;

            if (!SetConsoleMode(Handle, mode)) throw new Win32Exception();

            ReadConsoleOutput(Handle, Buffer, bufferSize, bufferCoord, ref region);

            bufferSize = new COORD((short)Width, (short)Height);
            bufferCoord = new COORD(0, 0);
            region = new SMALL_RECT { Left = 0, Top = 0, Right = (short)(width - 1), Bottom = (short)(height - 1) };
        }

        public override void Clear(ConsoleColor color) {
            CHAR_INFO chr = EMPTY_CHAR;
            chr.Attributes = (byte)((byte)color + ((byte)color << 4));

            Array.Fill(Buffer, chr);
        }

        public override void Draw() {
            WriteConsoleOutput(Handle, Buffer, bufferSize, bufferCoord, ref region);
        }

        public override void DrawChar(char c, int x, int y, ConsoleColor foreground, ConsoleColor background) {
            int index = (y * Width) + x;

            if (!(index >= 0 && index < Buffer.Length)) return;

            Buffer[index].Attributes = (byte)((byte)foreground + ((byte)background << 4));
            Buffer[index].AsciiChar = c;
        }

        public override void DrawString(string str, int x, int y, ConsoleColor foreground, ConsoleColor background) {
            for(int i = 0; i < str.Length; i++) {
                DrawChar(str[i], x + i, y, foreground, background);
            }
        }

        public override void Fill(char c, int x, int y, int w, int h, ConsoleColor color) {
            for (int y1 = y; y1 < (h + y); y1++) {
                for (int x1 = x; x1 < (w + x); x1++) {
                    DrawChar(c, x1, y1, color, color);
                }
            }
        }

        private static class Array<T> {

            public static T[] Empty() {
                return Empty(0);
            }

            public static T[] Empty(int size) {
                return new T[size];
            }
        }

        const int STD_OUTPUT_HANDLE = -11;
        const int ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        [StructLayout(LayoutKind.Explicit)]
        struct CHAR_INFO {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public char AsciiChar;
            [FieldOffset(2)]
            public ushort Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct COORD {
            public short X;
            public short Y;

            public COORD(short X, short Y) {
                this.X = X;
                this.Y = Y;
            }
        };

        struct SMALL_RECT {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

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
        static extern bool ReadConsoleOutput(
            IntPtr hConsoleOutput,
            [Out] CHAR_INFO[] lpBuffer,
            COORD dwBufferSize,
            COORD dwBufferCoord,
            ref SMALL_RECT lpReadRegion
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
            IntPtr hConsoleOutput,
            CHAR_INFO[] lpBuffer,
            COORD dwBufferSize,
            COORD dwBufferCoord,
            ref SMALL_RECT lpWriteRegion
        );
    }
}
