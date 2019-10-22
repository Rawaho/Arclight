using Arclight.Database.Auth.Model;
using Arclight.Server.Auth.Network.Message;
using Arclight.Shared.Game;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Auth.Network.Handler
{
    public static class ServerHandler
    {
        [MessageHandler(MessageOpcode.ClientServerListReq)]
        public static void HandleServerListReq(Session session, ClientServerListReq serverListReq)
        {
            var serverList = new ServerServerList();
            foreach (ServerModel server in ServerManager.Instance)
            {
                serverList.Servers.Add(new ServerServerList.Server
                {
                    Id         = (ushort)server.Id,
                    Host       = server.Host,
                    Port       = server.Port,
                    Name       = server.Name,
                    Population = 1
                });
            }

            session.SendMessage(serverList);
        }

        [MessageHandler(MessageOpcode.ClientServerConnectReq)]
        public static void HandleServerConnectReq(Session session, ClientServerConnectReq serverConnectReq)
        {
            ServerModel server = ServerManager.Instance.GetServer(serverConnectReq.ServerId);
            if (server == null)
                return;

            session.SendMessage(new ServerEnterServer
            {
                Host = server.Host,
                Port = server.Port
            });
        }
    }
}
