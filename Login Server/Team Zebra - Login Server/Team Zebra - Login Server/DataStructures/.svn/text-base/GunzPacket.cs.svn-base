/*
 * "Team Zebra - Login Server" - An open-source server emulator
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
 * This file is part of "Team Zebra - Login Server".
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

using System;
using System.Collections.Generic;
using System.Text;

namespace Zebra.DataStructures
{
    /// <summary>
    /// Class to represent a packet of Gunz network data.
    /// </summary>
    public class GunzPacket : WriteableBuffer
    {
        /// <summary>
        /// this packet's version
        /// </summary>
        private byte version;

        /// <summary>
        /// this packet's command ID
        /// </summary>
        private ushort commandID;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="version">version of the packet</param>
        /// <param name="commandID">packet's ID</param>
        public GunzPacket(byte version, ushort commandID)
            : base()
        {
            this.version = version;
            this.commandID = commandID;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="version">version of the packet</param>
        /// <param name="commandID">packet's ID</param>
        /// <param name="size">tentative size of the packet, can be resized</param>
        public GunzPacket(byte version, ushort commandID, int size)
            : base(size)
        {
            this.version = version;
            this.commandID = commandID;
        }

        /// <summary>
        /// Return this packet's data.
        /// </summary>
        /// <returns></returns>
        public byte[] returnData()
        {
            return base.getWrittenBuffer();
        }

        /// <summary>
        /// Write this packet's checksum to the header.
        /// </summary>
        public void writeChecksum()
        {
            ushort checksum = packetChecksum();

            data[4] = (byte)checksum;
            data[5] = (byte)(checksum / 256);
        }

        /// <summary>
        /// Write the version of this packet to the header.
        /// </summary>
        public void writeVersion()
        {
            writeShort((ushort)version);
        }

        /// <summary>
        /// Makes a checksum of the given packet.
        /// </summary>
        /// <param name="packet">the packet to be checksummed</param>
        /// <returns>the packet's checksum</returns>
        private ushort packetChecksum()
        {
            uint sum = 0;
            byte[] packet = returnData();
            int len = packet.Length;

            if (len > 65535)
                return 1;

            for (int i = 6; i < len; i++)
                sum += packet[i];

            for (int i = 0; i < 4; i++)
                sum -= packet[i];

            sum += (sum >> 0x10);

            return (ushort)sum;
        }
    }
}
