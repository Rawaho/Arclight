using Arclight.Database.Configuration;
using Arclight.Shared.Configuration;

namespace Arclight.Server.Character
{
    public class CharacterServerConfig
    {
        public class ServerConfiguration
        {
            public ushort Id { get; set; }
        }

        public ServerConfiguration Server { get; set; }
        public NetworkConfiguration Network { get; set; }
        public DatabaseConfiguration Database { get; set; }
    }
}
