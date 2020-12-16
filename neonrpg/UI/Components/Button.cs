using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.UI.Components {

    class Button : Component.InputComponent {

        public string Text { get; set; }

        public ConsoleColor Foreground { get; set; }
        public ConsoleColor Background { get; set; }

        private Action<Button> Action { get; }

        public Button(int id, string text, int x, int y, Action<Button> action, ConsoleColor foreground = ConsoleColor.White,
                      ConsoleColor background = ConsoleColor.Black) : base(id, x, y) {
            Text = text;
            Action = action;
            Foreground = foreground;
            Background = background;
        }

        public override void Render() {
            if(Selected) {
                NeonRPG.Console.DrawString("[" + Text + "]", X, Y, Background, Foreground);
            } else {
                NeonRPG.Console.DrawString("[" + Text + "]", X, Y, Foreground, Background);
            }
        }

        public override void Input(ConsoleKeyInfo keyInfo) {
            if(keyInfo.Key == ConsoleKey.Enter) {
                Action(this);
            }
        }
    }
}
