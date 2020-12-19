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

        public static void Run() {
            Running = true;

            SysCon.Clear();
            Console = NeonConsole.GetConsole("win", 121, 31);
            OpenScreen(new TitleScreen());

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
            }
        }

        private static void Draw() {
            Console.Clear(Color.BLACK);
            CurrentScreen.Render();

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
            Console.Close();
        }
    }
}