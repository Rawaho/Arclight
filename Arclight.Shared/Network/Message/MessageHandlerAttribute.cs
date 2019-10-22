using System;

namespace Arclight.Shared.Network.Message
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MessageHandlerAttribute : Attribute
    {
        public MessageOpcode Opcode { get; }
        public SessionState State { get; }

        public MessageHandlerAttribute(MessageOpcode opcode, SessionState state = SessionState.Authenticated)
        {
            Opcode = opcode;
            State  = state;
        }
    }
}
