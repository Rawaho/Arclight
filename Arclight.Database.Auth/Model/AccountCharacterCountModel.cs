namespace Arclight.Database.Auth.Model
{
    public class AccountCharacterCountModel
    {
        public uint Id { get; set; }
        public uint ServerId { get; set; }
        public byte Count { get; set; }

        public AccountModel Account { get; set; }
        public ServerModel Server { get; set; }
    }
}
