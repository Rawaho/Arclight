using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.World.Network.Message
{
    [Message(MessageOpcode.ServerCharacterLoadDistrictState)]
    public class ServerCharacterLoadDistrictState : IWritable
    {
        public void Write(BinaryWriter writer)
        {
            writer.Write((byte)1);
            {
                writer.Write(10003);
                writer.Write(8);
                writer.Write((byte)0);
            }
        }
    }
}
