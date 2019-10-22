using System.IO;
using System.Numerics;

namespace Arclight.Shared.Network.Message.Shared
{
    public class WorldPosition : IWritable
    {
        public ushort MapId { get; set; }
        public ulong Unknown8 { get; set; }
        public Vector3 Origin { get; set; }
        public float Orientation { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.Write(MapId);
            writer.Write(Unknown8);
            writer.Write(Origin.X);
            writer.Write(Origin.Y);
            writer.Write(Origin.Z);
            writer.Write(Orientation);
        }
    }
}
