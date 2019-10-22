using System;

namespace Arclight.Shared.Network.Message
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageAttribute : Attribute
    {
        public MessageOpcode Opcode { get; }

        public MessageAttribute(MessageOpcode opcode)
        {
            Opcode = opcode;
        }
    }
}
