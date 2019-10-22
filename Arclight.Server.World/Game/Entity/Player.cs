using Arclight.Server.World.Network;

namespace Arclight.Server.World.Game.Entity
{
    public class Player
    {
        public WorldSession Session { get; }

        public Player(WorldSession session)
        {
            Session = session;
        }

        public void SendPacketsOnAddToMap()
        {

        }
    }
}
