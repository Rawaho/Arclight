using System.IO;
using Arclight.Shared.Network.Message;
using Arclight.Shared.Network.Message.Shared;

namespace Arclight.Server.World.Network.Message
{
    [Message(MessageOpcode.ServerEnterGameServerRes)]
    public class ServerEnterGameServerRes : IWritable
    {
        public uint Unknown { get; set; }
        public byte Result { get; set; }
        public WorldPosition Position { get; set; }
        public byte Unknown2 { get; set; }
        public uint Unknown3 { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Unknown);
            writer.Write(Result);

            Position.Write(writer);

            writer.Write(Unknown2);
            writer.Write(Unknown3);
        }
    }
}
