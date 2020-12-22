using neonrpg.Item;
using neonrpg.Level;
using neonrpg.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace neonrpg.Entity.Entities {

    class EntityPlayer : BaseEntity {

        public Facing Facing { get; set; }
        public BaseItem Item { get; set; }

        public EntityPlayer(ushort x, ushort y) : base(0, "Player", x, y, '⮝', new Color("200", "180", "0")) {
            Facing = Facing.NORTH;
            Item = null;
        }

        public void Input(ConsoleKeyInfo keyInfo, ref BaseLevel level, ref int X, ref int Y) {
            int index;

            switch (keyInfo.Key) {
                case ConsoleKey.W:
                    this.Facing = Facing.NORTH;

                    if (this.Y - 1 < 0) return;

                    index = ((this.Y - 1) * level.Width) + this.X;
                    //                                                               + 1 because the 0th entity is the player - we don't want to count it
                    if (!level.Blocks[index].WalkableThrough || level.Entities[index + 1] != null) return;

                    Y++;
                    this.Y--;
                    break;
                case ConsoleKey.S:
                    this.Facing = Facing.SOUTH;

                    if (this.Y + 1 > level.Height - 1) return;

                    index = ((this.Y + 1) * level.Width) + this.X;
                    //                                                               + 1 because the 0th entity is the player - we don't want to count it
                    if (!level.Blocks[index].WalkableThrough || level.Entities[index + 1] != null) return;

                    Y--;
                    this.Y++;
                    break;
                case ConsoleKey.A:
                    this.Facing = Facing.WEST;

                    if (this.X - 1 < 0) return;

                    index = (this.Y * level.Width) + (this.X - 1);
                    //                                                               + 1 because the 0th entity is the player - we don't want to count it
                    if (!level.Blocks[index].WalkableThrough || level.Entities[index + 1] != null) return;

                    X++;
                    this.X--;
                    break;
                case ConsoleKey.D:
                    this.Facing = Facing.EAST;

                    if (this.X + 1 > level.Width - 1) return;

                    index = (this.Y * level.Width) + (this.X + 1);
                    //                                                               + 1 because the 0th entity is the player - we don't want to count it
                    if (!level.Blocks[index].WalkableThrough || level.Entities[index + 1] != null) return;

                    X--;
                    this.X++;
                    break;
                case ConsoleKey.E:
                    switch (this.Facing) {
                        case Facing.NORTH:
                            index = ((this.Y - 1) * level.Width) + this.X;

                            if (!(index >= 0 && index < level.Entities.Count)) return;

                            //                       + 1 because the 0th entity is the player - we don't want to count it
                            if (level.Entities[index + 1]?.Interactable == true) {
                                level.Entities[index + 1]?.Interact(ref level);
                                break;
                            }

                            if (level.Blocks[index]?.Interactable == true) {
                                level.Blocks[index]?.Interact(ref level);
                            }
                            break;
                        case Facing.EAST:
                            index = (this.Y * level.Width) + (this.X + 1);

                            if (!(index >= 0 && index < level.Entities.Count)) return;

                            //                       + 1 because the 0th entity is the player - we don't want to count it
                            if (level.Entities[index + 1]?.Interactable == true) {
                                level.Entities[index + 1]?.Interact(ref level);
                                break;
                            }

                            if (level.Blocks[index]?.Interactable == true) {
                                level.Blocks[index]?.Interact(ref level);
                            }
                            break;
                        case Facing.SOUTH:
                            index = ((this.Y + 1) * level.Width) + this.X;

                            if (!(index >= 0 && index < level.Entities.Count)) return;

                            //                       + 1 because the 0th entity is the player - we don't want to count it
                            if (level.Entities[index + 1]?.Interactable == true) {
                                level.Entities[index + 1]?.Interact(ref level);
                                break;
                            }

                            if (level.Blocks[index]?.Interactable == true) {
                                level.Blocks[index]?.Interact(ref level);
                            }
                            break;
                        case Facing.WEST:
                            index = (this.Y * level.Width) + (this.X - 1);

                            if (!(index >= 0 && index < level.Entities.Count)) return;

                            //                       + 1 because the 0th entity is the player - we don't want to count it
                            if (level.Entities[index + 1]?.Interactable == true) {
                                level.Entities[index + 1]?.Interact(ref level);
                                break;
                            }

                            if (level.Blocks[index]?.Interactable == true) {
                                level.Blocks[index]?.Interact(ref level);
                            }
                            break;
                    }

                    break;
                case ConsoleKey.R:
                    Item?.Use(this);
                    break;
                case ConsoleKey.D1:
                    Item = (Item == null ? new Item.Items.ItemDummy() : null);

                    break;
            }
        }

        public override void Render(int offsetX, int offsetY) {
            switch(this.Facing) {
                case Facing.NORTH:
                    this.Character = '⮝';
                    break;
                case Facing.EAST:
                    this.Character = '⮞';
                    break;
                case Facing.SOUTH:
                    this.Character = '⮟';
                    break;
                case Facing.WEST:
                    this.Character = '⮜';
                    break;
            }

            base.Render(offsetX, offsetY);
        }
    }
}
