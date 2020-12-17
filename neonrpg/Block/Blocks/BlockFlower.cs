using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Block.Blocks {

    class BlockFlower : BaseBlock {

        public BlockFlower(uint x, uint y, byte data) : base(1, "Flower", x, y, '*', Color.YELLOW, Color.GREEN, data: data) {
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
