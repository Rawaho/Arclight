using System.IO;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Auth.Network.Message
{
    [Message(MessageOpcode.ServerEnterServer)]
    public class ServerEnterServer : IWritable
    {
        public string Host { get; set; }
        public ushort Port { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.WriteStringUtf8(Host);
            writer.Write(Port);
        }
    }
}
