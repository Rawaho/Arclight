using Arclight.Shared.Command.Context;

namespace Arclight.Shared.Command.Handler
{
    [Command("test")]
    public class TestCommandCategory : CommandCategory
    {
        [Command("nested")]
        public class NestedCommandCategory : CommandCategory
        {
            [Command("meme")]
            public void MemeCommandHandler(ICommandContext context, uint test, string test2)
            {
            }
        }

        [Command("hello")]
        public void HelloCommandHandler(ICommandContext context)
        {
        }
    }
}
