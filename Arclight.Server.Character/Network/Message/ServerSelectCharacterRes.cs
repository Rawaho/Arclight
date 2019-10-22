using System.IO;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;
using Arclight.Shared.Network.Message.Shared;

namespace Arclight.Server.Character.Network.Message
{
    [Message(MessageOpcode.ServerSelectCharacterRes)]
    public class ServerSelectCharacterRes : IWritable
    {
        public class UnknownStructure : IWritable
        {
            public uint CharacterId { get; set; }
            public uint AccountId { get; set; }
            public uint Unknown8 { get; set; }
            public uint UnknownC { get; set; }
            public uint Unknown10 { get; set; }
            public string Host { get; set; }
            public ushort Port { get; set; }
            public WorldPosition Position { get; set; }
            public byte Unknown250 { get; set; }

            public void Write(BinaryWriter writer)
            {
                writer.Write(CharacterId);
                writer.Write(AccountId);
                writer.Write(Unknown8);
                writer.Write(UnknownC);
                writer.Write(Unknown10);
                writer.Write(new byte[16]);
                writer.WriteStringUtf8(Host);
                writer.Write(Port);

                Position.Write(writer);

                writer.Write(Unknown250);
            }
        }

        public UnknownStructure Unknown0 { get; set; }

        public void Write(BinaryWriter writer)
        {
            Unknown0.Write(writer);

            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write(0);
            writer.Write((byte)0);
            writer.Write(0);
        }
    }
}
