using neonrpg.IO.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.IO {

    abstract class NeonConsole {

        public int Width { get; set; }
        public int Height { get; set; }

        public NeonConsole(int width, int height) {
            Width = width;
            Height = height;
        }

        public static NeonConsole GetConsole(string platform, int width, int height) {
            switch(platform) {
                case "win":
                    return new Win32Console(width, height);
                default:
                    return null;
            }
        }

        //TODO more functions?
        public abstract void DrawChar(char c, int x, int y, ConsoleColor foreground, ConsoleColor background);
        public abstract void DrawString(string str, int x, int y, ConsoleColor foreground, ConsoleColor background);
        public abstract void Fill(char c, int x, int y, int w, int h, ConsoleColor color);
        public abstract void Draw();
        public abstract void Clear(ConsoleColor color);
    }
}
