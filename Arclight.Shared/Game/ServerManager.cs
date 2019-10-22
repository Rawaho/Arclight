using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Arclight.Database.Auth.Model;
using Arclight.Shared.Database;

namespace Arclight.Shared.Game
{
    public sealed class ServerManager : Singleton<ServerManager>, IEnumerable<ServerModel>
    {
        private ImmutableList<ServerModel> servers;

        private ServerManager()
        {
        }

        public void Initialise()
        {
            servers = DatabaseManager.Instance.AuthDatabase.GetServers();
        }

        /// <summary>
        /// Return <see cref="ServerModel"/> for supplied server id.
        /// </summary>
        public ServerModel GetServer(ushort id)
        {
            return servers.SingleOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Return the first available <see cref="ServerClusterModel"/> for supplied server id.
        /// </summary>
        public ServerClusterModel GetServerNode(ushort id)
        {
            // TODO: just returns the first node, actually load balance this
            ServerModel server = GetServer(id);
            return server.Nodes.First();
        }

        public IEnumerator<ServerModel> GetEnumerator()
        {
            return servers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
