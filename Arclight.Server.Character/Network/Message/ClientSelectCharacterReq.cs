using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Character.Network.Message
{
    [Message(MessageOpcode.ClientSelectCharacterReq)]
    public class ClientSelectCharacterReq : IReadable
    {
        public uint CharacterId { get; private set; }
        public uint Unknown4 { get; private set; }
        public uint Unknown8 { get; private set; }
        public uint UnknownC { get; private set; }
        public uint Unknown10 { get; private set; }

        public void Read(BinaryReader reader)
        {
            CharacterId = reader.ReadUInt32();
            Unknown4    = reader.ReadUInt32();
            Unknown8    = reader.ReadByte();
            UnknownC    = reader.ReadUInt32();
            Unknown10   = reader.ReadUInt32();
        }
    }
}
