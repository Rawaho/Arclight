using System;
using System.IO;
using System.Text;

namespace Arclight.Shared.Network
{
    public static class Extensions
    {
        public static int Remaining(this Stream stream)
        {
            if (stream.Length < stream.Position)
                throw new InvalidOperationException();

            return (int)(stream.Length - stream.Position);
        }

        /// <summary>
        /// Reads a UTF-8 length prefixed string.
        /// </summary>
        public static string ReadStringUtf8(this BinaryReader reader)
        {
            ushort length = reader.ReadUInt16();
            return Encoding.UTF8.GetString(reader.ReadBytes(length));
        }

        /// <summary>
        /// Writes a UTF-8 length prefixed string.
        /// </summary>
        public static void WriteStringUtf8(this BinaryWriter writer, string data)
        {
            writer.Write((ushort)Encoding.UTF8.GetByteCount(data));
            writer.Write(Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// Reads a UTF-16 length prefixed string.
        /// </summary>
        public static string ReadStringUtf16(this BinaryReader reader)
        {
            ushort length = reader.ReadUInt16();
            return Encoding.Unicode.GetString(reader.ReadBytes(length));
        }

        /// <summary>
        /// Writes a UTF-16 length prefixed string.
        /// </summary>
        public static void WriteStringUtf16(this BinaryWriter writer, string data)
        {
            writer.Write((ushort)Encoding.Unicode.GetByteCount(data));
            writer.Write(Encoding.Unicode.GetBytes(data));
        }
    }
}
