using System.Collections.Generic;
using System.IO;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Auth.Network.Message
{
    [Message(MessageOpcode.ServerServerList)]
    public class ServerServerList : IWritable
    {
        public class Server : IWritable
        {
            public ushort Id { get; set; }
            public ushort Port { get; set; }
            public string Name { get; set; }
            public string Host { get; set; }
            public uint Population { get; set; }
            public uint Unknown3 { get; set; }
            public byte CharacterCount { get; set; }

            public void Write(BinaryWriter writer)
            {
                writer.Write(Id);
                writer.Write(Port);
                writer.WriteStringUtf8(Name);
                writer.WriteStringUtf8(Host);
                writer.Write(Population);
                writer.Write(Unknown3);
                writer.Write(CharacterCount);
            }
        }

        public byte Unknown1 { get; set; }
        public List<Server> Servers { get; } = new List<Server>();

        public void Write(BinaryWriter writer)
        {
            writer.Write(Unknown1);
            writer.Write((byte)Servers.Count);

            foreach (Server server in Servers)
                server.Write(writer);
        }
    }
}
