using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Item {

    abstract class WeaponItem : BaseItem {

        public double Damage { get; set; }

        public WeaponItem(byte id, string name, double damage, char character, Color foreground, byte data = 0) : base(id, ItemType.WEAPON, name, character, foreground, data) {
            this.Damage = damage;
        }
    }
}
