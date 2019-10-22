using System.Collections.Generic;

namespace Arclight.Database.Auth.Model
{
    public class ServerModel
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public ushort Port { get; set; }

        public HashSet<ServerClusterModel> Nodes { get; set; } = new HashSet<ServerClusterModel>();
        public HashSet<AccountCharacterCountModel> CharacterCounts { get; set; } = new HashSet<AccountCharacterCountModel>();
    }
}
