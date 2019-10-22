namespace Arclight.Shared.Network.Packet
{
    public class ClientPacket : IPacket
    {
        public PacketHeader Header { get; }
        public byte[] Data { get; }

        public ClientPacket(PacketHeader header, byte[] data)
        {
            Header = header;
            Data   = data;
        }
    }
}
