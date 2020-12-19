using neonrpg.Entity.Entities;
using neonrpg.Level;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace neonrpg.Block.Blocks {

    class BlockDoor : BaseBlock {

        private static BlockDoor door;

        public BlockDoor(ushort x, ushort y, byte data) : base(3, "Door", x, y, '☐', new Color("60", "30", "0"), new Color("120", "60", "0"), true, false, data) { }

        public override void Interact(ref BaseLevel level) {
            Facing facing = level.Player.Facing;
            BaseLevel lvl;

            if (Data == 0) {
                lvl = Levels.LoadLevelFromResources(level.Name.Substring(0, level.Name.LastIndexOf('_')));
            } else {
                lvl = Levels.LoadLevelFromResources(level.Name + '_' + Data);
            }

            if(door != null) {
                ushort x = door.X;
                ushort y = door.Y;

                switch (facing) {
                    case Facing.NORTH:
                        y--;
                        break;
                    case Facing.EAST:
                        x++;
                        break;
                    case Facing.SOUTH:
                        y++;
                        break;
                    case Facing.WEST:
                        x--;
                        break;
                }

                lvl.Player = new EntityPlayer(x, y);
                lvl.Entities[0] = lvl.Player;
            }

            door = this;
            level = lvl;
        }
    }
}
