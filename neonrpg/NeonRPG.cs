using neonrpg.UI;
using neonrpg.UI.Screens;
using System;
using neonrpg.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SysCon = System.Console;
using neonrpg.Utilities;

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
            Console = NeonConsole.GetConsole("win", 121, 31);
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
            //if (f) {
            //    if (p < 100) {
            //        p += 0.0025f;
            //    } else {
            //        p = 99.975f;
            //        f = false;
            //    }
            //} else {
            //    if (p > 0) {
            //        p -= 0.0025f;
            //    } else {
            //        p = 0.0025f;
            //        f = true;
            //    }
            //}

            //if(p < 100) {
            //    p += 0.001f;
            //}

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

        //static int wx = 8;
        //static int wy = 8;
        //static int ww = 15;
        //static int wh = 10;

        //public static Color Rainbow(float progress) {
        //    float div = (Math.Abs(progress % 1) * 6);
        //    int ascending = (int)((div % 1) * 255);
        //    int descending = 255 - ascending;

        //    switch ((int)div) {
        //        case 0:
        //            return new Color("255", ascending.ToString(), "0");
        //        case 1:
        //            return new Color(descending.ToString(), "255", "0");
        //        case 2:
        //            return new Color("0", "255", ascending.ToString());
        //        case 3:
        //            return new Color("0", descending.ToString(), "255");
        //        case 4:
        //            return new Color(ascending.ToString(), "0", "255");
        //        default: // case 5:
        //            return new Color("255", "0", descending.ToString());
        //    }
        //}

        //static float p = 0;
        //static bool f = true;

        private static void Draw() {
            Console.Clear(Color.BLACK);
            CurrentScreen.Render();

            //Console.Fill(' ', wx, wy, ww, wh, Color.GREEN);
            //for (int y1 = 1; y1 < Console.Height - 1; y1++) {
            //    for (int x1 = 0; x1 < Console.Width; x1++) {
            //        Console.DrawChar(' ', x1, y1, Rainbow(p), Rainbow(p));
            //    }
            //}

            //Console.DrawChar('H', x, y, Color.WHITE, Color.BLACK);
            //Console.DrawChar('i', x + 1, y, Color.WHITE, Color.BLACK);
            //Console.DrawChar('!', x + 2, y, Color.WHITE, Color.BLACK);

            FramesRendered++;

            Delta = (DateTime.Now - LastTime).TotalSeconds;

            if ((DateTime.Now - LastTime).TotalSeconds >= 1) {
                FPS = FramesRendered;
                FramesRendered = 0;
                LastTime = DateTime.Now;
            }

            Console.DrawString("FPS: " + FPS.ToString(), 0, 0, Color.GREEN, Color.BLACK);
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