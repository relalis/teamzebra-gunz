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
    /// Contains misc. packets that don't have enough
    /// to have their own category.
    /// </summary>
    public static class MiscPackets
    {
        /// <summary>
        /// Sends the Clock.Synchronize packet to the
        /// given client.
        /// </summary>
        /// <param name="c">the client to send to</param>
        public static void clockSynchronize(Client c)
        {
            GunzPacket p = new GunzPacket(0x64, 0x169, 15);
            uint tickCount = (uint)System.Environment.TickCount;

            p.writeVersion();
            p.writeShort(15);
            p.writeShort(0);
            p.writeShort(8);
            p.writeCommandID();

            p.writeByte(0x00);
            p.writeInt(tickCount);

            p.writeChecksum();

            c.send(p);
        }
    }
}
