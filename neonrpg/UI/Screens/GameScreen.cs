using neonrpg.Entity.Entities;
using neonrpg.Level;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.UI.Screens {

    class GameScreen : Screen {

        private BaseLevel Level { get; set; }

        private int x;
        private int y;

        public override void Initialize() {
            Level = Levels.LoadLevelFromResources("world");
            x = (NeonRPG.Console.Width / 2 - Level.Width / 2);
            y = (NeonRPG.Console.Height / 2 - Level.Height / 2);

            base.Initialize();
        }

        public override void Render() {
            Level.Render(x, y);

            NeonRPG.Console.DrawString("X: " + Level.Player.X + ", Y: " + Level.Player.Y, 0, 1, Color.GREEN, Color.BLACK);

            base.Render();
        }

        public override void Input(ConsoleKeyInfo keyInfo) {
            Level.Player.Input(keyInfo, Level, ref x, ref y);

            base.Input(keyInfo);
        }
    }
}
