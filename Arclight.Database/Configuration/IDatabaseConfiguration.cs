namespace Arclight.Database.Configuration
{
    public interface IDatabaseConfiguration
    {
        IDatabaseConnectionString GetConnectionString(DatabaseType type);
    }
}
