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

namespace Zebra.PacketHandling.PacketCategories
{
    /// <summary>
    /// Contains the functions for all the 
    /// Stage.___ packets.
    /// </summary>
    public static class Stage
    {
        /// <summary>
        /// Changes the state of the given client in the channel.
        /// </summary>
        /// <param name="c">the client to change the state of</param>
        /// <param name="p">the packet with the state to change to</param>
        public static void state(Client c, GunzPacket p)
        {
            p.setIndex(11);

            UID playerUID = new UID();
            playerUID.first = (uint)p.readInt();
            playerUID.second = (uint)p.readInt();

            if (playerUID != c.uid)
                return;

            p.setIndex(27);

            c.stageState = (uint)p.readInt();
        }

        /// <summary>
        /// Sends the stage list to the given client.
        /// </summary>
        /// <param name="c">the client to send to</param>
        /// <param name="p">the request packet</param>
        public static void list(Client c, GunzPacket p)
        {
            // send blank list for now
            GunzPacket response = new GunzPacket(0x64, 0x522);
            response.startHeader();
            response.writeShort(0x00);
            response.writeInt(0x08);
            response.writeInt(0x5A);
            response.writeInt(0x00);
        }

        /// <summary>
        /// Sends the stage list to the given client.
        /// </summary>
        /// <param name="c">the client to send to</param>
        /// <param name="uidChannel">the UID of the channel to get the list from</param>
        public static void list(Client c, UID uidChannel)
        {
            // send blank list for now
            GunzPacket response = new GunzPacket(0x64, 0x522);
            response.startHeader();
            response.writeShort(0x00);
            response.writeInt(0x08);
            response.writeInt(0x5A);
            response.writeInt(0x00);
        }
    }
}
