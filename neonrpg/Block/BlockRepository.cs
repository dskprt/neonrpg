using neonrpg.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace neonrpg.Block {

    class BlockRepository {

        public static readonly Dictionary<byte, Type> REGISTRY = new Dictionary<byte, Type>() {
            { 0, typeof(BlockGrass) }, { 1, typeof(BlockFlower) }, { 2, typeof(BlockBrick) }
        };

        public static BaseBlock CreateFromId(byte id, ushort x, ushort y, byte data = 0) {
            ConstructorInfo constructor = REGISTRY[id].GetConstructor(new Type[] { typeof(ushort), typeof(ushort), typeof(byte) });

            if(constructor == null) {
                constructor = REGISTRY[id].GetConstructor(new Type[] { typeof(ushort), typeof(ushort) });
                return (BaseBlock) constructor.Invoke(new object[] { x, y });
            }

            return (BaseBlock) constructor.Invoke(new object[] { x, y, data });
        }
    }
}
