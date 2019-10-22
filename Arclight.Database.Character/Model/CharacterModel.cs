using System;

namespace Arclight.Database.Character.Model
{
    public class CharacterModel
    {
        public uint Id { get; set; }
        public uint AccountId { get; set; }
        public byte Index { get; set; }
        public string Name { get; set; }
        public byte Class { get; set; }
        public byte Level { get; set; }
        public ulong Appearance { get; set; }
        public ushort MapId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float O { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
