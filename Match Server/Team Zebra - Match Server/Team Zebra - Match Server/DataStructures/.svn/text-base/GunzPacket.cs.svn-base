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

using System;

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
        /// <param name="key">packet's encryption/decryption key</param>
        public GunzPacket(byte version, ushort commandID) : base()
        {
            this.version = version;
            this.commandID = commandID;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="arr">data to be turned into a packet</param>
        /// <param name="key">packet's encryption/decryption key</param>
        public GunzPacket(byte[] arr) : base(arr)
        {
            this.version = arr[0];
            this.commandID = (ushort)((arr[8]) + (arr[9] * 256));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="version">version of the packet</param>
        /// <param name="commandID">packet's ID</param>
        /// <param name="key">packet's encryption/decryption key</param>
        /// <param name="size">tentative size of the packet, can be resized</param>
        public GunzPacket(byte version, ushort commandID, int size) : base(size)
        {
            this.version = version;
            this.commandID = commandID;
        }

        /// <summary>
        /// Returns this packet's opcode.
        /// </summary>
        /// <returns>the packet's opcode</returns>
        public ushort getCommandID()
        {
            return commandID;
        }

        /// <summary>
        /// Write this packet's checksum to the header. Call
        /// this immediately before encryption.
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
        /// Writes the packet length to the packet. Called
        /// immediately before checksumming.
        /// </summary>
        public void writePacketLength()
        {
            data[2] = (byte)index;
            data[3] = (byte)(index / 256);
        }

        /// <summary>
        /// Writes the command size to the packet. Called
        /// immediately before writing the packet length.
        /// </summary>
        public void writeCommandSize()
        {
            data[6] = (byte)(index - 6);
            data[7] = (byte)((index - 6) / 256);
        }

        /// <summary>
        /// Writes the command ID to the packet. Only call
        /// at the right time, very crucial.
        /// </summary>
        public void writeCommandID()
        {
            writeShort(commandID);
        }

        /// <summary>
        /// Writes the header with 0x00 placeholders for the packet
        /// length, command size and checksum.
        /// </summary>
        public void startHeader()
        {
            writeVersion();
            writeShort(0x00);
            writeShort(0x00);
            writeShort(0x00);
            writeCommandID();
            writeByte(0x00);
        }

        /// <summary>
        /// Writes the packet length, checksum and command size.
        /// </summary>
        public void finishHeader()
        {
            writeCommandSize();
            writePacketLength();
            writeChecksum();
            trim();
        }

        /// <summary>
        /// Makes a checksum of the given packet.
        /// </summary>
        /// <param name="packet">the packet to be checksummed</param>
        /// <returns>the packet's checksum</returns>
        private ushort packetChecksum()
        {
            uint sum = 0;
            byte[] packet = getWrittenBuffer();
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

        /// <summary>
        /// Encrypts the packet. Obviously should
        /// only be called immediately after receiving.
        /// </summary>
        public void decrypt(byte[] key)
        {
            /*
             * Gunz packets only encrypt the two bytes that have
             * the packet length and everything after the checksum.
             * In other words, the encryption excludes the first 2
             * bytes and the 5th and 6th byte. Hence the reason
             * why we have all the weird stuff going in with excluding
             * certain bytes before we do the crypto functions.
             */

            rawDecrypt(ref data, 2, 2, key);
            rawDecrypt(ref data, 6, data.Length - 6, key);
        }

        /// <summary>
        /// Encrypts the packet. Obviously should
        /// only be called immedaitely before sending.
        /// </summary>
        public void encrypt(byte[] key)
        {
            /*
             * Gunz packets only encrypt the two bytes that have
             * the packet length and everything after the checksum.
             * In other words, the encryption excludes the first 2
             * bytes and the 5th and 6th byte. Hence the reason
             * why we have all the weird stuff going in with excluding
             * certain bytes before we do the crypto functions.
             */

            byte[] packet = getWrittenBuffer();
            rawEncrypt(ref packet, 2, 2, key);
            rawEncrypt(ref packet, 6, packet.Length - 6, key);
            data = packet;
        }

        /// <summary>
        /// "Raw" decrypt. Don't pass it the entire packet
        /// or problems arise.
        /// </summary>
        /// <param name="data">the data to be decrypted</param>
        /// <param name="index">the offset in the data to start at</param>
        /// <param name="length">the length of the data to be decrypted</param>
        /// <param name="key">the key to decrypt with</param>
        private void rawDecrypt(ref byte[] packet, int index, int length, byte[] key)
        {
            /* Credits to Phail for posting on RZ */
            for (int i = 0; i < length; ++i)
            {
                byte a = packet[index + i];
                a ^= 0x0F0;
                byte b = (byte)(7 & a);
                b <<= 5;
                a >>= 3;
                b = (byte)(a | b);
                packet[index + i] = (byte)(b ^ key[i % 32]);
            }

        }

        /// <summary>
        /// "Raw" encrypt. Don't pass it the entire packet
        /// or problems arise.
        /// </summary>
        /// <param name="data">the data to be encrypted</param>
        /// <param name="index">the offset in the data to start at</param>
        /// <param name="length">the length of the data to be encrypted</param>
        /// <param name="key">the key to encrypt with</param>
        private void rawEncrypt(ref byte[] packet, int index, int length, byte[] key)
        {
            /* Credits to Phail for posting on RZ */
            for (int i = 0; i < length; ++i)
            {
                byte a = packet[index + i];
                a ^= key[i % 32];
                a <<= 3;

                byte b = (byte)(a >> 8);
                b |= (byte)(a & 0xFF);
                b ^= 0xF0;
                packet[index + i] = b;
            }
        }

        /// <summary>
        /// Says whether or not the packet is encrypted.
        /// </summary>
        /// <returns>whether or not it's encrypted</returns>
        public bool isEncrypted()
        {
            return (version == 0x65 ? true : false);
        }

        /// <summary>
        /// ToString() method for printing packets.
        /// </summary>
        /// <returns>the string representation of this packet</returns>
        public override string ToString()
        {
            byte[] packet = getWrittenBuffer();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(packet.Length);

            for (int i = 0; i < packet.Length; i++)
            {
                sb.Append(packet[i].ToString("X2") + " ");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Trims the packet to only include what's been written to.
        /// </summary>
        public void trim()
        {
            data = getWrittenBuffer();
        }
    }
}
