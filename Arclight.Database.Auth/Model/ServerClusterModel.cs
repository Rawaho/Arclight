namespace Arclight.Database.Auth.Model
{
    public class ServerClusterModel
    {
        public uint Id { get; set; }
        public uint Index { get; set; }
        public string Host { get; set; }
        public ushort Port { get; set; }

        public ServerModel Server { get; set; }
    }
}
