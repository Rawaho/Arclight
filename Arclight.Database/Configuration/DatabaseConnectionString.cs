namespace Arclight.Database.Configuration
{
    public class DatabaseConnectionString : IDatabaseConnectionString
    {
        public DatabaseProvider Provider { get; set; }
        public string ConnectionString { get; set; }
    }
}
