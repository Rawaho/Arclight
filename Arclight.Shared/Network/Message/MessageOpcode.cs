namespace Arclight.Shared.Network.Message
{
    // Note: client treats these shorts as 2 separate bytes (first being category/group, the second being type)
    public enum MessageOpcode
    {
        None                             = 0x0000,
                                         
        BiSystemKeepAlive                = 0x0105,

        ClientLoginReq                   = 0x0201,
        ServerLoginResult                = 0x0202,
        ClientServerListReq              = 0x0203,
        ServerServerList                 = 0x0204,
        ClientServerConnectReq           = 0x0205,
        ServerEnterServer                = 0x0211,
        ClientEnterServerReq             = 0x0213,
        ServerEnterServerRes             = 0x0214,
        ClientLoginForGf                 = 0x0218,
        ClientPostRecvList               = 0x0232,
        ServerOptionLoad                 = 0x0231,

        ClientCreateCharacterReq         = 0x0301,
        ClientDeleteCharacterReq         = 0x0302,
        ClientCharacterListReq           = 0x0311,
        ServerCharacterListRes           = 0x0312,
        ClientSelectCharacterReq         = 0x0313,
        ServerSelectCharacterRes         = 0x0314,
        ClientEnterGameServerReq         = 0x0321,
        ServerEnterGameServerRes         = 0x0322,
        ServerCharacterDbLoadSync        = 0x0330,
        ClientCharacterInfoReq           = 0x0331,
        ServerCharacterInfoRes           = 0x0332,
        ServerCharacterLoadDistrictState = 0x0361,

        ServerWorldCurDate               = 0x0403,
        ServerWorldVersion               = 0x0404
    }
}
