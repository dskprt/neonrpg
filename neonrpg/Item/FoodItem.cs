using neonrpg.Entity.Entities;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Item {

    abstract class FoodItem : BaseItem {

        public double Healing { get; set; }

        public FoodItem(byte id, string name, double healing, char character, Color foreground, byte data = 0) : base(id, ItemType.FOOD, name, character, foreground, data) {
            this.Healing = healing;
        }
    }
}
