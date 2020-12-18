using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace neonrpg.Level {

    class Levels {

        public static BaseLevel LoadLevelByName(string name, string format = ".nano") {
            return LevelFormat.FORMATS[format].Parse((byte[]) Properties.Resources.ResourceManager.GetObject(name));
        }

        public static BaseLevel LoadLevelFromFile(string file) {
            string extension = Path.GetExtension(file);

            return LevelFormat.FORMATS[extension].Parse(File.ReadAllBytes(file));
        }
    }
}
