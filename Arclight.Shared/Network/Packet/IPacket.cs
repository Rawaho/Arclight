namespace Arclight.Shared.Network.Packet
{
    public interface IPacket
    { 
        PacketHeader Header { get; }
        byte[] Data { get; }
    }
}
