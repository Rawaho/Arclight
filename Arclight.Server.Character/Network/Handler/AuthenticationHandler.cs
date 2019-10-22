using Arclight.Database.Auth.Model;
using Arclight.Server.Character.Network.Message;
using Arclight.Shared.Database;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Character.Network.Handler
{
    public static class AuthenticationHandler
    {
        [MessageHandler(MessageOpcode.ClientEnterServerReq, SessionState.None)]
        public static void HandleEnterServerReq(CharacterSession session, ClientEnterServerReq enterServerReq)
        {
            void SendError(byte result)
            {
                session.SendMessage(new ServerEnterServerRes
                {
                    Result = result
                });
            }

            AccountModel account = DatabaseManager.Instance.AuthDatabase.GetAccount(enterServerReq.AccountId, enterServerReq.SessionKey);
            if (account == null)
            {
                SendError(1);
                return;
            }

            session.Authenticate(account);
            session.SendMessage(new ServerEnterServerRes
            {
                Result    = 0,
                AccountId = account.Id
            });
        }
    }
}
