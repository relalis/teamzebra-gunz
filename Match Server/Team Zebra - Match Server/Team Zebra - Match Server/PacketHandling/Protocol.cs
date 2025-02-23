/*
 * "Team Zebra - Match Server" - An open-source server emulator
 * for the free, online third-person-shooter "Gunz: The Duel".
 * 
 * This project is in no way affiliated with MAIET Entertainment, Inc., 
 * ijji, NHN Inc., LevelUp! Games, or any previous or future "Gunz: The Duel"
 * publishers. All trademarks, copyrights, etc. belong to their respective 
 * owners. This project contains no code from any of the afforementioned 
 * companies.
 * 
 * Copyright 2009 Team Zebra
 * Contact at <ZebraForceFive@gmail.com>
 * 
 * This file is part of "Team Zebra - Match Server".
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

namespace Zebra.PacketHandling
{
    /// <summary>
    /// Enumeration of the entire Gunz protocol
    /// and each command's opcode.
    /// </summary>
    public enum Protocol : ushort
    {
        LOCAL_INFO = 0xC9,
        LOCAL_ECHO = 0xCA,
        LOCAL_LOGIN = 0xCB,
        VERSION = 0x01,
        DEBUGTEST = 0x64,
        NET_ENUM = 0x12D,
        NET_CONNECT = 0x12E,
        NET_DISCONNECT = 0x12F,
        NET_CLEAR = 0x130,
        NET_CHECKPING = 0x141,
        NET_PING = 0x142,
        NET_PONG = 0x143,
        HSHIELD_PING = 0x144,
        HSHIELD_PONG = 0x145,
        NET_ONCONNECT = 0x137,
        NET_ONDISCONNECT = 0x138,
        NET_ONERROR = 0x139,
        NET_CONNECTTOZONESERVER = 0x14B,
        NET_REQUESTINFO = 0x155,
        NET_RESPONSEINFO = 0x156,
        NET_ECHO = 0x15F,
        MATCH_ANNOUNCE = 0x192,
        CLOCK_SYNCHRONIZE = 0x169,
        MATCH_LOGIN = 0x3E9,
        MATCH_RESPONSELOGIN = 0x3EA,
        MATCH_RESPONSE_RESULT = 0x193,
        MATCH_LOGINNETMARBLE = 0x3EB,
        MATCH_LOGINNETMARBLEJP = 0x3EC,
        MATCH_LOGINFROMDBAGENT = 0x3ED,
        MATCH_LOGINFAILEDFROMDBAGENT = 0x3F0,
        MATCH_FINH = 0x3F1,
        MC_MATCH_DISCONNMSG = 0x3F2,
        MC_MATCH_LOGIN_NHNUSA = 0x3F3,
        MATCH_OBJECTCACHE = 0x44D,
        MATCH_BRIDGEPEER = 0x3EE,
        MATCH_BRIDGEPEERACK = 0x3EF,
        MATCHSERVER_REQUESTRECOMMANDEDCHANNEL = 0x4B1,
        MATCHSERVER_RESPONSERECOMMANDEDCHANNEL = 0x4B2,
        CHANNEL_JOIN = 0x4B5,
        CHANNEL_RESPONSEJOIN = 0x4B7,
        CHANNEL_REQUESTJOINFROMNAME = 0x4B6,
        CHANNEL_LEAVE = 0x4B8,
        CHANNEL_LISTSTART = 0x4BB,
        CHANNEL_LISTSTOP = 0x4BC,
        CHANNEL_LIST = 0x4BD,
        CHANNEL_REQUEST_CHAT = 0x4C9,
        CHANNEL_CHAT = 0x4CA,
        CHANNEL_REQUEST_RULE = 0x4CE,
        CHANNEL_RESPONSE_RULE = 0x4CF,
        CHANNEL_REQUESTALLPLAYERLIST = 0x4D0,
        CHANNEL_RESPONSEALLPLAYERLIST = 0x4D1,
        STAGE_CREATE = 0x515,
        STAGE_REQUESTJOIN = 0x518,
        STAGE_REQUESTPRIVATEJOIN = 0x519,
        STAGE_JOIN = 0x517,
        STAGE_LEAVE = 0x51B,
        STAGE_REQUEST_PLAYERLIST = 0x51C,
        STAGE_FOLLOW = 0x51D,
        STAGE_RESPONSE_FOLLOW = 0x51E,
        STAGE_RESPONSEJOIN = 0x51A,
        STAGE_REQUIREPASSWORD = 0x534,
        REQUESTGAMEINFO = 0x5AB,
        RESPONSEGAMEINFO = 0x5AC,
        STAGE_RESPONSECREATE = 0x516,
        STAGE_REQUEST_ENTERBATTLE = 0x579,
        STAGE_ENTERBATTLE = 0x57A,
        STAGE_LEAVEBATTLE = 0x57B,
        STAGE_START = 0x597,
        STAGE_MAP = 0x586,
        STAGE_CHAT = 0x529,
        STAGE_REQUESTQUICKJOIN = 0x52A,
        STAGE_RESPONSEQUICKJOIN = 0x52B,
        STAGE_STAGEGO = 0x533,
        STAGE_STATE = 0x58E,
        STAGE_TEAM = 0x58F,
        STAGE_MASTER = 0x58D,
        STAGE_LISTSTART = 0x520,
        STAGE_LISTSTOP = 0x521,
        STAGE_LIST = 0x522,
        STAGE_REQUESTSTAGELIST = 0x51F,
        CHANNEL_REQUESTPLAYERLIST = 0x4C5,
        CHANNEL_RESPONSEPLAYERLIST = 0x4C6,
        STAGE_REQUESTSTAGESETTING = 0x583,
        STAGE_RESPONSESTAGESETTING = 0x584,
        STAGE_STAGESETTING = 0x585,
        STAGE_LAUNCH = 0x598,
        STAGE_FINISH = 0x5A2,
        STAGE_REQUESTPEERLIST = 0x5B5,
        STAGE_RESPONSEPEERLIST = 0x5B6,
        LOADING_COMPLETE = 0x5A1,
        MATCH_REQUESTPEERRELAY = 0x5BF,
        MATCH_RESPONSEPEERRELAY = 0x5C0,
        STAGE_ROUNDSTATE = 0x5DD,
        GAME_KILL = 0x5E7,
        GAME_REQUST_SPAWN = 0x5EB,
        GAME_RESPONSE_SPAWN = 0x5EC,
        GAME_LEVELUP = 0x5E9,
        GAME_LEVELDOWN = 0x5EA,
        GAME_DEAD = 0x5E8,
        GAME_TEAMBONUS = 0x5ED,
        GAME_REQUESTTIMESYNC = 0x5F1,
        GAME_RESPONSETIMESYNC = 0x5F2,
        GAME_REPORTTIMESYNC = 0x5F3,
        STAGE_REQUESTFORCEDENTRY = 0x587,
        STAGE_RESPONSEFORCEDENTRY = 0x588,
        STAGE_ROUNDFINISHINFO = 0x5DE,
        MATCH_NOTIFY = 0x191,
        MATCH_WHISPER = 0x641,
        MATCH_WHERE = 0x642,
        MATCH_USEROPTION = 0x645,
        CHATROOM_CREATE = 0x673,
        CHATROOM_JOIN = 0x674,
        CHATROOM_LEAVE = 0x675,
        CHATROOM_SELECTWRITE = 0x681,
        CHATROOM_INVITE = 0x67D,
        CHATROOM_CHAT = 0x67E,
        MATCH_REQUESTACCOUNTCHARLIST = 0x6A5,
        MATCH_RESPONSEACCOUNTCHARLIST = 0x6A6,
        MATCH_REQUESTACCOUNTCHARINFO = 0x6B7,
        MATCH_RESPONSEACCOUNTCHARINFO = 0x6B8,
        MATCH_REQUESTSELECTCHAR = 0x6A7,
        MATCH_RESPONSESELECTCHAR = 0x6A8,
        MATCH_REQUESTCHARINFO = 0x6A9,
        MATCH_RESPONSECHARINFO = 0x6AA,
        MATCH_REQUESTDELETECHAR = 0x6B1,
        MATCH_RESPONSEDELETECHAR = 0x6B2,
        MATCH_REQUESTCREATECHAR = 0x6AF,
        MATCH_RESPONSECREATECHAR = 0x6B0,
        MATCH_REQUESTCOPYTOTESTSERVER = 0x6B3,
        MATCH_RESPONSECOPYTOTESTSERVER = 0x6B4,
        MATCH_REQUESTCHARINFODETAIL = 0x6B5,
        MATCH_RESPONSECHARINFODETAIL = 0x6B6,
        MATCH_REQUESTSIMPLECHARINFO = 0x709,
        MATCH_RESPONSESIMPLECHARINFO = 0x70A,
        MATCH_REQUESTMYSIMPLECHARINFO = 0x70B,
        MATCH_RESPONSEMYSIMPLECHARINFO = 0x70C,
        MATCH_REQUESTBUYITEM = 0x713,
        MATCH_RESPONSEBUYITEM = 0x714,
        MATCH_REQUESTSELLITEM = 0x715,
        MATCH_RESPONSESELLITEM = 0x716,
        MATCH_REQUESTSHOPITEMLIST = 0x717,
        MATCH_RESPONSESHOPITEMLIST = 0x718,
        MATCH_REQUESTCHARACTERITEMLIST = 0x71D,
        MATCH_RESPONSECHARACTERITEMLIST = 0x71E,
        MATCH_REQUESTEQUIPITEM = 0x71F,
        MATCH_RESPONSEEQUIPITEM = 0x720,
        MATCH_REQUESTTAKEOFFITEM = 0x721,
        MATCH_RESPONSETAKEOFFITEM = 0x722,
        MATCH_REQUESTACCOUNTITEMLIST = 0x727,
        MATCH_RESPONSEACCOUNTITEMLIST = 0x728,
        MATCH_REQUESTBRINGACCOUNTITEM = 0x729,
        MATCH_RESPONSEBRINGACCOUNTITEM = 0x72A,
        MATCH_REQUESTBRINGBACKACCOUNTITEM = 0x72B,
        MATCH_RESPONSEBRINGBACKACCOUNTITEM = 0x72C,
        MATCH_EXPIREDRENTITEM = 0x72D,
        MATCH_ITEMGAMBLE = 0x731,
        MATCH_GAMBLERESULTITEM = 0x732,
        MATCH_REQUEST_SUICIDE = 0x5FB,
        MATCH_RESPONSE_SUICIDE = 0x5FC,
        MATCH_RESPONSE_SUICIDERESERVE = 0x5FD,
        MATCH_REQUEST_OBTAIN_WORLDITEM = 0x605,
        MATCH_WORLDITEM_OBTAIN = 0x606,
        MATCH_WORLDITEM_SPAWN = 0x607,
        MATCH_REQUEST_SPAWN_WORLDITEM = 0x608,
        MATCH_RESET_TEAMMEMBERS = 0x610,
        MATCH_ASSIGN_COMMANDER = 0x60F,
        MATCH_SET_OBSERVER = 0x611,
        MATCH_LADDER_REQUEST_CHALLENGE = 0x623,
        MATCH_LADDER_RESPONSE_CHALLENGE = 0x624,
        MATCH_LADDER_SEARCHRIVAL = 0x626,
        MATCH_LADDER_REQUEST_CANCELCHALLENGE = 0x627,
        MATCH_LADDER_CANCELCHALLENGE = 0x628,
        LADDER_PREPARE = 0x62A,
        LADDER_LAUNCH = 0x62B,
        MATCH_REQUESTPROPOSAL = 0x619,
        MATCH_RESPONSEPROPOSAL = 0x61A,
        MATCH_ASKAGREEMENT = 0x61B,
        MATCH_REPLYAGREEMENT = 0x61C,
        MATCH_FRIEND_ADD = 0x76D,
        MATCH_FRIEND_REMOVE = 0x76E,
        MATCH_FRIEND_LIST = 0x76F,
        MATCH_RESPONSE_FRIENDLIST = 0x770,
        MATCH_FRIEND_MSG = 0x771,
        MATCH_CLAN_REQUESTCREATECLAN = 0x7D0,
        MATCH_CLAN_RESPONSECREATECLAN = 0x7D1,
        MATCH_CLAN_ASKSPONSORAGREEMENT = 0x7D2,
        MATCH_CLAN_ANSWERSPONSORAGREEMENT = 0x7D3,
        MATCH_CLAN_REQUESTAGREEDCREATECLAN = 0x7D4,
        MATCH_CLAN_AGREEDRESPONSECREATECLAN = 0x7D5,
        MATCH_CLAN_REQUESTCLOSECLAN = 0x7D6,
        MATCH_CLAN_RESPONSECLOSECLAN = 0x7D7,
        MATCH_CLAN_REQUESTJOINCLAN = 0x7D8,
        MATCH_CLAN_RESPONSEJOINCLAN = 0x7D9,
        MATCH_CLAN_ASKJOINAGREEMENT = 0x7DA,
        MATCH_CLAN_ANSWERJOINAGREEMENT = 0x7DB,
        MATCH_CLAN_REQUESTAGREEDJOINCLAN = 0x7DC,
        MATCH_CLAN_RESPONSEAGREEDJOINCLAN = 0x7DD,
        MATCH_CLAN_REQUESTLEAVECLAN = 0x7DE,
        MATCH_CLAN_RESPONSELEAVECLAN = 0x7DF,
        MATCH_CLAN_UPDATECHARCLANINFO = 0x7E0,
        MATCH_CLAN_MASTER_REQUESTCHANGEGRADE = 0x7E1,
        MATCH_CLAN_MASTER_RESPONSECHANGEGRADE = 0x7E2,
        MATCH_CLAN_ADMIN_REQUESTEXPELMEMBER = 0x7E3,
        MATCH_CLAN_ADMIN_RESPONSELEAVEMEMBER = 0x7E4,
        MATCH_CLAN_REQUEST_MSG = 0x7E5,
        MATCH_CLAN_MSG = 0x7E6,
        MATCH_CLAN_REQUEST_CLANMEMBERLIST = 0x7E7,
        MATCH_CLAN_RESPONSE_CLANMEMBERLIST = 0x7E8,
        MATCH_CLAN_REQUEST_CLAN_INFO = 0x7E9,
        MATCH_CLAN_RESPONSE_CLAN_INFO = 0x7EA,
        MATCH_CLAN_STANDBY_CLANLIST = 0x7EB,
        MATCH_CLAN_MEMBER_CONNECTED = 0x7EC,
        MATCH_CLAN_REQUEST_EMBLEMURL = 0x803,
        MATCH_CLAN_RESPONSE_EMBLEMURL = 0x804,
        MATCH_CLAN_LOCAL_EMBLEMREADY = 0x807,
        MC_MATCH_CLAN_ACCOUNCE_DELETE = 0x808,
        MATCH_CALLVOTE = 0x834,
        MATCH_NOTIFYCALLVOTE = 0x835,
        MATCH_NOTIFYVOTERESULT = 0x836,
        MATCH_VOTEYES = 0x839,
        MATCH_VOTENO = 0x83A,
        VOTE_STOP = 0x83C,
        MATCH_BROADCAST_CLANRENEWVICTORIES = 0x898,
        MATCH_BROADCAST_CLANINTERRUPTVICTORIES = 0x899,
        MATCH_BROADCAST_DUELRENEWVICTORIES = 0x89A,
        MATCH_BROADCAST_DUELINTERRUPTVICTORIES = 0x89B,
        MATCH_ASSIGN_BERSERKER = 0xBB9,
        MATCH_DUEL_QUEUE_INFO = 0xC1C,
        MATCH_QUEST_PING = 0x177C,
        MATCH_QUEST_PONG = 0x177D,
        EVENT_CHANGEMASTER = 0x259,
        EVENT_CHANGEPASSWORD = 0x25A,
        EVENT_REQUESTJJANG = 0x263,
        EVENT_REMOVEJJANG = 0x264,
        EVENT_UPDATEJJANG = 0x265,
        QUEST_NPCSPAWN = 0x1770,
        QUEST_ENTRUST_NPC_CONTROL = 0x1771,
        QUEST_CHECKSUM_NPCINFO = 0x1772,
        QUEST_REQUEST_NPCDEAD = 0x1773,
        QUEST_NPCDEAD = 0x1774,
        QUEST_REFRESHPLAYERSTATUS = 0x1775,
        QUEST_NPC_ALLCLEAR = 0x1776,
        QUEST_ROUND_START = 0x17D4,
        QUEST_REQUESTDEAD = 0x1777,
        QUEST_PLAYERDEAD = 0x1778,
        QUEST_OBTAINQUESTITEM = 0x1779,
        QUEST_OBTAINZITEM = 0x177B,
        QUEST_STATE_MAPSET = 0x177A,
        QUEST_STAGE_GAMEINFO = 0x17AD,
        QUEST_SECTORBONUS = 0x17AE,
        QUEST_GAMEINFO = 0x17A3,
        QUEST_COMBAT_STATE = 0x17A4,
        QUEST_SECTOR_START = 0x17A5,
        QUEST_COMPLETE = 0x17A6,
        QUEST = 0x17A7,
        QUEST2 = 0x5208,
        QUEST3 = 0x5209,
        QUEST4 = 0x520A,
        QUEST5 = 0x520C,
        QUEST6 = 0x520D,
        QUEST7 = 0x520E,
        QUEST8 = 0x520F,
        QUEST9 = 0x5210,
        QUEST10 = 0x5211,
        QUEST11 = 0x5212,
        QUEST12 = 0x5213,
        QUEST13 = 0x5214,
        QUEST14 = 0x17D5,
        QUEST15 = 0x17D6,
        QUEST16 = 0x17AC,
        QUEST17 = 0x5216,
        QUEST18 = 0x5217,
        MONSTER = 0x5215,
        QUEST_REQUEST_MOVETO_PORTAL = 0x17A9,
        QUEST_MOVETO_PORTAL = 0x17AA,
        QUEST_READYTO_NEWSECTOR = 0x17AB,
        QUEST_PEER_NPC_BASICINFO = 0x1798,
        QUEST_PEER_NPC_HPINFO = 0x1799,
        QUEST_PEER_NPC_ATTACK_MELEE = 0x179A,
        QUEST_PEER_NPC_ATTACK_RANGE = 0x179B,
        QUEST_PEER_NPC_SKILL_START = 0x179C,
        QUEST_PEER_NPC_SKILL_EXECUTE = 0x179D,
        QUEST_PEER_NPC_DEAD = 0x179E,
        QUEST_TEST_REQUESTNPCSPAWN = 0x1AF5,
        QUEST_TEST_CLEARNPC = 0x1AF6,
        QUEST_TEST_SECTORCLEAR = 0x1AF7,
        QUEST_TEST_FINISH = 0x1AF8,
        TEST_BIRDTEST1 = 0xEA61,
        TEST_PEERTEST_PING = 0xEA62,
        TEST_PEERTEST_PONG = 0xEA63,
        ADMIN_ANNOUNCE = 0x1F5,
        ADMIN_PINGTOALL = 0x209,
        ADMIN_REQUESTSERVERINFO = 0x1F9,
        ADMIN_RESPONSESERVERINFO = 0x1FA,
        ADMIN_HALT = 0x1FF,
        ADMIN_TERMINAL = 0x200,
        ADMIN_REQUESTUPDATEACCOUNTUGRADE = 0x201,
        ADMIN_RESPONSEUPDATEACCOUNTUGRADE = 0x202,
        ADMIN_REQUESTBANPLAYER = 0x203,
        ADMIN_RESPONSEBANPLAYER = 0x204,
        ADMIN_REQUESTSWITCHLADDERGAME = 0x20A,
        ADMIN_HIDE = 0x213,
        ADMIN_RELOADCLIENTHASH = 0x214,
        MC_ADMIN_RESET_ALL_HACKING_BLOCK = 0x215,
        MC_ADMIN_RELOAD_GAMBLEITEM = 0x216,
        PEER_OPEN = 0x271B,
        PEER_MOVE = 0x2725,
        PEER_OBJECTCHANGEWEAPON = 0x2726,
        PEER_OBJECTCHANGEPARTS = 0x2727,
        PEER_OBJECTATTACK = 0x272F,
        PEER_OBJECTDAMAGE = 0x2730,
        PEER_CHAT = 0x2744,
        PEER_CHATICON = 0x2745,
        PEER_REACTION = 0x2746,
        PEER_ENCHANTDAMAGE = 0x2747,
        PEER_SHOT = 0x2732,
        PEER_SHOT_MELEE = 0x2735,
        PEER_SHOT_SP = 0x2733,
        PEER_RELOAD = 0x2731,
        PEER_OBJECTSPMOTION = 0x273E,
        PEER_CHANGECHARACTER = 0x271F,
        PEER_DIE = 0x2739,
        PEER_SPAWN = 0x273A,
        PEER_DASH = 0x273D,
        PEER_OBJECTSKILL = 0x2734,
        PEER_CHARACTERBASICINFO = 0x271C,
        PEER_CHARACTERHPINFO = 0x271D,
        PEER_CHARACTERHPAPINFO = 0x271E,
        PEER_UDPTEST = 0x2715,
        PEER_UDPTESTREPLY = 0x2716,
        PEER_PING = 0x2711,
        PEER_PONG = 0x2712,
        AGENT_LOCATETOCLIENT = 0x13C5,
        AGENT_RESPONSELOGIN = 0x13A0,
        AGENT_PEERBINDTCP = 0x13CF,
        AGENT_PEERBINDUDP = 0x13D0,
        AGENT_PEERUNBIND = 0x13D1,
        AGENT_ERROR = 0x1395,
        AGENT_TUNNELINGTCP = 0x13D9,
        AGENT_TUNNELINGUDP = 0x13DA,
        AGENT_ALLOWTUNNELINGTCP = 0x13DB,
        AGENT_ALLOWTUNNELINGUDP = 0x13DC,
        AGENT_DEBUGPING = 0x13ED,
        AGENT_DEBUGTEST = 0x13EE,
        ANNOUNCE = 0x1B59,
        SWITCH_CLAN_SERVER_STATUS = 0x1B5B,
        MC_MATCH_SCHEDULE_STOP_SERVER = 0x1B5D,
        TEST = 0x7919,
        MC_RESPONSE_KEEPER_MANAGER_CONNECT = 0x791E,
        MC_REQUEST_KEEPERMGR_ANNOUNCE = 0x792C,
        MC_REQUEST_KEEPER_ANNOUNCE = 0x792D,
        CHECK_PING = 0x791A,
        REQUEST_MATCHSERVER_STATUS = 0x791C,
        RESPONSE_MATCHSERVER_STSTUS = 0x791D,
        MC_REQUEST_DOWNLOAD_SERVER_PATCH_FILE = 0x7D18,
        MC_REQUEST_STOP_SERVER = 0x7D1C,
        MC_REQUEST_CONNECTION_STATE = 0x791F,
        MC_RESPONSE_CONNECTION_STATE = 0x7920,
        MC_REQUEST_SERVER_HEARBEAT = 0x7921,
        MC_RESPONSE_SERVER_HEARHEAT = 0x7922,
        MC_REQUEST_START_SERVER = 0x7D1A,
        MC_REQUEST_KEEPER_CONNECT_MATCHSERVER = 0x7923,
        MC_RESPONSE_KEEPER_CONNECT_MATCHSERVER = 0x7924,
        MC_REQUEST_REFRESH_SERVER = 0x7925,
        MC_REQUEST_PREPARE_SERVER_PATCH = 0x7D1E,
        MC_REQUEST_SERVER_PATCH = 0x7D20,
        MC_REQUEST_LAST_JOB_STATE = 0x7926,
        MC_RESPONSE_LAST_JOB_STATE = 0x7927,
        MC_REQUEST_CONFIG_STATE = 0x7928,
        MC_RESPONSE_CONFIG_STATE = 0x7929,
        MC_REQUEST_SET_ONE_CONFIG = 0x792A,
        MC_RESPONSE_SET_ONE_CONFIG = 0x792B,
        MC_REQUEST_STOP_AGENT_SERVER = 0x810A,
        MC_REQUEST_START_AGENT_SERVER = 0x810C,
        MC_REQUEST_DOWNLOAD_AGENT_PATCH_FILE = 0x810E,
        MC_REQUEST_PREPARE_AGENT_PATCH = 0x8110,
        MC_REQUEST_AGENT_PATCH = 0x8112,
        MC_REQUEST_RESET_PATCH = 0x792E,
        MC_REQUEST_DISCONNECT_SERVER = 0x792F,
        MC_REQUEST_REBOOT_WINDOWS = 0x7930,
        MC_REQUEST_ANNOUNCE_STOP_SERVER = 0x7931,
        MC_RESPONSE_ANNOUNCE_STOP_SERVER = 0x7932,
        MC_REQUEST_KEEPER_MANAGER_SCHEDULE = 0x84D1,
        MC_RESPONSE_KEEPER_MANAGER_SCHEDULE = 0x84D2,
        MC_REQUEST_SERVER_AGENT_STATE = 0x7933,
        MC_RESPONSE_SERVER_AGENT_STATE = 0x7934,
        MC_REQUEST_SERVER_STATUS = 0x8113,
        MC_RESPONSE_SERVER_STATUS = 0x8114,
        MC_REQUEST_START_SERVER_SCHEDULE = 0x84D3,
        MC_REQUEST_WRITE_CLIENT_CRC = 0x7935,
        MC_RESPONSE_WRITE_CLIENT_CRC = 0x7936,
        MC_REQUEST_KEEPER_RELOAD_SERVER_CONFIG = 0x7937,
        MC_REQUEST_RELOAD_CONFIG = 0x7938,
        MC_REQUEST_KEEPER_ADD_HASHMAP = 0x7939,
        MC_RESPONSE_KEEPER_ADD_HASHMAP = 0x793A,
        MC_REQUEST_ADD_HASHMAP = 0x793B,
        MC_RESPONSE_ADD_HASHMAP = 0x793C,
        MONSTER_INFO = 0x5215,
        MC_REQUEST_SERVER_LIST_INFO = 0x9C41,
        MC_RESPONSE_SERVER_LIST_INFO = 0x9C42,
        MC_RESPONSE_BLOCK_COUNTRY_CODE_IP = 0x9C43,
        MC_RESPONSE_BLOCK_COUNTRYCODE = 0xC351,
        MC_LOCAL_UPDATE_USE_COUNTRY_FILTER = 0xC352,
        MC_LOCAL_GET_DB_IP_TO_COUNTRY = 0xC353,
        MC_LOCAL_GET_DB_BLOCK_COUNTRY_CODE = 0xC354,
        MC_LOCAL_GET_DB_CUSTOM_IP = 0xC355,
        MC_LOCAL_UPDAET_IP_TO_COUNTRY = 0xC356,
        MC_LOCAL_UPDAET_BLOCK_COUTRYCODE = 0xC357,
        MC_LOCAL_UPDAET_CUSTOM_IP = 0xC358,
        MC_LOCAL_UPDATE_ACCEPT_INVALID_IP = 0xC359,
        MC_REQUEST_XTRAP_HASHVALUE = 0x1F41,
        MC_RESPONSE_XTRAP_HASHVALUE = 0x1F42,
        MC_REQUEST_XTRAP_DETECTCRACK = 0x1F45,
        MC_REQUEST_XTRAP_SEEDKEY = 0x1F43,
        MC_RESPONSE_XTRAP_SEEDKEY = 0x1F44,
        MC_RESPONSE_GAMBLEITEMLIST = 0x723,
        CON_CLEAR = 0xA028,
        CON_SIZE = 0xA029,
        CON_HIDE = 0xA02A,
        CON = 0xA02B,
        DIS = 0xA02C,
        T = 0xA410,
        BP = 0xA411,
        EP = 0xA412,
        CHANGESKIN = 0xC738,
        REPORT119 = 0xC739,
        MESSAGE = 0xC73A,
        LOCAL_EVENT_OPTAIN_SPECIAL_WORLDITEM = 0xCB21,
        QUEST_NPCLOCALSPAWN = 0xCF08
    }
}
