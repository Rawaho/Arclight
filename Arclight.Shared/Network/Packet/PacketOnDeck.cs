namespace Arclight.Shared.Network.Packet
{
    public class PacketOnDeck
    {
        public PacketHeader Header { get; }
        public FragmentedBuffer Buffer { get; }

        public PacketOnDeck(PacketHeader header)
        {
            Header = header;
            Buffer = new FragmentedBuffer(header.Length - PacketHeader.Size);
        }
    }
}
