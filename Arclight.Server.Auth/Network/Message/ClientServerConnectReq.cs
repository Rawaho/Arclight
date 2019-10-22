using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Auth.Network.Message
{
    [Message(MessageOpcode.ClientServerConnectReq)]
    public class ClientServerConnectReq : IReadable
    {
        public ushort ServerId { get; private set; }

        public void Read(BinaryReader reader)
        {
            ServerId = reader.ReadUInt16();
        }
    }
}
