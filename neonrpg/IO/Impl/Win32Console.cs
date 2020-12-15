using Microsoft.Win32.SafeHandles;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace neonrpg.IO.Impl {

    class Win32Console : NeonConsole {

        private const string CLEAR_SEQUENCE = "\u001b[2J";
        private Win32.CHAR_INFO EMPTY_CHAR = new Win32.CHAR_INFO() {
            Attributes = 0,
            Char = new Win32.CHAR_UNION() {
                AsciiChar = (byte)' ',
                UnicodeChar = ' '
            }
        };

        private Win32.CHAR_INFO[] Buffer { get; set; }
        private Win32.CHAR_INFO[] PrevBuffer { get; set; }
        private IntPtr Handle { get; set; }

        private Win32.COORD bufferSize;
        private Win32.COORD bufferCoord;
        private Win32.SMALL_RECT region;

        public Win32Console(int width, int height) : base(width, height) {
            Buffer = Array<Win32.CHAR_INFO>.Empty(width * height);
            PrevBuffer = Array<Win32.CHAR_INFO>.Empty(width * height);
            Handle = Win32.GetStdHandle(Win32.STD_OUTPUT_HANDLE);

            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            int mode = 0;

            if (!Win32.GetConsoleMode(Handle, ref mode)) throw new Win32Exception();

            mode |= Win32.ENABLE_VIRTUAL_TERMINAL_PROCESSING;

            if (!Win32.SetConsoleMode(Handle, mode)) throw new Win32Exception();

            Win32.ReadConsoleOutput(Handle, Buffer, bufferSize, bufferCoord, ref region);

            bufferSize = new Win32.COORD((short)Width, (short)Height);
            bufferCoord = new Win32.COORD(0, 0);
            region = new Win32.SMALL_RECT { Left = 0, Top = 0, Right = (short)(width - 1), Bottom = (short)(height - 1) };
        }

        public override void Clear(ConsoleColor color) {
            Win32.CHAR_INFO chr = EMPTY_CHAR;
            chr.Attributes = (byte)((byte)color + ((byte)color << 4));

            Array.Fill(Buffer, chr);
        }

        public override void Draw() {
            Win32.WriteConsoleOutput(Handle, Buffer, bufferSize, bufferCoord, ref region);
        }

        public override void DrawChar(char c, int x, int y, ConsoleColor foreground, ConsoleColor background) {
            int index = (y * Width) + x;

            if (!(index >= 0 && index < Buffer.Length)) return;

            Buffer[index].Attributes = (byte)((byte)foreground + ((byte)background << 4));
            Buffer[index].Char.AsciiChar = (byte)c;
        }

        //TODO implement this
        public override void DrawString(string str, int x, int y, ConsoleColor foreground, ConsoleColor background) {
            throw new NotImplementedException();
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
    }
}
