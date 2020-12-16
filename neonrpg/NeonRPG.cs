using neonrpg.UI;
using neonrpg.UI.Screens;
using System;
using neonrpg.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SysCon = System.Console;

namespace neonrpg {

    class NeonRPG {

        public static Screen CurrentScreen { get; set; }
        public static NeonConsole Console { get; set; }
        private static bool Running { get; set; }

        private static double Delta { get; set; }
        private static DateTime LastTime { get; set; }
        private static int FramesRendered { get; set; }
        private static int FPS { get; set; }

        static int x = 11;
        static int y = 11;

        //Array values;
        //Random random;

        public static void Run() {
            Running = true;

            SysCon.Clear();
            Console = NeonConsole.GetConsole("win", 120, 30);
            OpenScreen(new TitleScreen());

            //values = Enum.GetValues(typeof(ConsoleColor));
            //random = new Random();

            while (Running) {
                Update();
                Draw();
                Console.Draw();

                //Thread.Sleep(1000 / 60);
            }
        }

        private static void Update() {
            if (SysCon.KeyAvailable) {
                ConsoleKeyInfo key = SysCon.ReadKey(true);
                CurrentScreen.Input(key);

                //switch (key.Key) {
                //    case ConsoleKey.S:
                //        y++;
                //        wy--;
                //        break;
                //    case ConsoleKey.W:
                //        y--;
                //        wy++;
                //        break;
                //    case ConsoleKey.A:
                //        x--;
                //        wx++;
                //        break;
                //    case ConsoleKey.D:
                //        x++;
                //        wx--;
                //        break;
                //}
            }
        }

        static int wx = 8;
        static int wy = 8;
        static int ww = 15;
        static int wh = 10;

        private static void Draw() {
            Console.Clear(ConsoleColor.Black);
            CurrentScreen.Render();

            //Console.Fill(' ', wx, wy, ww, wh, ConsoleColor.Green);
            //for (int y1 = 0; y1 < console.Height; y1++) {
            //    for (int x1 = 0; x1 < console.Width; x1++) {
            //        console.DrawChar('.', x1, y1, (ConsoleColor)values.GetValue(random.Next(values.Length)), (ConsoleColor)values.GetValue(random.Next(values.Length)));
            //    }
            //}

            //Console.DrawChar('H', x, y, ConsoleColor.White, ConsoleColor.Black);
            //Console.DrawChar('i', x + 1, y, ConsoleColor.White, ConsoleColor.Black);
            //Console.DrawChar('!', x + 2, y, ConsoleColor.White, ConsoleColor.Black);

            FramesRendered++;

            Delta = (DateTime.Now - LastTime).TotalSeconds;

            if ((DateTime.Now - LastTime).TotalSeconds >= 1) {
                FPS = FramesRendered;
                FramesRendered = 0;
                LastTime = DateTime.Now;
            }

            Console.DrawString("FPS: " + FPS.ToString(), 0, 0, ConsoleColor.Green, ConsoleColor.Black);
        }

        public static void OpenScreen(Screen screen) {
            screen.Initialize();
            CurrentScreen = screen;
        }

        public static void Shutdown() {
            Running = false;
            SysCon.Clear();
        }
    }
}
