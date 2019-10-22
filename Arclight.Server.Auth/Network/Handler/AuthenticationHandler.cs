using Arclight.Database.Auth.Model;
using Arclight.Server.Auth.Network.Message;
using Arclight.Shared.Cryptography;
using Arclight.Shared.Database;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Auth.Network.Handler
{
    public static class AuthenticationHandler
    {
        [MessageHandler(MessageOpcode.ClientLoginReq, SessionState.None)]
        public static void HandleClientLoginReq(Session session, ClientLoginReq loginReq)
        {
            void SendError(uint errorCode, string errorMessage = "")
            {
                session.SendMessage(new ServerLoginResult
                {
                    ErrorCode    = errorCode,
                    ErrorMessage = errorMessage
                });
            }

            AccountModel account = DatabaseManager.Instance.AuthDatabase.GetAccount(loginReq.Username);
            if (account == null || !BCryptProvider.Verify(loginReq.Password, account.Password))
            {
                SendError(1);
                return;
            }

            session.Authenticate(account);

            // TODO: check for bans

            ulong sessionKey = RandomProvider.GenerateSessionKey();
            DatabaseManager.Instance.AuthDatabase.UpdateSessionKey(account, sessionKey);

            session.SendMessage(new ServerLoginResult
            {
                AccountId    = account.Id,
                SessionKey   = sessionKey
            });

            session.SendMessage(new ServerOptionLoad
            {
            });
        }
    }
}
