using neonrpg.UI.Components;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace neonrpg.UI.Screens {

    class TitleScreen : Screen {

        private const string title = "neonrpg";

        private const string btn0 = "Play";
        private const string btn1 = "Options";
        private const string btn2 = "Quit";

        public override void Initialize() {
            int titleX = (NeonRPG.Console.Width / 2) - (title.Length / 2);
            this.components.Add(new Label(title, titleX, 1, Color.WHITE, Color.BLACK));

            int btn0X = (NeonRPG.Console.Width / 2) - (btn0.Length / 2);
            int btn1X = (NeonRPG.Console.Width / 2) - (btn1.Length / 2);
            int btn2X = (NeonRPG.Console.Width / 2) - (btn2.Length / 2);

            int y = NeonRPG.Console.Height / 2;

            this.inputComponents.Add(new Button(0, btn0, btn0X, y, btn => {
                Debug.WriteLine(btn0);
                NeonRPG.OpenScreen(new GameScreen());
            }, Color.WHITE, Color.BLACK));

            this.inputComponents.Add(new Button(1, btn1, btn1X, y + 1, btn => {
                Debug.WriteLine(btn1);
            }, Color.WHITE, Color.BLACK));

            this.inputComponents.Add(new Button(2, btn2, btn2X, y + 2, btn => {
                Debug.WriteLine(btn2);
                NeonRPG.Shutdown();
            }, Color.WHITE, Color.BLACK));

            base.Initialize();
        }
    }
}