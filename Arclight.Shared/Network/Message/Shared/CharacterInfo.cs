using System.Collections.Generic;
using System.IO;

namespace Arclight.Shared.Network.Message.Shared
{
    public class CharacterInfo : IReadable, IWritable
    {
        public class CharacterBase : IReadable, IWritable
        {
            public string Name { get; set; } = string.Empty;
            public byte Class { get; set; }
            public byte Unknown2B { get; set; }
            public uint Unknown2C { get; set; }
            public ulong Appearance { get; set; }
            public ulong Unknown38 { get; set; }

            public void Read(BinaryReader reader)
            {
                Name       = reader.ReadStringUtf16();
                Class      = reader.ReadByte();
                Unknown2B  = reader.ReadByte();
                Unknown2C  = reader.ReadUInt32();
                Appearance = reader.ReadUInt64();
                Unknown38  = reader.ReadUInt64();
            }

            public void Write(BinaryWriter writer)
            {
                writer.WriteStringUtf16(Name);
                writer.Write(Class);
                writer.Write(Unknown2B);
                writer.Write(Unknown2C);
                writer.Write(Appearance);
                writer.Write(Unknown38);
            }
        }

        public class UnknownStructure : IReadable, IWritable
        {
            public long Unknown0 { get; set; } = -1;
            public int Unknown8 { get; set; } = -1;
            public uint UnknownC { get; set; }
            public long Unknown10 { get; set; } = -1;
            public int Unknown14 { get; set; } = -1;
            public uint Unknown18 { get; set; }

            public void Read(BinaryReader reader)
            {
                Unknown0  = reader.ReadInt64();
                Unknown8  = reader.ReadInt32();
                UnknownC  = reader.ReadUInt32();
                Unknown10 = reader.ReadInt64();
                Unknown14 = reader.ReadInt32();
                Unknown18 = reader.ReadUInt32();
            }

            public void Write(BinaryWriter writer)
            {
                writer.Write(Unknown0);
                writer.Write(Unknown8);
                writer.Write(UnknownC);
                writer.Write(Unknown10);
                writer.Write(Unknown14);
                writer.Write(Unknown18);
            }
        }

        public class CharacterStats : IReadable, IWritable
        {
            public uint CurrentHealth { get; set; }
            public uint MaximumHealth { get; set; }
            public uint CurrentSoulForce { get; set; }
            public uint MaximumSoulForce { get; set; }
            public uint Unknown8 { get; set; }
            public uint Unknown1C { get; set; }
            public uint UnknownC { get; set; }
            public uint Stamina { get; set; }
            public uint Unknown10 { get; set; }
            public uint Unknown24 { get; set; }
            public float MovementSpeed { get; set; }
            public float AttackSpeed { get; set; }

            public void Read(BinaryReader reader)
            {
                CurrentHealth    = reader.ReadUInt32();
                MaximumHealth    = reader.ReadUInt32();
                CurrentSoulForce = reader.ReadUInt32();
                MaximumSoulForce = reader.ReadUInt32();
                Unknown8         = reader.ReadUInt32();
                Unknown1C        = reader.ReadUInt32();
                UnknownC         = reader.ReadUInt32();
                Stamina          = reader.ReadUInt32();
                Unknown10        = reader.ReadUInt32();
                Unknown24        = reader.ReadUInt32();
                MovementSpeed    = reader.ReadSingle();
                AttackSpeed      = reader.ReadSingle();
            }

            public void Write(BinaryWriter writer)
            {
                writer.Write(CurrentHealth);
                writer.Write(MaximumHealth);
                writer.Write(CurrentSoulForce);
                writer.Write(MaximumSoulForce);
                writer.Write(Unknown8);
                writer.Write(Unknown1C);
                writer.Write(UnknownC);
                writer.Write(Stamina);
                writer.Write(Unknown10);
                writer.Write(Unknown24);
                writer.Write(MovementSpeed);
                writer.Write(AttackSpeed);
            }
        }

        public class UnknownStructure2 : IReadable, IWritable
        {
            public uint Unknown0 { get; set; }
            public float Unknown4 { get; set; }
            public byte Unknown8 { get; set; }
            public uint UnknownC { get; set; }
            public byte Unknown10 { get; set; }

            public void Read(BinaryReader reader)
            {
                Unknown0  = reader.ReadUInt32();
                Unknown4  = reader.ReadSingle();
                Unknown8  = reader.ReadByte();
                UnknownC  = reader.ReadUInt32();
                Unknown10 = reader.ReadByte();
            }

            public void Write(BinaryWriter writer)
            {
                writer.Write(Unknown0);
                writer.Write(Unknown4);
                writer.Write(Unknown8);
                writer.Write(UnknownC);
                writer.Write(Unknown10);
            }
        }

        public uint Id { get; set; }
        public CharacterBase Base { get; set; } = new CharacterBase();
        public byte Level { get; set; }
        public byte Unknown55 { get; set; }
        public uint AccountId { get; set; }
        public byte Unknown23C { get; set; }
        public uint Unknown240 { get; set; }
        public byte Unknown8C { get; set; }
        public uint Unknown88 { get; set; }
        public byte Unknown94 { get; set; }
        public int Unknown90 { get; set; } = -1;
        public UnknownStructure[] Unknown9C { get; set; } = new UnknownStructure[13];
        public uint Unknown238 { get; set; }
        public uint Unknown244 { get; set; }
        public uint Unknown248 { get; set; }
        public uint Unknown24C { get; set; }
        public string Unknown250 { get; set; } = string.Empty;
        public uint Unknown264 { get; set; }
        public CharacterStats Stats { get; set; } = new CharacterStats();
        public byte Unknown265 { get; set; }
        public string Unknown268 { get; set; } = string.Empty;
        public ushort Energy { get; set; }
        public ushort BonusEnergy { get; set; }
        public ushort Unknown298 { get; set; }
        public byte Unknown299 { get; set; }
        public uint Unknown29C { get; set; }
        public byte Unknown2A0 { get; set; }
        public uint Unknown2A4 { get; set; }
        public List<UnknownStructure2> Unknown28A { get; set; } = new List<UnknownStructure2>();
        public byte Index { get; set; }
        public uint Unknown2BC { get; set; }

        public CharacterInfo()
        {
            for (var i = 0; i < Unknown9C.Length; i++)
                Unknown9C[i] = new UnknownStructure();
        }

        public void Read(BinaryReader reader)
        {
            Id = reader.ReadUInt32();

            Base.Read(reader);

            Level      = reader.ReadByte();
            Unknown55  = reader.ReadByte();
            AccountId  = reader.ReadUInt32();
            Unknown23C = reader.ReadByte();
            Unknown240 = reader.ReadUInt32();
            Unknown8C  = reader.ReadByte();
            Unknown88  = reader.ReadUInt32();
            Unknown94  = reader.ReadByte();
            Unknown90  = reader.ReadInt32();

            foreach (var unknown in Unknown9C)
                unknown.Read(reader);

            Unknown238 = reader.ReadUInt32();
            Unknown244 = reader.ReadUInt32();
            Unknown248 = reader.ReadUInt32();
            Unknown24C = reader.ReadUInt32();
            Unknown250 = reader.ReadStringUtf8();
            Unknown264 = reader.ReadUInt32();

            Stats.Read(reader);

            Unknown265  = reader.ReadByte();
            Unknown268  = reader.ReadStringUtf8();
            Energy      = reader.ReadUInt16();
            BonusEnergy = reader.ReadUInt16();
            Unknown298  = reader.ReadUInt16();
            Unknown299  = reader.ReadByte();
            Unknown29C  = reader.ReadUInt32();
            Unknown2A0  = reader.ReadByte();
            Unknown2A4  = reader.ReadUInt32();

            byte unknownCount = reader.ReadByte();
            for (int i = 0; i < unknownCount; i++)
            {
                var unknown = new UnknownStructure2();
                unknown.Read(reader);
                Unknown28A.Add(unknown);
            }

            Index      = reader.ReadByte();
            Unknown2BC = reader.ReadUInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Id);

            Base.Write(writer);

            writer.Write(Level);
            writer.Write(Unknown55);
            writer.Write(AccountId);
            writer.Write(Unknown23C);
            writer.Write(Unknown240);
            writer.Write(Unknown8C);
            writer.Write(Unknown88);
            writer.Write(Unknown94);
            writer.Write(Unknown90);

            foreach (var unknown in Unknown9C)
                unknown.Write(writer);

            writer.Write(Unknown238);
            writer.Write(Unknown244);
            writer.Write(Unknown248);
            writer.Write(Unknown24C);
            writer.WriteStringUtf8(Unknown250);
            writer.Write(Unknown264);

            Stats.Write(writer);

            writer.Write(Unknown265);
            writer.WriteStringUtf8(Unknown268);
            writer.Write(Energy);
            writer.Write(BonusEnergy);
            writer.Write(Unknown298);
            // Echelon/Rank related
            writer.Write(Unknown299);
            writer.Write(Unknown29C);
            writer.Write(Unknown2A0);
            writer.Write(Unknown2A4);

            writer.Write((byte)Unknown28A.Count);
            Unknown28A.ForEach(s => s.Write(writer));

            writer.Write(Index);
            writer.Write(Unknown2BC);
        }
    }
}
