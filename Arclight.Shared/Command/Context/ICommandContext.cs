using Arclight.Shared.Network;

namespace Arclight.Shared.Command.Context
{
    public interface ICommandContext
    {
        Session Session { get; }

        void SendMessage(string message);
        void SendError(string message);
    }
}
