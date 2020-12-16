using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.UI.Components {

    class Label : Component {

        public string Text { get; set; }
        public ConsoleColor Foreground { get; set; }
        public ConsoleColor Background { get; set; }

        public Label(string text, int x, int y, ConsoleColor foreground = ConsoleColor.White,
                     ConsoleColor background = ConsoleColor.Black) : base(x, y) {
            Text = text;
            Foreground = foreground;
            Background = background;
        }

        public override void Render() {
            NeonRPG.Console.DrawString(Text, X, Y, Foreground, Background);
        }
    }
}
