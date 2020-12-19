using neonrpg.Entity.Entities;
using neonrpg.Level;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.UI.Screens {

    class GameScreen : Screen {

        private BaseLevel level;

        private int x;
        private int y;

        public override void Initialize() {
            level = Levels.LoadLevelFromResources("doors");
            x = (NeonRPG.Console.Width / 2 - level.Width / 2);
            y = (NeonRPG.Console.Height / 2 - level.Height / 2);

            base.Initialize();
        }

        public override void Render() {
            level.Render(x, y);

            NeonRPG.Console.DrawString("X: " + level.Player.X + ", Y: " + level.Player.Y, 0, 1, Color.GREEN, Color.BLACK);

            base.Render();
        }

        public override void Input(ConsoleKeyInfo keyInfo) {
            level.Player.Input(keyInfo, ref level, ref x, ref y);

            base.Input(keyInfo);
        }
    }
}
