using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.World.Network.Message
{
    [Message(MessageOpcode.ClientEnterGameServerReq)]
    public class ClientEnterGameServerReq : IReadable
    {
        public uint AccountId { get; private set; }
        public uint CharacterId { get; private set; }
        public ulong Unknown { get; private set; }
        public byte Unknown2 { get; private set; }
        public ulong SessionKey { get; private set; }

        public void Read(BinaryReader reader)
        {
            AccountId   = reader.ReadUInt32();
            CharacterId = reader.ReadUInt32();
            Unknown     = reader.ReadUInt64();
            Unknown2    = reader.ReadByte();
            SessionKey  = reader.ReadUInt64();
        }
    }
}
