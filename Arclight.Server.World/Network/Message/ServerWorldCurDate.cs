using System.IO;
using Arclight.Shared.Network.Message;

namespace Arclight.Server.World.Network.Message
{
    [Message(MessageOpcode.ServerWorldCurDate)]
    public class ServerWorldCurDate : IWritable
    {
        public ulong Timestamp { get; set; }
        public ushort Year { get; set; }
        public ushort Month { get; set; }
        public ushort Day { get; set; }
        public ushort Hour { get; set; }
        public ushort Minute { get; set; }
        public ushort Second { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Timestamp);
            writer.Write(Year);
            writer.Write(Month);
            writer.Write(Day);
            writer.Write(Hour);
            writer.Write(Minute);
            writer.Write(Second);
            writer.Write((ushort)0);
        }
    }
}
