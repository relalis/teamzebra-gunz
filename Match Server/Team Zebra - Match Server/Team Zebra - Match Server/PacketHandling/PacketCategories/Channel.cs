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

using Zebra.DatabaseInteraction;
using Zebra.DataStructures;
using Zebra.Miscellaneous;

namespace Zebra.PacketHandling.PacketCategories
{
    /// <summary>
    /// Contains the functions for all the
    /// Channel.___ packets.
    /// </summary>
    public static class Channel
    {
        /// <summary>
        /// Lets a user join a channel.
        /// </summary>
        /// <param name="c">the client that's joining</param>
        public static void responseJoin(Client c, GunzPacket p)
        {
            p.setIndex(11);
            UID uidPlayer = new UID();
            UID uidChannel = new UID();

            uidPlayer.first = (uint)p.readInt();
            uidPlayer.second = (uint)p.readInt();
            uidChannel.first = (uint)p.readInt();
            uidChannel.second = (uint)p.readInt();

            if (uidPlayer != c.uid)
                return;

            Lobby lobby = Lobby.getLobbyByUID(uidChannel);

            GunzPacket response = new GunzPacket(0x64, 0x4B7);
            response.startHeader();

            response.writeInt(uidChannel.first);
            response.writeInt(uidChannel.second);
            response.writeInt(0x00);
            response.writeString(lobby.name);
            response.writeByte(0x00);
            response.writeByte(0x01);

            response.finishHeader();
            c.send(response);
            lobby.addClient(c);

            responseRule(c, uidChannel);
            Stage.list(c, uidChannel);
        }

        /// <summary>
        /// Sends the channel rule info to the client.
        /// </summary>
        /// <param name="c">the client that's requesting</param>
        public static void responseRule(Client c, GunzPacket p)
        {
            p.setIndex(11);
            UID channelUID = new UID();
            channelUID.first = (uint)p.readInt();
            channelUID.second = (uint)p.readInt();

            GunzPacket response = new GunzPacket(0x64, 0x4CF);
            response.startHeader();

            response.writeInt(channelUID.first);
            response.writeInt(channelUID.second);
            response.writeString(Lobby.getLobbyByUID(channelUID).rule);
            response.writeByte(0x00);

            response.finishHeader();
            c.send(response);
        }

        /// <summary>
        /// Sends the channel rule info to the client.
        /// </summary>
        /// <param name="c">the client that's requesting</param>
        /// <param name="uidChannel">the channel to send the rules of</param>
        public static void responseRule(Client c, UID uidChannel)
        {
            GunzPacket response = new GunzPacket(0x64, 0x4CF);
            response.startHeader();

            response.writeInt(uidChannel.first);
            response.writeInt(uidChannel.second);
            response.writeString(Lobby.getLobbyByUID(uidChannel).rule);
            response.writeByte(0x00);

            response.finishHeader();
            c.send(response);
        }

        /// <summary>
        /// Handles the lobby chat packet.
        /// </summary>
        /// <param name="p">the packet with the chat</param>
        public static void chat(Client c, GunzPacket p)
        {
            p.setIndex(11);
            UID uidPlayer = new UID();
            UID uidChannel = new UID();
            string chat;
            uint type = (uint)(c.isAdmin() ? 255 : 0);

            uidPlayer.first = (uint)p.readInt();
            uidPlayer.second = (uint)p.readInt();
            uidChannel.first = (uint)p.readInt();
            uidChannel.second = (uint)p.readInt();
            chat = p.readStringFromLength();

            if (uidPlayer != c.uid)
                return;

            if (!handleCommand(c, chat, uidChannel))
            {
                GunzPacket response = new GunzPacket(0x64, 0x4CA);
                Lobby channel = Lobby.getLobbyByUID(uidChannel);

                response.startHeader();

                response.writeInt(uidChannel.first);
                response.writeInt(uidChannel.second);
                response.writeString(c.account.currentCharacter.name);
                response.writeByte(0x58); // ?
                response.writeString(chat);
                response.writeByte(0xF8); // ?
                response.writeInt(type);

                response.finishHeader();
                channel.sendToAllPlayers(response);
            }
        }

        /// <summary>
        /// Attempts to handle a chat command.
        /// </summary>
        /// <param name="c">the client sending the command</param>
        /// <param name="chat">the chat string to be handled</param>
        /// <param name="channelUID">the channel the command is coming from</param>
        /// <returns></returns>
        public static bool handleCommand(Client c, string chat, UID uidChannel)
        {
            GunzPacket response = new GunzPacket(0x64, 0x4CA);
            string responseMessage = "";
            bool isHandled;
            string[] tokens = chat.Split(new char[] { ' ' });

            switch (tokens[0].ToLower())
            {
                case "/info":
                    if (c.isAdmin())
                    {
                        responseMessage = "Commands: /ban <character name>, /unban <character name>, /addjjang <character name>, " +
                            "/removejjang <character name>";
                    }
                    else
                    {
                        responseMessage = "No custom commands implemented yet.";
                    }

                    isHandled = true;
                    break;
                case "/ban":
                    if(!c.isAdmin())
                    {
                        responseMessage = "Invalid command. For administrator use only.";
                    }
                    else if (tokens.Length != 2)
                    {
                        responseMessage = "Invalid command. Usage: /ban <character name>";
                    }
                    else
                    {
                        int AID = DynamicSQL.executeScalarInt("SELECT AID FROM Characters WHERE Name = '" + tokens[1] + "'");
                        int result = DynamicSQL.executeQuery("UPDATE Accounts SET UGradeID = 253 WHERE AID = " + AID);

                        if (result == 0)
                        {
                            responseMessage = "Character \"" + tokens[1] + "\" banned unsuccessfully.";
                        }
                        else
                        {
                            responseMessage = "Character \"" + tokens[1] + "\" banned successfully.";
                        }
                    }

                    isHandled = true;
                    break;
                case "/unban":
                    if (!c.isAdmin())
                    {
                        responseMessage = "Invalid command. For administrator use only.";
                    }
                    else if (tokens.Length != 2)
                    {
                        responseMessage = "Invalid command. Usage: /unban <character name>";
                    }
                    else
                    {
                        int AID = DynamicSQL.executeScalarInt("SELECT AID FROM Characters WHERE Name = '" + tokens[1] + "'");
                        int result = DynamicSQL.executeQuery("UPDATE Accounts SET UGradeID = 0 WHERE AID = " + AID);

                        if (result == 0)
                        {
                            responseMessage = "Character \"" + tokens[1] + "\" unbanned unsuccessfully.";
                        }
                        else
                        {
                            responseMessage = "Character \"" + tokens[1] + "\" unbanned successfully.";
                        }
                    }

                    isHandled = true;
                    break;
                case "/addjjang":
                    if (!c.isAdmin())
                    {
                        responseMessage = "Invalid command. For administrator use only.";
                    }
                    else if (tokens.Length != 2)
                    {
                        responseMessage = "Invalid command. Usage: /addjjang <character name>";
                    }
                    else
                    {
                        int AID = DynamicSQL.executeScalarInt("SELECT AID FROM Characters WHERE Name = '" + tokens[1] + "'");
                        int result = DynamicSQL.executeQuery("UPDATE Accounts SET UGradeID = 2 WHERE AID = " + AID);

                        if (result == 0)
                        {
                            responseMessage = "Character \"" + tokens[1] + "\" given Jjang unsuccessfully.";
                        }
                        else
                        {
                            responseMessage = "Character \"" + tokens[1] + "\" given Jjang successfully.";
                        }
                    }

                    isHandled = true;
                    break;
                case "/removejjang":
                    if (!c.isAdmin())
                    {
                        responseMessage = "Invalid command. For administrator use only.";
                    }
                    else if (tokens.Length != 2)
                    {
                        responseMessage = "Invalid command. Usage: /removejjang <character name>";
                    }
                    else
                    {
                        int AID = DynamicSQL.executeScalarInt("SELECT AID FROM Characters WHERE Name = '" + tokens[1] + "'");
                        int result = DynamicSQL.executeQuery("UPDATE Accounts SET UGradeID = 0 WHERE AID = " + AID);

                        if (result == 0)
                        {
                            responseMessage = "Character \"" + tokens[1] + "\" removed Jjang unsuccessfully.";
                        }
                        else
                        {
                            responseMessage = "Character \"" + tokens[1] + "\" removed Jjang successfully.";
                        }
                    }

                    isHandled = true;
                    break;
                default:
                    isHandled = false;
                    break;
            }

            if (isHandled)
            {
                response.startHeader();
                response.writeInt(uidChannel.first);
                response.writeInt(uidChannel.second);
                response.writeString("System Message");
                response.writeByte(0x58); // ?
                response.writeString(responseMessage);
                response.writeByte(0xF8); // ?
                response.writeInt(0x00);
                response.finishHeader();
                c.send(response);
            }

            return isHandled;
        }

        /// <summary>
        /// Sends the player list to the given client.
        /// </summary>
        /// <param name="c">the client to send the list to</param>
        /// <param name="p">the request packet</param>
        public static void responsePlayerList(Client c, GunzPacket p)
        {
            p.setIndex(11);

            UID uidPlayer = new UID();
            UID uidChannel = new UID();
            int page;
            System.Collections.Generic.List<Client> players;
            Lobby channel;

            uidPlayer.first = (uint)p.readInt();
            uidPlayer.second = (uint)p.readInt();
            uidChannel.first = (uint)p.readInt();
            uidChannel.second = (uint)p.readInt();
            page = p.readInt();

            if (uidPlayer != c.uid)
                return;

            channel = Lobby.getLobbyByUID(uidChannel);
            players = channel.players;
        }
    }
}
