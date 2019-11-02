using System.IO;

namespace Arclight.TableExtractor
{
    public class ArchiveXorStream : FileStream
    {
        private const byte XorByte = 0x55;

        public ArchiveXorStream(string path, FileMode mode)
            : base(path, mode)
        {
        }

        public override int ReadByte()
        {
            byte value = (byte)base.ReadByte();
            return value ^ XorByte;
        }

        public override int Read(byte[] array, int offset, int count)
        {
            var move = base.Read(array, offset, count);
            for (int i = offset; i < offset + count; i++)
                array[i] ^= XorByte;

            return move;
        }
    }
}
