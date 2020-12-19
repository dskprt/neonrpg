using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace neonrpg.Level {

    class Levels {

        public static BaseLevel LoadLevelFromResources(string name, string format = ".nano") {
            return LevelFormat.FORMATS[format].Parse(name, (byte[]) Properties.Resources.ResourceManager.GetObject(name));
        }
    }
}
