using System;
using System.Runtime.InteropServices;

namespace Arclight.Shared
{
    public static class Extensions
    {
        public static byte[] Serialise(this object obj)
        {
            GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            try
            {
                byte[] payload = new byte[Marshal.SizeOf(obj.GetType())];
                Marshal.Copy(handle.AddrOfPinnedObject(), payload, 0, payload.Length);
                return payload;
            }
            finally
            {
                handle.Free();
            }
        }

        /*public static T DeSerialise<T>(this ReadOnlySpan<byte> data) where T : new()
        {
            unsafe
            {
                fixed (byte* ptr = &data.GetPinnableReference())
                    return (T)Marshal.PtrToStructure(new IntPtr(ptr), typeof(T));
            }
        }*/

        public static T DeSerialise<T>(this byte[] data) where T : new()
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }
    }
}
