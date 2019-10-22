using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using Arclight.Shared.Configuration;
using NLog;

namespace Arclight.Shared.Network
{
    public sealed class NetworkManager<T> : Singleton<NetworkManager<T>> where T : Session, new()
    {
        private static readonly ILogger log = LogManager.GetLogger("NetworkManager");

        private ConnectionListener<T> connectionListener;

        private readonly ConcurrentQueue<T> pendingAdd = new ConcurrentQueue<T>();
        private readonly ConcurrentQueue<T> pendingRemove = new ConcurrentQueue<T>();

        private readonly HashSet<T> sessions = new HashSet<T>();

        private NetworkManager()
        {
        }

        public void Initialise(NetworkConfiguration config)
        {
            connectionListener = new ConnectionListener<T>(IPAddress.Parse(config.Host), config.Port);
            connectionListener.OnNewSession += session => pendingAdd.Enqueue(session);

            log.Info($"Listening for connections on port {config.Port}.");
        }

        public void Shutdown()
        {
            connectionListener?.Shutdown();
        }

        public void Update(double lastTick)
        {
            while (pendingRemove.TryDequeue(out T session))
                sessions.Remove(session);

            while (pendingAdd.TryDequeue(out T session))
                sessions.Add(session);

            foreach (T session in sessions)
                session.Update(lastTick);
        }
    }
}
