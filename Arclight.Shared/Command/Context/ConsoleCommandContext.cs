using Arclight.Shared.Network;
using NLog;

namespace Arclight.Shared.Command.Context
{
    public class ConsoleCommandContext : ICommandContext
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public Session Session { get; } = null;

        public void SendMessage(string message)
        {
            log.Info(message);
        }

        public void SendError(string message)
        {
            log.Error(message);
        }
    }
}
