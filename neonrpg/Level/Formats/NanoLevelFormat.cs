using neonrpg.Block;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace neonrpg.Level.Formats {

    class NanoLevelFormat : LevelFormat {

        private static readonly byte[] MAGIC = { 78, 65, 78, 48, 227, 200, 162, 213 };

        // awful code
        public override BaseLevel Parse(Stream stream) {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer);

            if(!buffer.Take(MAGIC.Length).SequenceEqual(MAGIC)) {
                throw new InvalidDataException("Bad magic.");
            }

            buffer = buffer.Skip(MAGIC.Length).ToArray();

            byte version = buffer[0];
            buffer = buffer.Skip(1).ToArray();

            byte[] buffer0 = buffer.Take(2).ToArray();
            Array.Reverse(buffer0);
            ushort width = BitConverter.ToUInt16(buffer0);
            buffer = buffer.Skip(2).ToArray();

            buffer0 = buffer.Take(2).ToArray();
            Array.Reverse(buffer0);
            ushort height = BitConverter.ToUInt16(buffer0);
            buffer = buffer.Skip(2).ToArray();

            List<BaseBlock> blocks = new List<BaseBlock>();

            while(buffer.Length > 0) {
                buffer0 = buffer.Take(2).ToArray();
                Array.Reverse(buffer0);
                ushort x = BitConverter.ToUInt16(buffer0);
                buffer = buffer.Skip(2).ToArray();

                buffer0 = buffer.Take(2).ToArray();
                Array.Reverse(buffer0);
                ushort y = BitConverter.ToUInt16(buffer0);
                buffer = buffer.Skip(2).ToArray();

                byte id = buffer[0];
                byte data = buffer[1];
                buffer = buffer.Skip(2).ToArray();

                blocks.Add(BlockRepository.CreateFromId(id, x, y, data));
            }

            return new BaseLevel(width, height, blocks);
        }
    }
}
