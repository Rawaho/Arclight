using System;

namespace Arclight.Database.Configuration
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public DatabaseConnectionString Auth { get; set; }
        public DatabaseConnectionString Character { get; set; }
        public DatabaseConnectionString World { get; set; }

        public IDatabaseConnectionString GetConnectionString(DatabaseType type)
        {
            return type switch
            {
                DatabaseType.Auth => Auth,
                DatabaseType.Character => Character,
                DatabaseType.World => World,
                _ => throw new ArgumentException("")
            };
        }
    }
}
