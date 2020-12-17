using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using neonrpg.Block;
using neonrpg.Level.Formats;

namespace neonrpg.Level {

    abstract class LevelFormat {

        public static readonly Dictionary<string, LevelFormat> FORMATS = new Dictionary<string, LevelFormat>() {
            { ".nano", new NanoLevelFormat() }
        };

        public abstract BaseLevel Parse(Stream stream);
    }
}
