using neonrpg.Entity.Entities;
using neonrpg.Level;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.UI.Screens {

    class GameScreen : Screen {

        private BaseLevel CurrentLevel { get; set; }
        private int X { get; set; }
        private int Y { get; set; }

        private EntityPlayer Player { get; set; }

        public override void Initialize() {
            CurrentLevel = Levels.LoadLevelByName("world");
            X = (NeonRPG.Console.Width / 2 - CurrentLevel.Width / 2);
            Y = (NeonRPG.Console.Height / 2 - CurrentLevel.Height / 2);

            Player = new EntityPlayer((ushort)Math.Floor(CurrentLevel.Width / 2d), (ushort)Math.Floor(CurrentLevel.Height / 2d));

            base.Initialize();
        }

        public override void Render() {
            CurrentLevel.Render(X, Y);
            Player.Render(-Player.X + (int)Math.Round(NeonRPG.Console.Width / 2d), -Player.Y + (int)Math.Floor(NeonRPG.Console.Height / 2d));

            NeonRPG.Console.DrawString("X: " + Player.X + ", Y: " + Player.Y, 0, 1, Color.GREEN, Color.BLACK);

            base.Render();
        }

        public override void Input(ConsoleKeyInfo keyInfo) {
            int index;

            switch(keyInfo.Key) {
                case ConsoleKey.W:
                    if (Player.Y - 1 < 0) return;

                    index = ((Player.Y - 1) * CurrentLevel.Width) + Player.X;
                    if (!CurrentLevel.Blocks[index].WalkableThrough) return;

                    Y++;
                    Player.Y--;
                    break;
                case ConsoleKey.S:
                    if (Player.Y + 1 > CurrentLevel.Height - 1) return;

                    index = ((Player.Y + 1) * CurrentLevel.Width) + Player.X;
                    if (!CurrentLevel.Blocks[index].WalkableThrough) return;

                    Y--;
                    Player.Y++;
                    break;
                case ConsoleKey.A:
                    if (Player.X - 1 < 0) return;

                    index = (Player.Y * CurrentLevel.Width) + (Player.X - 1);
                    if (!CurrentLevel.Blocks[index].WalkableThrough) return;

                    X++;
                    Player.X--;
                    break;
                case ConsoleKey.D:
                    if (Player.X + 1 > CurrentLevel.Width - 1) return;

                    index = (Player.Y * CurrentLevel.Width) + (Player.X + 1);
                    if (!CurrentLevel.Blocks[index].WalkableThrough) return;

                    X--;
                    Player.X++;
                    break;
            }

            base.Input(keyInfo);
        }
    }
}
