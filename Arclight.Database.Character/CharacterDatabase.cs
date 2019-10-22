using System.Collections.Generic;
using System.Linq;
using Arclight.Database.Character.Model;
using Arclight.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Arclight.Database.Character
{
    public class CharacterDatabase
    {
        private readonly IDatabaseConfiguration configuration;

        public CharacterDatabase(IDatabaseConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Migrate()
        {
            using var context = new CharacterContext(configuration);
            context.Database.Migrate();
        }

        /// <summary>
        /// Return all <see cref="CharacterModel"/> for supplied account id.
        /// </summary>
        public List<CharacterModel> GetCharacters(uint accountId)
        {
            using var context = new CharacterContext(configuration);
            return context.Characters
                .Where(c => c.AccountId == accountId)
                .ToList();
        }

        public CharacterModel GetCharacter(uint accountId, uint characterId)
        {
            using var context = new CharacterContext(configuration);
            return context.Characters.SingleOrDefault(c => c.Id == characterId && c.AccountId == accountId);
        }

        public void CreateCharacter(CharacterModel character)
        {
            using var context = new CharacterContext(configuration);
            context.Add(character);
            context.SaveChanges();
        }
    }
}
