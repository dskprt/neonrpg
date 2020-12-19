using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Block.Blocks {

    class BlockAir : BaseBlock {

        public BlockAir(ushort x, ushort y) : base(255, "Air", x, y, ' ', new Color("0", "0", "0"), new Color("0", "0", "0")) { }
    }
}
