using Arclight.Shared.Command.Context;
using Arclight.Shared.Cryptography;
using Arclight.Shared.Database;

namespace Arclight.Shared.Command.Handler
{
    [Command("account")]
    public class AccountCommandCategory : CommandCategory
    {
        [Command("create")]
        public void AccountCreateCommandHandler(ICommandContext context, string username, string password)
        {
            string encryptedPassword = BCryptProvider.HashPassword(password);
            if (DatabaseManager.Instance.AuthDatabase.CreateAccount(username.ToLower(), encryptedPassword))
                context.SendMessage($"Successfully created Account {username}.");
            else
                context.SendError($"Failed to create account {username}!");
        }
    }
}
