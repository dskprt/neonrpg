using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.UI.Components {

    class Label : Component {

        public string Text { get; set; }
        public Color Foreground { get; set; }
        public Color Background { get; set; }

        public Label(string text, int x, int y, Color foreground,
                     Color background) : base(x, y) {
            Text = text;
            Foreground = foreground;
            Background = background;
        }

        public override void Render() {
            NeonRPG.Console.DrawString(Text, X, Y, Foreground, Background);
        }
    }
}