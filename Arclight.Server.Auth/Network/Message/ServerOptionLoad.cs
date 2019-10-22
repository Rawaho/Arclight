using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.Auth.Network.Message
{
    [Message(MessageOpcode.ServerOptionLoad)]
    public class ServerOptionLoad : IWritable
    {
        public void Write(BinaryWriter writer)
        {
            writer.Write(new byte[64]);

            for (int i = 0; i < 14; i++)
            {
                writer.Write((byte)0);
                writer.Write((byte)0);
            }
        }
    }
}
