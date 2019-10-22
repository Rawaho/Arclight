using Arclight.Database.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace Arclight.Database.Character
{
    public class CharacterContextFactory : IDesignTimeDbContextFactory<CharacterContext>
    {
        public CharacterContext CreateDbContext(string[] args)
        {
            return new CharacterContext(new DatabaseConfiguration
            {
                Character = new DatabaseConnectionString
                {
                    Provider         = DatabaseProvider.MySql,
                    ConnectionString = "server=127.0.0.1;port=3306;username=arclight;password=arclight;database=arclight_character;"
                }
            });
        }
    }
}
