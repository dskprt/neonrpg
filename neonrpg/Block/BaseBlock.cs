using neonrpg.Entity.Entities;
using neonrpg.Level;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Block {

    class BaseBlock {

        public byte Id { get; set; }
        public string Name { get; set; }
        public ushort X { get; set; }
        public ushort Y { get; set; }

        public char Character { get; }
        public Color Foreground { get; set; }
        public Color Background { get; set; }

        public bool Interactable { get; }
        public bool WalkableThrough { get; }

        public byte Data { get; set; }

        public BaseBlock(byte id, string name, ushort x, ushort y, char character, Color foreground, Color background, bool canInteract = false, bool canWalkThrough = true, byte data = 0) {
            Id = id;
            Name = name;
            X = x;
            Y = y;
            Character = character;
            Foreground = foreground;
            Background = background;
            Interactable = canInteract;
            WalkableThrough = canWalkThrough;
            Data = data;
        }

        public void Render(int offsetX, int offsetY) {
            NeonRPG.Console.DrawChar(Character, offsetX + X, offsetY + Y, Foreground, Background);
        }

        public virtual void Interact(ref BaseLevel level) { }
    }
}
