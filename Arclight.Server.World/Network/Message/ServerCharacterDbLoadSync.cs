using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.World.Network.Message
{
    [Message(MessageOpcode.ServerCharacterDbLoadSync)]
    public class ServerCharacterDbLoadSync : IWritable
    {
        public void Write(BinaryWriter writer)
        {
        }
    }
}
