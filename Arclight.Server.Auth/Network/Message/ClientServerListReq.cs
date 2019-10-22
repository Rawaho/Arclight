using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Auth.Network.Message
{
    [Message(MessageOpcode.ClientServerListReq)]
    public class ClientServerListReq : IReadable
    {
        public uint Unknown0 { get; set; }

        public void Read(BinaryReader reader)
        {
            Unknown0 = reader.ReadUInt32();
        }
    }
}
