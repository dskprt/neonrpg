using neonrpg.Entity.Entities;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Item {

    abstract class BaseItem {

        public byte Id { get; set; }
        public ItemType Type { get; set; }
        public string Name { get; set; }

        public char Character { get; }
        public Color Foreground { get; set; }

        public byte Data { get; set; }

        public BaseItem(byte id, ItemType type, string name, char character, Color foreground, byte data = 0) {
            Id = id;
            Type = type;
            Name = name;
            Character = character;
            Foreground = foreground;
            Data = data;
        }

        public abstract void Use(EntityPlayer player);
    }
}
