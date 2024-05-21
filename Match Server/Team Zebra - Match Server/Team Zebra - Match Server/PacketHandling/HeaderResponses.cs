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

using Zebra.DataStructures;
using Zebra.PacketHandling.PacketCategories;

namespace Zebra.PacketHandling
{
    public static class HeaderResponses
    {
        /// <summary>
        /// Acts on a given packet based on its opcode.
        /// </summary>
        /// <param name="c">the client to respond to</param>
        /// <param name="p">the packet to respond to</param>
        public static void respondToHeader(Client c, GunzPacket p)
        {
            ushort commandID = p.readShort(8);

            switch ((Protocol)commandID)
            {
                case Protocol.MATCH_LOGIN:
                    MiscPackets.clockSynchronize(c);
                    Match.responseLogin(c, p);
                    break;
                case Protocol.MATCH_REQUESTACCOUNTCHARLIST:
                    Match.responseAccountCharList(c);
                    break;
                case Protocol.MATCH_REQUESTACCOUNTCHARINFO:
                    Match.responseAccountCharInfo(c, p);
                    break;
                case Protocol.MATCH_REQUESTSELECTCHAR:
                    Match.responseSelectChar(c, p);
                    break;
                case Protocol.MATCH_REQUESTCREATECHAR:
                    Match.responseCreateChar(c, p);
                    break;
                case Protocol.MATCH_REQUESTDELETECHAR:
                    Match.responseDeleteChar(c, p);
                    break;
                case Protocol.MATCHSERVER_REQUESTRECOMMANDEDCHANNEL:
                    MatchServer.responseRecommendedChannel(c);
                    break;
                case Protocol.CHANNEL_JOIN:
                    Channel.responseJoin(c, p);
                    break;
                case Protocol.CHANNEL_REQUEST_CHAT:
                    Channel.chat(c, p);
                    break;
                case Protocol.NET_PING:
                    Net.pong(c, p);
                    break;
                case Protocol.STAGE_STATE:
                    Stage.state(c, p);
                    break;
                case Protocol.MATCH_REQUESTSHOPITEMLIST:
                    Match.responseShopItemList(c, p);
                    break;
                case Protocol.MATCH_REQUESTCHARACTERITEMLIST:
                    Match.responseCharacterItemList(c, p);
                    break;
                case Protocol.MATCH_REQUESTBUYITEM:
                    Match.responseBuyItem(c, p);
                    break;
                case Protocol.MATCH_REQUESTSELLITEM:
                    Match.responseSellItem(c, p);
                    break;
                case Protocol.MATCH_REQUESTTAKEOFFITEM:
                    Match.responseTakeOffItem(c, p);
                    break;
                case Protocol.MATCH_REQUESTEQUIPITEM:
                    Match.responseEquipItem(c, p);
                    break;
                case Protocol.QUEST2:
                    Quest.responseQuestItem(c, p);
                    break;
                case Protocol.CHANNEL_REQUEST_RULE:
                    Channel.responseRule(c, p);
                    break;
                case Protocol.STAGE_REQUESTSTAGELIST:
                    Stage.list(c, p);
                    break;
                default:
                    break;
            }
        }
    }
}
