using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Block.Blocks {

    class BlockGrass : BaseBlock {

        public BlockGrass(uint x, uint y) : base(0, "Grass", x, y, ' ', Color.GREEN, Color.GREEN) { }
    }
}
