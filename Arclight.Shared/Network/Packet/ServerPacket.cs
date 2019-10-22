using System.IO;
using Arclight.Shared.Cryptography;
using Arclight.Shared.Network.Message;

namespace Arclight.Shared.Network.Packet
{
    public class ServerPacket : IPacket
    {
        public PacketHeader Header { get; }
        public byte[] Data { get; }

        public ServerPacket(MessageOpcode opcode, ushort seed, IWritable message)
        {
            using (var stream = new PacketCryptoStream(seed))
            {
                using var writer = new BinaryWriter(stream);

                writer.Write((byte)((int)opcode >> 8));
                writer.Write((byte)((int)opcode & 0xFF));
                message.Write(writer);

                Data = stream.ToArray();
            }
            
            Header = new PacketHeader
            {
                Seed     = seed,
                Length   = (ushort)(Data.Length + PacketHeader.Size),
                Unknown2 = 1
            };
        }
    }
}
