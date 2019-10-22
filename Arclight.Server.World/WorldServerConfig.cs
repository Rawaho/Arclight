using Arclight.Database.Configuration;
using Arclight.Shared.Configuration;

namespace Arclight.Server.World
{
    public class WorldServerConfig
    {
        public NetworkConfiguration Network { get; set; }
        public DatabaseConfiguration Database { get; set; }
    }
}
