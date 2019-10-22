using System;
using System.Numerics;
using Arclight.Database.Auth.Model;
using Arclight.Database.Character.Model;
using Arclight.Server.Character.Network.Message;
using Arclight.Shared.Configuration;
using Arclight.Shared.Game;
using Arclight.Shared.Network.Message;
using Arclight.Shared.Network.Message.Shared;

namespace Arclight.Server.Character.Network.Handler
{
    public class CharacterHandler
    {
        [MessageHandler(MessageOpcode.ClientCharacterListReq)]
        public static void HandleCharacterListReq(CharacterSession session, ClientCharacterListReq characterListReq)
        {
            session.RefreshCharacters();
        }

        [MessageHandler(MessageOpcode.ClientCreateCharacterReq)]
        public static void HandleCreateCharacterReq(CharacterSession session, ClientCreateCharacterReq createCharacterReq)
        {
            if (session.GetCharacterByIndex(createCharacterReq.Character.Index) != null)
                return;

            // TODO: validate character appearance

            CharacterInfo info = createCharacterReq.Character;
            var character = new CharacterModel
            {
                AccountId  = session.Account.Id,
                Index      = info.Index,
                Name       = info.Base.Name,
                Class      = info.Base.Class,
                Level      = info.Level,
                Appearance = info.Base.Appearance,

                // hardcoded for the time being
                MapId = 10003,
                X     = 10444.9951f,
                Y     = 10179.7461f,
                Z     = 100.325394f
            };

            try
            {
                session.CreateCharacter(character);
            }
            catch (Exception exception)
            {
                // TODO: send some error
            }
        }

        [MessageHandler(MessageOpcode.ClientDeleteCharacterReq)]
        public static void HandleDeleteCharacterReq(CharacterSession session, ClientDeleteCharacterReq deleteCharacterReq)
        {

        }

        [MessageHandler(MessageOpcode.ClientSelectCharacterReq)]
        public static void HandleSelectCharacterReq(CharacterSession session, ClientSelectCharacterReq selectCharacterReq)
        {
            CharacterModel character = session.GetCharacterById(selectCharacterReq.CharacterId);
            if (character == null)
                return;

            ServerClusterModel node = ServerManager.Instance.GetServerNode(
                ConfigurationManager<CharacterServerConfig>.Instance.Model.Server.Id);
            if (node == null)
                return;

            session.SendMessage(new ServerSelectCharacterRes
            {
                Unknown0 = new ServerSelectCharacterRes.UnknownStructure
                {
                    CharacterId = character.Id,
                    AccountId   = session.Account.Id,
                    Host        = node.Host,
                    Port        = node.Port,
                    Position    = new WorldPosition
                    {
                        MapId       = character.MapId,
                        Origin      = new Vector3(character.X, character.Y, character.Z),
                        Orientation = character.O
                    }
                }
            });
        }
    }
}
