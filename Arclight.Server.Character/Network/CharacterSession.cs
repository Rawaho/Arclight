using System.Collections.Generic;
using System.Linq;
using Arclight.Database.Character.Model;
using Arclight.Server.Character.Network.Message;
using Arclight.Shared.Database;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message.Shared;

namespace Arclight.Server.Character.Network
{
    public class CharacterSession : Session
    {
        private List<CharacterModel> characters = new List<CharacterModel>();

        /// <summary>
        /// Return <see cref="CharacterModel"/> at supplied id.
        /// </summary>
        public CharacterModel GetCharacterById(uint id)
        {
            return characters.SingleOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Return <see cref="CharacterModel"/> at supplied index.
        /// </summary>
        public CharacterModel GetCharacterByIndex(byte index)
        {
            return characters.SingleOrDefault(c => c.Index == index);
        }

        /// <summary>
        /// Start tracking <see cref="CharacterModel"/> model, saving it to the database and sending <see cref="ServerCharacterListRes"/> to the client.
        /// </summary>
        public void CreateCharacter(CharacterModel character)
        {
            DatabaseManager.Instance.CharacterDatabase.CreateCharacter(character);
            characters.Add(character);

            SendCharacterList(character.Id);
        }

        /// <summary>
        /// Refresh tracked <see cref="CharacterModel"/> models from the database and sending <see cref="ServerCharacterListRes"/> to the client.
        /// </summary>
        public void RefreshCharacters()
        {
            characters = DatabaseManager.Instance.CharacterDatabase.GetCharacters(Account.Id);
            SendCharacterList(characters.Count > 0 ? characters[0].Id : 0);
        }

        private void SendCharacterList(uint selectedCharacterId)
        {
            SendMessage(new ServerCharacterListRes
            {
                Characters = characters.Select(c => new CharacterInfo
                {
                    Id   = c.Id,
                    Base =
                    {
                        Name       = c.Name,
                        Class      = c.Class,
                        Appearance = c.Appearance
                    },
                    Level = c.Level,
                    Index = c.Index
                    
                }).ToList(),
                SelectedCharacterId = selectedCharacterId
            });
        }
    }
}
