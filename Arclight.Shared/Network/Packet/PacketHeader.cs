using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Shared.Network.Packet
{
    public class PacketHeader : IReadable, IWritable
    {
        public const uint Size = 5u;

        public ushort Seed { get; set; }
        public ushort Length { get; set; } // inclusive
        public byte Unknown2 { get; set; }

        public void Read(BinaryReader reader)
        {
            Seed     = reader.ReadUInt16();
            Length   = reader.ReadUInt16();
            Unknown2 = reader.ReadByte();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Seed);
            writer.Write(Length);
            writer.Write(Unknown2);
        }
    }
}
