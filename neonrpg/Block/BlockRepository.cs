using neonrpg.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace neonrpg.Block {

    class BlockRepository {

        public static readonly Dictionary<uint, Type> REGISTRY = new Dictionary<uint, Type>() {
            { 0, typeof(BlockGrass) }, { 1, typeof(BlockFlower) }, { 2, typeof(BlockBrick) }
        };

        public static BaseBlock CreateFromId(uint id, uint x, uint y, byte data = 0) {
            ConstructorInfo constructor = REGISTRY[id].GetConstructor(new Type[] { typeof(uint), typeof(uint), typeof(byte) });

            if(constructor == null) {
                constructor = REGISTRY[id].GetConstructor(new Type[] { typeof(uint), typeof(uint) });
                return (BaseBlock) constructor.Invoke(new object[] { x, y });
            }

            return (BaseBlock) constructor.Invoke(new object[] { x, y, data });
        }
    }
}
