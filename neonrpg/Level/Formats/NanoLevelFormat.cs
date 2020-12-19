using neonrpg.Block;
using neonrpg.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace neonrpg.Level.Formats {

    class NanoLevelFormat : LevelFormat {

        private static readonly byte[] MAGIC = { 78, 65, 78, 48, 227, 200, 162, 213 };
        private static readonly byte[] ENTITIES_START_MAGIC = { 149, 151, 147, 153 };

        // awful code
        public override BaseLevel Parse(string name, byte[] buffer) {
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

            ushort[] spawnCoordinates = new ushort[2];
            List<BaseBlock> blocks;
            List<BaseEntity> entities;

            ushort x;
            ushort y;

            switch (version) {
                case 1:
                    spawnCoordinates[0] = (ushort)Math.Floor(width / 2d);
                    spawnCoordinates[1] = (ushort)Math.Floor(height / 2d);

                    blocks = new List<BaseBlock>();
                    entities = new List<BaseEntity>();

                    while (buffer.Length > 0) {
                        buffer0 = buffer.Take(2).ToArray();
                        Array.Reverse(buffer0);
                        x = BitConverter.ToUInt16(buffer0);
                        buffer = buffer.Skip(2).ToArray();

                        buffer0 = buffer.Take(2).ToArray();
                        Array.Reverse(buffer0);
                        y = BitConverter.ToUInt16(buffer0);
                        buffer = buffer.Skip(2).ToArray();

                        byte id = buffer[0];
                        byte data = buffer[1];
                        buffer = buffer.Skip(2).ToArray();

                        blocks.Add(BlockRepository.CreateFromId(id, x, y, data));
                        entities.Add(null);
                    }
                    break;
                case 2:
                    buffer0 = buffer.Take(2).ToArray();
                    Array.Reverse(buffer0);
                    x = BitConverter.ToUInt16(buffer0);
                    buffer = buffer.Skip(2).ToArray();

                    buffer0 = buffer.Take(2).ToArray();
                    Array.Reverse(buffer0);
                    y = BitConverter.ToUInt16(buffer0);
                    buffer = buffer.Skip(2).ToArray();

                    spawnCoordinates[0] = x;
                    spawnCoordinates[1] = y;

                    blocks = new List<BaseBlock>();
                    entities = new List<BaseEntity>();

                    while (!buffer.Take(ENTITIES_START_MAGIC.Length).ToArray().SequenceEqual(ENTITIES_START_MAGIC)) {
                        buffer0 = buffer.Take(2).ToArray();
                        Array.Reverse(buffer0);
                        x = BitConverter.ToUInt16(buffer0);
                        buffer = buffer.Skip(2).ToArray();

                        buffer0 = buffer.Take(2).ToArray();
                        Array.Reverse(buffer0);
                        y = BitConverter.ToUInt16(buffer0);
                        buffer = buffer.Skip(2).ToArray();

                        byte id = buffer[0];
                        byte data = buffer[1];
                        buffer = buffer.Skip(2).ToArray();

                        blocks.Add(BlockRepository.CreateFromId(id, x, y, data));
                        entities.Add(null);
                    }

                    buffer = buffer.Skip(ENTITIES_START_MAGIC.Length).ToArray();

                    while (buffer.Length > 0) {
                        buffer0 = buffer.Take(2).ToArray();
                        Array.Reverse(buffer0);
                        x = BitConverter.ToUInt16(buffer0);
                        buffer = buffer.Skip(2).ToArray();

                        buffer0 = buffer.Take(2).ToArray();
                        Array.Reverse(buffer0);
                        y = BitConverter.ToUInt16(buffer0);
                        buffer = buffer.Skip(2).ToArray();

                        byte id = buffer[0];
                        byte data = buffer[1];
                        buffer = buffer.Skip(2).ToArray();

                        int index = (y * width) + x;

                        entities[index] = EntityRepository.CreateFromId(id, x, y, data);
                    }
                    break;
                default:
                    throw new InvalidDataException("Unknown version.");
            }

            return new BaseLevel(name, width, height, blocks, entities, spawnCoordinates);
        }
    }
}
