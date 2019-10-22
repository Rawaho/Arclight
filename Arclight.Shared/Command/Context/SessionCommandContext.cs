using Arclight.Shared.Network;

namespace Arclight.Shared.Command.Context
{
    public class SessionCommandContext : ICommandContext
    {
        public Session Session { get; }

        public SessionCommandContext(Session session)
        {
            Session = Session;
        }

        public void SendMessage(string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendError(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
