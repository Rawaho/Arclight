using System.IO;

namespace Arclight.Shared.Network.Message
{
    public interface IReadable
    {
        void Read(BinaryReader reader);
    }
}
