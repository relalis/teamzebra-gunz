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
    /// Contains the functions for all the Quest.___
    /// packets.
    /// </summary>
    public static class Quest
    {
        /// <summary>
        /// Sends the quest item list to the given client.
        /// </summary>
        /// <param name="c">the client that's requesting</param>
        /// <param name="p">the request packet</param>
        public static void responseQuestItem(Client c, GunzPacket p)
        {
            // send a blank list to everyone for now
            GunzPacket response = new GunzPacket(0x64, 0x5209);
            response.startHeader();
            response.writeInt(0x08);
            response.writeInt(0x04);
            response.writeInt(0x00);
            response.finishHeader();
            c.send(response);
        }
    }
}
