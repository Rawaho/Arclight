using System.IO;
using Arclight.Shared.Network.Message;
using Arclight.Shared.Network.Message.Shared;

namespace Arclight.Server.Character.Network.Message
{
    [Message(MessageOpcode.ClientCreateCharacterReq)]
    public class ClientCreateCharacterReq : IReadable
    {
        public CharacterInfo Character { get; } = new CharacterInfo();
        public uint Outfit { get; private set; }

        public void Read(BinaryReader reader)
        {
            Character.Read(reader);
            Outfit = reader.ReadUInt32();
        }
    }
}
