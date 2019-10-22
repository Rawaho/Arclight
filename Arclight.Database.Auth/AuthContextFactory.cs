using Arclight.Database.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace Arclight.Database.Auth
{
    public class AuthContextFactory : IDesignTimeDbContextFactory<AuthContext>
    {
        public AuthContext CreateDbContext(string[] args)
        {
            return new AuthContext(new DatabaseConfiguration
            {
                Auth = new DatabaseConnectionString
                {
                    Provider         = DatabaseProvider.MySql,
                    ConnectionString = "server=127.0.0.1;port=3306;username=arclight;password=arclight;database=arclight_auth;"
                }
            });
        }
    }
}
