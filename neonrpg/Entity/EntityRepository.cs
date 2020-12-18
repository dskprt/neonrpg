using neonrpg.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace neonrpg.Entity {

    class EntityRepository {

        public static readonly Dictionary<byte, Type> REGISTRY = new Dictionary<byte, Type>() {
            { 0, typeof(EntityPlayer) }
        };

        public static BaseEntity CreateFromId(byte id, ushort x, ushort y, byte data = 0) {
            ConstructorInfo constructor = REGISTRY[id].GetConstructor(new Type[] { typeof(ushort), typeof(ushort), typeof(byte) });

            if (constructor == null) {
                constructor = REGISTRY[id].GetConstructor(new Type[] { typeof(ushort), typeof(ushort) });
                return (BaseEntity)constructor.Invoke(new object[] { x, y });
            }

            return (BaseEntity)constructor.Invoke(new object[] { x, y, data });
        }
    }
}
