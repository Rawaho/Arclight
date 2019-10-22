using System;
using System.IO;

namespace Arclight.Shared.Network
{
    public class FragmentedBuffer
    {
        public bool IsComplete => data.Length == offset;

        public byte[] Data
        {
            get
            {
                if (!IsComplete)
                    throw new InvalidOperationException();
                return data;
            }
        }

        private uint offset;
        private readonly byte[] data;

        public FragmentedBuffer(uint size)
        {
            data = new byte[size];
        }

        public void Populate(BinaryReader reader)
        {
            if (IsComplete)
                throw new InvalidOperationException();

            int remaining = reader.BaseStream.Remaining();
            if (remaining < data.Length - offset)
            {
                // not enough data, push entire frame into packet
                byte[] newData = reader.ReadBytes(remaining);
                Buffer.BlockCopy(newData, 0, data, (int)offset, remaining);

                offset += (uint)newData.Length;
            }
            else
            {
                // enough data, push required frame into packet
                byte[] newData = reader.ReadBytes(data.Length - (int)offset);
                Buffer.BlockCopy(newData, 0, data, (int)offset, newData.Length);

                offset += (uint)newData.Length;
            }
        }
    }
}
