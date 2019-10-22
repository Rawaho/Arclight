using System.IO;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Auth.Network.Message
{
    [Message(MessageOpcode.ServerLoginResult)]
    public class ServerLoginResult : IWritable
    {
        public uint AccountId { get; set; }
        public byte Unknown4 { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public uint ErrorCode { get; set; }
        public byte Unknown820 { get; set; }
        public string Unknown822 { get; set; } = string.Empty;
        public ulong SessionKey { get; set; }
        public byte Unknown858 { get; set; }
        public ushort Unknown85A { get; set; }
        public byte Unknown85C { get; set; }
        public byte Unknown85D { get; set; }
        public byte Unknown85E { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.Write(AccountId);
            writer.Write(Unknown4);
            writer.Write(new byte[18]); // looks like a MAC address
            writer.WriteStringUtf16(ErrorMessage);
            writer.Write(ErrorCode);
            writer.Write(Unknown820);
            writer.WriteStringUtf16(Unknown822);
            writer.Write(SessionKey);
            writer.Write(Unknown858);
            writer.Write(Unknown85A);
            writer.Write(Unknown85C);
            writer.Write(Unknown85D);
            writer.Write(Unknown85E);
        }
    }
}
