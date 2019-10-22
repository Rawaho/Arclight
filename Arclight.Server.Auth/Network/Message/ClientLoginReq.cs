using System.IO;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Auth.Network.Message
{
    [Message(MessageOpcode.ClientLoginReq)]
    public class ClientLoginReq : IReadable
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string MacAddress { get; private set; }
        public uint Unknown { get; private set; } // static value of 10000003

        public void Read(BinaryReader reader)
        {
            Username   = reader.ReadStringUtf16();
            Password   = reader.ReadStringUtf16();
            MacAddress = reader.ReadStringUtf16();
            Unknown    = reader.ReadUInt32();
        }
    }
}
