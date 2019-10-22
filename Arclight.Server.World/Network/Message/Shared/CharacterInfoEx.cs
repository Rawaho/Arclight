using System.IO;
using Arclight.Shared.Network.Message;
using Arclight.Shared.Network.Message.Shared;

namespace Arclight.Server.World.Network.Message.Shared
{
    public class CharacterInfoEx : IWritable
    {
        public CharacterInfo Character { get; set; }
        public WorldPosition Position { get; set; }
        public float Unknown2E0 { get; set; }
        public float Unknown2E4 { get; set; }

        public void Write(BinaryWriter writer)
        {
            Character.Write(writer);
            Position.Write(writer);

            writer.Write(Unknown2E0);
            writer.Write(Unknown2E4);
        }
    }
}
