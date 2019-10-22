using System.Collections.Generic;
using System.IO;
using Arclight.Shared.Network.Message;
using Arclight.Shared.Network.Message.Shared;

namespace Arclight.Server.Character.Network.Message
{
    [Message(MessageOpcode.ServerCharacterListRes)]
    public class ServerCharacterListRes : IWritable
    {
        public List<CharacterInfo> Characters { get; set; } = new List<CharacterInfo>();
        public uint SelectedCharacterId { get; set; }
        public byte Unknown { get; set; } // CharacterSelectPanel_cl + 0xA0
        public byte Unknown2 { get; set; } // InventoryDialog_cl + 0x13B8
        public ulong InitialiseTime { get; set; }
        public byte Unknown3 { get; set; }
        public ulong ResetTime { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.Write((byte)Characters.Count);

            foreach (CharacterInfo character in Characters)
                character.Write(writer);

            writer.Write(SelectedCharacterId);

            writer.Write(Unknown);
            writer.Write(Unknown2);
            writer.Write(InitialiseTime);
            writer.Write(Unknown3);
            writer.Write(ResetTime);
        }
    }
}
