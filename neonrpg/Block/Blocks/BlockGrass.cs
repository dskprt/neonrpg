using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Block.Blocks {

    class BlockGrass : BaseBlock {

        public BlockGrass(ushort x, ushort y) : base(0, "Grass", x, y, ' ', new Color("0", "140", "10"), new Color("0", "140", "10")) { }
    }
}
