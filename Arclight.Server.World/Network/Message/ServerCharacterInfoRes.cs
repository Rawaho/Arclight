using System.IO;
using Arclight.Server.World.Network.Message.Shared;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.World.Network.Message
{
    [Message(MessageOpcode.ServerCharacterInfoRes)]
    public class ServerCharacterInfoRes : IWritable
    {
        public class MyCharacterInfoEx : IWritable
        {
            public CharacterInfoEx CharacterInfo { get; set; }
            public uint Unknown2E8 { get; set; }
            public ulong Unknown2F0 { get; set; }
            public byte Unknown318 { get; set; }
            public byte Unknown319 { get; set; }
            public byte Unknown31A { get; set; }
            public byte Unknown31B { get; set; }
            public uint Unknown344 { get; set; }
            public uint Unknown34C { get; set; }
            public string Unknown32C { get; set; } = string.Empty;
            public byte Unknown31C { get; set; }
            public ulong Unknown310 { get; set; }
            public uint Unknown320 { get; set; }
            public uint Unknown324 { get; set; }
            public uint Unknown328 { get; set; }

            public void Write(BinaryWriter writer)
            {
                CharacterInfo.Write(writer);

                writer.Write(Unknown2E8);
                writer.Write(Unknown2F0);
                writer.Write(Unknown318);
                writer.Write(Unknown319);
                writer.Write(Unknown31A);
                writer.Write(Unknown31B);
                writer.Write(Unknown344);
                writer.Write(Unknown34C);

                // either 3xlong or 24 bytes
                writer.Write(0L);
                writer.Write(0L);
                writer.Write(0L);

                writer.WriteStringUtf8(Unknown32C);
                writer.Write(Unknown31C);
                writer.Write(Unknown310);
                writer.Write(Unknown320);
                writer.Write(Unknown324);
                writer.Write(Unknown328);
            }
        }

        public MyCharacterInfoEx MyCharacterInfo { get; set; }
        public byte Unknown { get; set; }
        public byte Unknown2 { get; set; }
        public byte Unknown3 { get; set; }

        public void Write(BinaryWriter writer)
        {
            MyCharacterInfo.Write(writer);

            writer.Write(Unknown);
            writer.Write(Unknown2);
            writer.Write(Unknown3);
        }
    }
}
