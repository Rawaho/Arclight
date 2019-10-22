using Arclight.Database.Auth;
using Arclight.Database.Character;
using Arclight.Database.Configuration;
using NLog;

namespace Arclight.Shared.Database
{
    public sealed class DatabaseManager : Singleton<DatabaseManager>
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public AuthDatabase AuthDatabase { get; private set; }
        public CharacterDatabase CharacterDatabase { get; private set; }

        private DatabaseManager()
        {
        }

        public void Initialise(DatabaseConfiguration configuration)
        {
            if (configuration.Auth != null)
                AuthDatabase = new AuthDatabase(configuration);
            if (configuration.Character != null)
                CharacterDatabase = new CharacterDatabase(configuration);
        }

        public void Migrate()
        {
            log.Info("Applying database migrations...");

            AuthDatabase?.Migrate();
            CharacterDatabase?.Migrate();
        }
    }
}
