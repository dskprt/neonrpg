using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace neonrpg.Level {

    class Levels {

        public static BaseLevel LoadLevelFromResources(string name, string format = ".nano") {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("neonrpg.Assets.Levels." + name + ".nano")) {
                using (MemoryStream memoryStream = new MemoryStream()) {
                    stream.CopyTo(memoryStream);
                    return LevelFormat.FORMATS[format].Parse(name, memoryStream.ToArray());
                }
            }
        }

        public static byte[] ReadLevelResource(BaseLevel level, string resource) {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("neonrpg.Assets.Levels." + level.Name + "_Data." + resource)) {
                using (MemoryStream memoryStream = new MemoryStream()) {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        public static string ReadLevelResourceAsString(BaseLevel level, string resource) {
            return Encoding.Default.GetString(ReadLevelResource(level, resource));
        }
    }
}
