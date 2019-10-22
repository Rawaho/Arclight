using System.IO;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.World.Network.Message
{
    [Message(MessageOpcode.BiSystemKeepAlive)]
    public class BiSystemKeepAlive : IReadable, IWritable
    {
        public uint Seed { get; set; }
        public ulong TickCount { get; set; } // for client at least this is GetTickCount64
        public string Check { get; private set; }

        public void Read(BinaryReader reader)
        {
            TickCount = reader.ReadUInt64();
            Seed      = reader.ReadUInt32();
            Check     = reader.ReadStringUtf8();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(0L);
            writer.Write(Seed);
        }
    }
}
