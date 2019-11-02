using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Arclight.Shared.GameTable
{
    public class GameTable<T> : IEnumerable<T> where T : class
    {
        private readonly Dictionary<ulong, T> entries = new Dictionary<ulong, T>();

        /// <summary>
        /// Create a new <see cref="GameTable{T}"/> from the supplied table file.
        /// </summary>
        public GameTable(string path)
        {
            // sort by metadata token to make sure the order is guaranteed
            FieldInfo[] fields = typeof(T)
                .GetFields()
                .OrderBy(f => f.MetadataToken)
                .ToArray();

            if (fields.Length == 0)
                throw new GameTableException("No valid fields for model!");

            FieldInfo index = null;
            foreach (FieldInfo info in fields)
            {
                GameTableIndexAttribute attribute = info.GetCustomAttribute<GameTableIndexAttribute>();
                if (attribute == null)
                    continue;

                if (index != null)
                    throw new GameTableException("More than a single index field is defined for model!");

                index = info;
            }

            Read(path, fields, index);
        }

        private void Read(string path, FieldInfo[] fields, FieldInfo index)
        {
            // incremental index value is used if not index field is specified
            ulong id = 1;

            using FileStream stream = new FileStream(path, FileMode.Open);
            using BinaryReader reader = new BinaryReader(stream);

            uint count = reader.ReadUInt32();
            for (uint i = 0; i < count; i++)
            {
                T entry = ReadEntry(reader, fields);
                if (index != null)
                {
                    id = Convert.ToUInt64(index.GetValue(entry));
                    entries.Add(id, entry);
                }
                else
                    entries.Add(id++, entry);
            }

            // TODO: validate digest
            reader.ReadHexString();
            
            if (stream.Position != stream.Length)
                throw new GameTableException("Failed to read the entire table!");
        }

        private T ReadEntry(BinaryReader reader, FieldInfo[] fields)
        {
            T entry = Activator.CreateInstance<T>();
            foreach (FieldInfo info in fields)
            {
                switch (info.FieldType.Name)
                {
                    case nameof(Byte):
                        info.SetValue(entry, reader.ReadByte());
                        break;
                    case nameof(UInt16):
                        info.SetValue(entry, reader.ReadUInt16());
                        break;
                    case nameof(UInt32):
                        info.SetValue(entry, reader.ReadUInt32());
                        break;
                    case nameof(String):
                        info.SetValue(entry, reader.ReadStringUtf16());
                        break;
                    default:
                        throw new GameTableException($"Unhandled field {info.FieldType.Name} type for model!");
                }
            }

            return entry;
        }

        /// <summary>
        /// Return an entry with the supplied id.
        /// </summary>
        public T GetEntry(ulong id)
        {
            return entries.TryGetValue(id, out T value) ? value : null;
        }

        /// <summary>
        /// Return an entry that satisfies the supplied predicate.
        /// </summary>
        public T GetEntry(Func<T, bool> predicate)
        {
            return entries.Values.FirstOrDefault(predicate);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return entries.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
