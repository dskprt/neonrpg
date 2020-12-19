using neonrpg.Block;
using neonrpg.Entity;
using neonrpg.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Level {

    class BaseLevel {

        public string Name { get; set; }

        public ushort Width { get; set; }
        public ushort Height { get; set; }

        public EntityPlayer Player { get; set; }

        public List<BaseBlock> Blocks { get; set; }
        public List<BaseEntity> Entities { get; set; }

        public BaseLevel(string name, ushort width, ushort height, List<BaseBlock> blocks, List<BaseEntity> entities, ushort[] spawnCoordinates) {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Blocks = blocks;
            this.Entities = entities;

            this.Player = new EntityPlayer(spawnCoordinates[0], spawnCoordinates[1]);
            this.Entities.Insert(0, this.Player);
        }

        public void Render(int offsetX, int offsetY) {
            this.Blocks.ForEach(block => block.Render(offsetX, offsetY));
            this.Entities.ForEach(entity => { if (entity != null) entity.Render(offsetX, offsetY); });
        }

        public void Replace(BaseLevel level) {
            this.Name = level.Name;
            this.Width = level.Width;
            this.Height = level.Height;
        }
    }
}
