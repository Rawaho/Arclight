using System.IO;

namespace Arclight.Shared.Cryptography
{
    public class PacketCryptoStream : MemoryStream
    {
        private static readonly byte[] key =
        {
            0x57, 0x19, 0xC6, 0x2D, 0x56, 0x68, 0x3A, 0xCC,
            0x60, 0x3B, 0x0B, 0xB1, 0x90, 0x5C, 0x4A, 0xF8,
            0x80, 0x28, 0xB1, 0x45, 0xB6, 0x85, 0xE7, 0x4C,
            0x06, 0x2D, 0x55, 0x83, 0xAF, 0x44, 0x99, 0x95,
            0xD9, 0x98, 0xBF, 0xAE, 0x53, 0x43, 0x63, 0xC8,
            0x4A, 0x71, 0x80, 0x9D, 0x0B, 0xA1, 0x70, 0x8A,
            0x0F, 0x54, 0x9C, 0x1B, 0x06, 0xC0, 0xEA, 0x3C,
            0xC0, 0x88, 0x71, 0x48, 0xB3, 0xB9, 0x45, 0x78,
            0xFF, 0xFF, 0xFF, 0xFF, 0x7F, 0x7F, 0x7F, 0xFF,
            0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0xFF, 0xFF,
            0x00, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0x00, 0xFF,
            0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF,
            0xFF, 0x00, 0xFF, 0xFF
        };


        private readonly ushort seed;

        public PacketCryptoStream(ushort seed)
        {
            this.seed = seed;
        }

        public PacketCryptoStream(ushort seed, byte[] buffer)
            : base(buffer)
        {
            this.seed = seed;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var move = base.Read(buffer, offset, count);
            Xor(buffer, offset, count, Position - move);
            return move;
        }

        public override int ReadByte()
        {
            byte value = (byte)base.ReadByte();
            Xor(ref value, Position - sizeof(byte));
            return value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Xor(buffer, offset, count, Position);
            base.Write(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            Xor(ref value, Position);
            base.WriteByte(value);
        }

        private void Xor(byte[] buffer, int offset, int count, long position)
        {
            for (int i = 0; i < count - offset; i++)
                buffer[i + offset] ^= key[4 * seed + (position + i) % 3];
        }

        private void Xor(ref byte value, long position)
        {
            value ^= key[4 * seed + position % 3];
        }
    }
}
