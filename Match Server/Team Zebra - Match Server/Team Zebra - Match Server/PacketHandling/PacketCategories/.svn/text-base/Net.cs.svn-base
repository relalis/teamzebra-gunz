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
    /// Contains all the functions for the
    /// Net.___ packets.
    /// </summary>
    public static class Net
    {
        /// <summary>
        /// Respond to a ping from a client.
        /// </summary>
        /// <param name="c">the client that pinged</param>
        /// <param name="p">the response to send</param>
        public static void pong(Client c, GunzPacket p)
        {
            GunzPacket response = new GunzPacket(0x64, 0x8E, 15);
            response.startHeader();

            p.setIndex(11);
            response.writeInt((uint)p.readInt());

            response.finishHeader();
            c.send(response);
        }
    }
}
