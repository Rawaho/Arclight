using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.World.Network.Message
{
    [Message(MessageOpcode.ClientCharacterInfoReq)]
    public class ClientCharacterInfoReq : IReadable
    {
        public uint CharacterId { get; private set; }
        public byte Unknown { get; private set; }

        public void Read(BinaryReader reader)
        {
            CharacterId = reader.ReadUInt32();
            Unknown     = reader.ReadByte();
        }
    }
}
