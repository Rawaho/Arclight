using System;
using Arclight.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Arclight.Database
{
    public static class Extensions
    {
        public static void UseConfiguration(this DbContextOptionsBuilder optionsBuilder, IDatabaseConfiguration configuration, DatabaseType type)
        {
            IDatabaseConnectionString connectionString = configuration.GetConnectionString(type);
            switch (connectionString.Provider)
            {
                case DatabaseProvider.MySql:
                    optionsBuilder.UseMySql(connectionString.ConnectionString);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
