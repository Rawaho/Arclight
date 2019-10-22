using Arclight.Shared.Command.Context;

namespace Arclight.Shared.Command
{
    public interface ICommandCategory
    {
        CommandResult Invoke(ICommandContext context, string[] parameters, uint depth);
    }
}
