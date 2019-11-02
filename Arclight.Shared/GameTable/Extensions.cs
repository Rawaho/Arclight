using System.IO;
using System.Text;

namespace Arclight.Shared.GameTable
{
    public static class Extensions
    {
        public static string ReadStringUtf16(this BinaryReader reader)
        {
            ushort length = reader.ReadUInt16();
            return Encoding.Unicode.GetString(reader.ReadBytes(length * 2));
        }

        public static string ReadHexString(this BinaryReader reader)
        {
            ushort length = reader.ReadUInt16();
            return Encoding.UTF8.GetString(reader.ReadBytes(length));
        }
    }
}
