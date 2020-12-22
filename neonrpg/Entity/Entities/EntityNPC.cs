using neonrpg.Level;
using neonrpg.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace neonrpg.Entity.Entities {

    class EntityNPC : BaseEntity {

        private NPCType Type { get; set; }
        private Dictionary<int, int> ShopItems { get; set; }

        public EntityNPC(ushort x, ushort y, byte data = 0) : base(1, "NPC", x, y, '%', new Color("250", "90", "0"), true, data) { }

        public override void Initialize(BaseLevel level) {
            dynamic json = JsonConvert.DeserializeObject(Levels.ReadLevelResourceAsString(level, "npc" + this.Data + ".json"));

            this.ShopItems = new Dictionary<int, int>();
            this.Type = (NPCType) json.type;

            if(this.Type == NPCType.SHOP) {
                foreach(dynamic item in json.interaction.items) {
                    ShopItems.Add((int)item.id, (int)item.price);
                }
            }
        }

        public override void Interact(ref BaseLevel level) {
            // TODO actual interaction system
            foreach(var item in this.ShopItems) {
                Debug.WriteLine(item.Key + ": " + item.Value + "$");
            }
        }

        private enum NPCType {

            SHOP = 0
        }
    }
}
