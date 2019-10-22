using System;

namespace Arclight.Shared.Command
{
    public class CommandException : Exception
    {
        public CommandException(string message)
            : base(message)
        {
        }
    }
}
