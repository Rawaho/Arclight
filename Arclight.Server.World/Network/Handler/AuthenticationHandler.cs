using System;
using System.Numerics;
using Arclight.Database.Auth.Model;
using Arclight.Database.Character.Model;
using Arclight.Shared.Network.Message;
using Arclight.Server.World.Network.Message;
using Arclight.Server.World.Network.Message.Shared;
using Arclight.Shared.Database;
using Arclight.Shared.Network;
using Arclight.Shared.Network.Message.Shared;

namespace Arclight.Server.World.Network.Handler
{
    public static class AuthenticationHandler
    {
        [MessageHandler(MessageOpcode.ClientEnterGameServerReq, SessionState.None)]
        public static void HandleEnterGameServerReq(WorldSession session, ClientEnterGameServerReq enterGameServerReq)
        {
            AccountModel account = DatabaseManager.Instance.AuthDatabase.GetAccount(
                enterGameServerReq.AccountId, enterGameServerReq.SessionKey);
            if (account == null)
                return;

            CharacterModel character = DatabaseManager.Instance.CharacterDatabase.GetCharacter(
                enterGameServerReq.AccountId, enterGameServerReq.CharacterId);
            if (character == null)
                return;

            session.Authenticate(account);
            session.Accept(character);

            DateTimeOffset now = DateTimeOffset.Now;
            session.SendMessage(new ServerWorldCurDate
            {
                Timestamp = (ulong)now.ToUnixTimeSeconds(),
                Year      = (ushort)now.Year,
                Month     = (ushort)now.Month,
                Day       = (ushort)now.Day,
                Hour      = (ushort)now.Hour,
                Minute    = (ushort)now.Minute,
                Second    = (ushort)now.Second
            });

            session.SendMessage(new ServerWorldVersion
            {
                Unknown2 = 1,
                Unknown3 = 0x0322,
                Unknown4 = 0x3BBB
            });

            // ServerEventDayEventBoosterList
            // 0x1753

            session.SendMessage(new ServerEnterGameServerRes
            {
                Result   = 1,
                Position = new WorldPosition
                {
                    MapId       = character.MapId,
                    Origin      = new Vector3(character.X, character.Y, character.Z),
                    Orientation = character.O
                }
            });
        }

        [MessageHandler(MessageOpcode.ClientCharacterInfoReq)]
        public static void HandleCharacterInfoReq(WorldSession session, ClientCharacterInfoReq characterInfoReq)
        {
            session.SendMessage(new ServerCharacterInfoRes
            {
                MyCharacterInfo = new ServerCharacterInfoRes.MyCharacterInfoEx
                {
                    CharacterInfo = new CharacterInfoEx
                    {
                        Character = new CharacterInfo
                        {
                            Id   = session.Character.Id,
                            Base =
                            {
                                Name       = session.Character.Name,
                                Class      = session.Character.Class,
                                Appearance = session.Character.Appearance
                            },
                            Stats =
                            {
                                MovementSpeed = 100f
                            },
                            Level     = session.Character.Level,
                            AccountId = session.Account.Id,
                            Index     = session.Character.Index
                        },
                        Position = new WorldPosition
                        {
                            MapId       = session.Character.MapId,
                            Origin      = new Vector3(session.Character.X, session.Character.Y, session.Character.Z),
                            Orientation = session.Character.O
                        }
                    }
                }
            });
        }
    }
}
