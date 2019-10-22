using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Character.Network.Message
{
    [Message(MessageOpcode.ClientDeleteCharacterReq)]
    public class ClientDeleteCharacterReq : IReadable
    {
        public uint CharacterId { get; private set; }

        public void Read(BinaryReader reader)
        {
            CharacterId = reader.ReadUInt32();
        }
    }
}
