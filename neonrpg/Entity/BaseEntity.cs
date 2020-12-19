using neonrpg.Entity.Entities;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Entity {

    class BaseEntity {

        public byte Id { get; set; }
        public string Name { get; set; }
        public ushort X { get; set; }
        public ushort Y { get; set; }

        public char Character { get; set; }
        public Color Foreground { get; set; }

        public bool Interactable { get; }

        public byte Data { get; set; }

        public BaseEntity(byte id, string name, ushort x, ushort y, char character, Color foreground, bool canInteract = false, byte data = 0) {
            Id = id;
            Name = name;
            X = x;
            Y = y;
            Character = character;
            Foreground = foreground;
            Interactable = canInteract;
            Data = data;
        }

        public virtual void Render(int offsetX, int offsetY) {
            NeonRPG.Console.DrawChar(Character, offsetX + X, offsetY + Y, Foreground, Color.TRANSPARENT);
        }

        public virtual void Interact(EntityPlayer player) { }
    }
}
