using System;
using Arclight.Database.Character.Model;
using Arclight.Shared.Network;

namespace Arclight.Server.World.Network
{
    public class WorldSession : Session
    {
        public CharacterModel Character { get; private set; }

        /// <summary>
        /// Initialise <see cref="WorldSession"/> with a new <see cref="CharacterModel"/>.
        /// </summary>
        public void Accept(CharacterModel character)
        {
            if (Character != null)
                throw new InvalidOperationException();

            Character = character;
        }
    }
}
