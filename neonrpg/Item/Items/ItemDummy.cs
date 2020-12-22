using neonrpg.Entity.Entities;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace neonrpg.Item.Items {

    class ItemDummy : BaseItem {

        public ItemDummy(byte data = 0) : base(0, ItemType.OTHER, "Dummy", '&', Color.BLUE, data) { }

        public override void Use(EntityPlayer player) {
            Debug.WriteLine("I was used!");
        }
    }
}
