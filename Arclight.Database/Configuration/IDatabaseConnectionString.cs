namespace Arclight.Database.Configuration
{
    public interface IDatabaseConnectionString
    {
        DatabaseProvider Provider { get; set; }
        string ConnectionString { get; set; }
    }
}
