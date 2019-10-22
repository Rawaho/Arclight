using System;
using System.Collections.Generic;

namespace Arclight.Database.Auth.Model
{
    public class AccountModel
    {
        public uint Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreateTime { get; set; }
        public ulong? SessionKey { get; set; }

        public HashSet<AccountCharacterCountModel> CharacterCounts { get; set; } = new HashSet<AccountCharacterCountModel>();
    }
}
