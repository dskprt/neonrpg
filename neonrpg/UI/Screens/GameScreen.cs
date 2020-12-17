using neonrpg.Level;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.UI.Screens {

    class GameScreen : Screen {

        private BaseLevel CurrentLevel { get; set; }
        private int X { get; set; }
        private int Y { get; set; }

        public override void Initialize() {
            CurrentLevel = Levels.LoadLevelFromFile("C:\\Users\\usr\\Documents\\testt.nano");
            X = (int)(NeonRPG.Console.Width / 2 - CurrentLevel.Width / 2);
            Y = (int)(NeonRPG.Console.Height / 2 - CurrentLevel.Height / 2);

            base.Initialize();
        }

        public override void Render() {
            CurrentLevel.Render(X, Y);

            base.Render();
        }

        public override void Input(ConsoleKeyInfo keyInfo) {
            switch(keyInfo.Key) {
                case ConsoleKey.W:
                    Y--;
                    break;
                case ConsoleKey.S:
                    Y++;
                    break;
                case ConsoleKey.A:
                    X--;
                    break;
                case ConsoleKey.D:
                    X++;
                    break;
            }

            base.Input(keyInfo);
        }
    }
}
