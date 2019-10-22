using System.Collections.Immutable;
using System.Linq;
using Arclight.Database.Auth.Model;
using Arclight.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Arclight.Database.Auth
{
    public class AuthDatabase
    {
        private readonly IDatabaseConfiguration configuration;

        public AuthDatabase(IDatabaseConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Migrate()
        {
            using var context = new AuthContext(configuration);
            context.Database.Migrate();
        }

        /// <summary>
        /// Return <see cref="AccountModel"/> by supplied username.
        /// </summary>
        public AccountModel GetAccount(string username)
        {
            using var context = new AuthContext(configuration);
            return context.Accounts
                .Include(m => m.CharacterCounts)
                .SingleOrDefault(a => a.Username == username);
        }

        /// <summary>
        /// Return <see cref="AccountModel"/> by supplied id and session key.
        /// </summary>
        public AccountModel GetAccount(uint id, ulong sessionKey)
        {
            using var context = new AuthContext(configuration);
            return context.Accounts
                .Include(m => m.CharacterCounts)
                .SingleOrDefault(a => a.Id == id && a.SessionKey == sessionKey);
        }

        /// <summary>
        /// Create a new <see cref="AccountModel"/> with supplied username and bcrypt digest.
        /// </summary>
        public bool CreateAccount(string username, string bcryptDigest)
        {
            using var context = new AuthContext(configuration);
            context.Add(new AccountModel
            {
                Username = username,
                Password = bcryptDigest
            });

            try
            {
                return context.SaveChanges() != 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Update <see cref="AccountModel"/> with session key.
        /// </summary>
        public void UpdateSessionKey(AccountModel account, ulong sessionKey)
        {
            using var context = new AuthContext(configuration);
            account.SessionKey = sessionKey;
            context.Update(account);
            context.SaveChanges();
        }

        public ImmutableList<ServerModel> GetServers()
        {
            using var context = new AuthContext(configuration);
            return context.Servers
                .Include(m => m.Nodes)
                .AsNoTracking()
                .ToImmutableList();
        }
    }
}
