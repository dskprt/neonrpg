using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Block.Blocks {

    class BlockFlower : BaseBlock {

        public BlockFlower(ushort x, ushort y, byte data) : base(1, "Flower", x, y, '*', Color.YELLOW, new Color("0", "140", "10"), data: data) {
            switch(data) {
                case 0:
                    Foreground = Color.YELLOW;
                    break;
                case 1:
                    Foreground = Color.RED;
                    break;
            }
        }
    }
}
