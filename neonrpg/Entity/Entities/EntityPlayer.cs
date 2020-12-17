using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Entity.Entities {

    class EntityPlayer : BaseEntity {

        public EntityPlayer(ushort x, ushort y) : base(0, "Player", x, y, '@', new Color("200", "180", "0")) { }

        //public override void Render(int offsetX, int offsetY) {
        //    NeonRPG.Console.DrawChar(Character, (int)(NeonRPG.Console.Width / 2 - 0.5), (int)(NeonRPG.Console.Height / 2 - 0.5), Foreground, Color.TRANSPARENT);
        //}
    }
}
