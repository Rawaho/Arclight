using Arclight.Database.Configuration;
using Arclight.Shared.Configuration;

namespace Arclight.Server.Auth
{
    public class AuthServerConfiguration
    {
        public NetworkConfiguration Network { get; set; }
        public DatabaseConfiguration Database { get; set; }
    }
}
