using neonrpg.UI;
using neonrpg.UI.Screens;
using neonrpg.Utilities;
using System;
using neonrpg.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace neonrpg {

    class NeonRPG {

        public static NeonRPG Instance { get; set; }

        public bool Running { get; set; }

        private DateTime LastTime { get; set; }
        private int FramesRendered { get; set; }
        private int FPS { get; set; }
        private NeonConsole NConsole { get; set; }

        int x = 11;
        int y = 11;

        public NeonRPG() {
            Instance = this;
        }

        Array values;
        Random random;

        public void Run() {
            values = Enum.GetValues(typeof(ConsoleColor));
            random = new Random();
            
            Running = true;

            NConsole = NeonConsole.GetConsole("win", 90, 40);

            while (Running) {
                Update();
                Draw();
                NConsole.Draw();

                //Thread.Sleep(1000 / 60);
            }
        }

        private void Update() {
            if (Console.KeyAvailable) {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key) {
                    case ConsoleKey.S:
                        y++;
                        wy--;
                        break;
                    case ConsoleKey.W:
                        y--;
                        wy++;
                        break;
                    case ConsoleKey.A:
                        x--;
                        wx++;
                        break;
                    case ConsoleKey.D:
                        x++;
                        wx--;
                        break;
                }
            }
        }

        int wx = 8;
        int wy = 8;
        int ww = 15;
        int wh = 10;

        private void Draw() {
            NConsole.Clear(ConsoleColor.Black);

            NConsole.Fill(' ', wx, wy, ww, wh, ConsoleColor.Green);

            NConsole.DrawChar('H', x, y, ConsoleColor.White, ConsoleColor.Black);
            NConsole.DrawChar('i', x + 1, y, ConsoleColor.White, ConsoleColor.Black);
            NConsole.DrawChar('!', x + 2, y, ConsoleColor.White, ConsoleColor.Black);

            FramesRendered++;

            if ((DateTime.Now - LastTime).TotalSeconds >= 1) {
                FPS = FramesRendered;
                FramesRendered = 0;
                LastTime = DateTime.Now;
            }

            Console.Title = FPS.ToString();
        }
    }
}
