using neonrpg.Item.Items;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace neonrpg.Item {

    class ItemRepository {

        public static readonly Dictionary<byte, Type> REGISTRY = new Dictionary<byte, Type>() {
            { 0, typeof(ItemDummy) }
        };

        public static BaseItem CreateFromId(byte id, byte data = 0) {
            ConstructorInfo constructor = REGISTRY[id].GetConstructor(new Type[] { typeof(byte) });

            if (constructor == null) {
                constructor = REGISTRY[id].GetConstructor(new Type[] { });
                return (BaseItem)constructor.Invoke(new object[] { });
            }

            return (BaseItem)constructor.Invoke(new object[] { data });
        }
    }
}
