using System.IO;

namespace Arclight.Shared.Network.Message
{
    public interface IWritable
    {
        void Write(BinaryWriter writer);
    }
}
