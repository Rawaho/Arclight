using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.World.Network.Message
{
    [Message(MessageOpcode.ServerWorldVersion)]
    public class ServerWorldVersion : IWritable
    {
        public uint Unknown { get; set; }
        public uint Unknown2 { get; set; }
        public uint Unknown3 { get; set; }
        public uint Unknown4 { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Unknown);
            writer.Write(Unknown2);
            writer.Write(Unknown3);
            writer.Write(Unknown4);
        }
    }
}
