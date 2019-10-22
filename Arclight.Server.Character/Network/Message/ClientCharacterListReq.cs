using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Character.Network.Message
{
    [Message(MessageOpcode.ClientCharacterListReq)]
    public class ClientCharacterListReq : IReadable
    {
        public ulong SessionKey { get; private set; }

        public void Read(BinaryReader reader)
        {
            SessionKey = reader.ReadUInt64();
        }
    }
}
