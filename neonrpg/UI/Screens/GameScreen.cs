using neonrpg.Entity.Entities;
using neonrpg.Level;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.UI.Screens {

    class GameScreen : Screen {

        public BaseLevel level;

        private int x;
        private int y;

        public override void Initialize() {
            this.level = Levels.LoadLevelFromResources("doors");
            this.level.Initialize();

            x = (NeonRPG.Console.Width / 2 - this.level.Width / 2);
            y = (NeonRPG.Console.Height / 2 - this.level.Height / 2);

            base.Initialize();
        }

        public override void Render() {
            this.level.Render(x, y);

            NeonRPG.Console.DrawString("X: " + this.level.Player.X + ", Y: " + this.level.Player.Y, 0, 1, Color.GREEN, Color.BLACK);

            if(this.level.Player.Item != null) {
                NeonRPG.Console.DrawChar('!', 0, 2, Color.GREEN, Color.BLACK);
            }

            base.Render();
        }

        public override void Input(ConsoleKeyInfo keyInfo) {
            this.level.Player.Input(keyInfo, ref this.level, ref x, ref y);

            base.Input(keyInfo);
        }
    }
}
