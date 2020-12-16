using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.UI {

    abstract class Component {

        public int X { get; set; }
        public int Y { get; set; }

        public Component(int x, int y) {
            X = x;
            Y = y;
        }

        public abstract void Render();

        public abstract class InputComponent {

            public int Id { get; }
            public int X { get; set; }
            public int Y { get; set; }
            public bool Selected { get; set; }

            public InputComponent(int id, int x, int y) {
                Id = id;
                X = x;
                Y = y;
                Selected = false;
            }

            public abstract void Input(ConsoleKeyInfo keyInfo);
            public abstract void Render();
        }
    }
}
