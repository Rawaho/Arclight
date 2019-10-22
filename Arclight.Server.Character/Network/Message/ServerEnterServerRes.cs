using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Character.Network.Message
{
    [Message(MessageOpcode.ServerEnterServerRes)]
    public class ServerEnterServerRes : IWritable
    {
        public byte Result { get; set; }
        public uint AccountId { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Result);
            writer.Write(AccountId);
        }
    }
}
