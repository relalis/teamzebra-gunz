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

using System.Collections;
using System.Collections.Generic;

using Zebra.DatabaseInteraction;
using Zebra.Miscellaneous;

namespace Zebra.DataStructures
{
    /// <summary>
    /// Class to represent a lobby in the game.
    /// </summary>
    public class Lobby
    {
        /// <summary>
        /// the name of this channel
        /// </summary>
        public string name;

        /// <summary>
        /// max number of players
        /// </summary>
        public uint maxPlayers;

        /// <summary>
        /// the rule set for this channel
        /// </summary>
        public string rule;

        /// <summary>
        /// minimum level allowed in this channel
        /// </summary>
        public byte minLevel;

        /// <summary>
        /// maximum level allowed in this channel
        /// </summary>
        public byte maxLevel;

        /// <summary>
        /// the type of this channel
        /// </summary>
        public ChannelType type;

        /// <summary>
        /// this channel's UID
        /// </summary>
        public UID uid;

        /// <summary>
        /// the list of players in this channel
        /// </summary>
        public List<Client> players;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Lobby()
        {
            this.name = "Default Channel";
            this.maxPlayers = 100;
            this.rule = "novice";
            this.minLevel = 0;
            this.maxLevel = 255;
            this.type = ChannelType.general;
            this.uid.first = 0;
            this.uid.second = 0;
            players = new List<Client>();
        }

        /// <summary>
        /// Creates the global channel list for the server.
        /// </summary>
        /// <returns>the channel list</returns>
        public static bool createGlobalChannelList()
        {
            try
            {
                ArrayList channelNames = StaticSQL.executeScalarArray("SELECT Name FROM Channels");

                foreach(string s in channelNames)
                {
                    Lobby newLobby = new Lobby();

                    newLobby.name = s;
                    newLobby.maxPlayers = (uint)StaticSQL.executeScalarInt("SELECT MaxPlayers FROM Channels " +
                        "WHERE Name = '" + s + "'");
                    newLobby.rule = StaticSQL.executeScalarString("SELECT [Rule] FROM Channels " +
                        "WHERE Name = '" + s + "'");
                    newLobby.minLevel = StaticSQL.executeScalarByte("SELECT MinLevel FROM Channels " +
                        "WHERE Name = '" + s + "'");
                    newLobby.maxLevel = StaticSQL.executeScalarByte("SELECT MaxLevel FROM Channels " +
                        "WHERE Name = '" + s + "'");
                    newLobby.uid.second = (uint)StaticSQL.executeScalarInt("SELECT UID FROM Channels " +
                        "WHERE Name = '" + s + "'");

                    Globals.channels.Add(newLobby);
                }

                System.Console.WriteLine("   Channel list initialized successfully!");
                return true;
            }
            catch
            {
                System.Console.WriteLine("   Error in intializing channel list!");
                return false;
            }
        }

        /// <summary>
        /// Returns a lobby based on its UID.
        /// </summary>
        /// <param name="uid">the UID of the lobby</param>
        /// <returns>the lobby with the given UID</returns>
        public static Lobby getLobbyByUID(UID uid)
        {
            foreach (Lobby l in Globals.channels)
            {
                if (l.uid == uid)
                {
                    return l;
                }
            }

            return null;
        }

        /// <summary>
        /// Adds a player to this channel.
        /// </summary>
        /// <param name="c">player to add</param>
        /// <returns>whether the add was successful</returns>
        public bool addClient(Client c)
        {
            if (players.Count < maxPlayers)
            {
                if (c.account.currentCharacter.level >= minLevel &&
                   c.account.currentCharacter.level <= maxLevel)
                {
                    // first remove the client from any lobby it might already be in
                    foreach (Lobby l in Globals.channels)
                        l.removeClient(c);

                    // now add it to this lobby
                    players.Add(c);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a player from this channel
        /// </summary>
        /// <param name="c">the player to remove</param>
        /// <returns>whether the remove was successful</returns>
        public bool removeClient(Client c)
        {
            return players.Remove(c);
        }

        /// <summary>
        /// Send a packet to all players in this lobby
        /// </summary>
        /// <param name="p">the packet to send</param>
        public void sendToAllPlayers(GunzPacket p)
        {
            foreach (Client c in players)
                c.send(p);
        }
    }
}
