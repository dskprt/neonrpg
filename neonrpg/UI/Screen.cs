using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace neonrpg.UI {

    class Screen {

        public List<Component> components = new List<Component>();
        public List<Component.InputComponent> inputComponents = new List<Component.InputComponent>();

        private int Selection { get; set; }

        public virtual void Initialize() {
            if(inputComponents.Count != 0) {
                inputComponents.Sort((a, b) => a.Id.CompareTo(b.Id));

                Selection = 0;
                inputComponents[Selection].Selected = true;
            }
        }

        public virtual void Render() {
            components.ForEach(component => component.Render());
            inputComponents.ForEach(component => component.Render());
        }

        public virtual void Input(ConsoleKeyInfo keyInfo) {
            if (inputComponents.Count == 0) return;

            switch (keyInfo.Key) {
                case ConsoleKey.Tab:
                case ConsoleKey.DownArrow:
                    if (Selection + 1 > inputComponents.Count - 1) {
                        Selection = 0;
                    } else {
                        Selection++;
                    }

                    break;
                case ConsoleKey.UpArrow:
                    if (Selection - 1 < 0) {
                        Selection = inputComponents.Count - 1;
                    } else {
                        Selection--;
                    }

                    break;
            }

            inputComponents.ForEach(component => {
                if (inputComponents[Selection].Id == component.Id) {
                    component.Selected = true;
                    component.Input(keyInfo);
                } else {
                    component.Selected = false;
                }
            });
        }
    }
}
