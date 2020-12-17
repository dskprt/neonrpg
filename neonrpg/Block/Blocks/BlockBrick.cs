using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Block.Blocks {

    class BlockBrick : BaseBlock {

        public BlockBrick(uint x, uint y) : base(2, "Brick", x, y, '░', Color.WHITE, new Color("230", "70", "70"), canWalkThrough: false) { }
    }
}
