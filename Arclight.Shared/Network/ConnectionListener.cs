using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Arclight.Shared.Network
{
    public class ConnectionListener<T> where T : Session, new()
    {
        public delegate void NewSessionEvent(T newSession);

        /// <summary>
        /// Raised on <see cref="Session"/> creation for a new client.
        /// </summary>
        public event NewSessionEvent OnNewSession;

        private volatile bool shutdownRequested;

        public ConnectionListener(IPAddress host, int port)
        {
            var listener = new TcpListener(host, port);
            listener.Start();

            Thread listenerThread = new Thread(async () =>
            {
                while (!shutdownRequested)
                {
                    var session = new T();
                    session.Accept(await listener.AcceptSocketAsync());

                    OnNewSession?.Invoke(session);
                }

                listener.Stop();
            });
            listenerThread.Start();
        }

        public void Shutdown()
        {
            shutdownRequested = true;
        }
    }
}
