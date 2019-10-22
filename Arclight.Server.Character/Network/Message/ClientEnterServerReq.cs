using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Character.Network.Message
{
    [Message(MessageOpcode.ClientEnterServerReq)]
    public class ClientEnterServerReq : IReadable
    {
        public uint AccountId { get; private set; }
        public ushort Unknown { get; private set; }
        public ulong SessionKey { get; private set; }
        public byte Unknown2 { get; private set; }

        public void Read(BinaryReader reader)
        {
            AccountId  = reader.ReadUInt32();
            Unknown    = reader.ReadUInt16();
            SessionKey = reader.ReadUInt64();
            Unknown2   = reader.ReadByte();
        }
    }
}
