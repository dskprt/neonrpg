using neonrpg.Block;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Level {

    class BaseLevel {

        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public List<BaseBlock> Blocks { get; set; }

        public BaseLevel(ushort width, ushort height, List<BaseBlock> blocks) {
            this.Width = width;
            this.Height = height;
            this.Blocks = blocks;
        }

        public void Render(int offsetX, int offsetY) {
            foreach(BaseBlock block in Blocks) {
                block.Render(offsetX, offsetY);
            }
        }
    }
}
