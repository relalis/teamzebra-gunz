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

//#define PRINT_INCOMING_DATA

using System;
using System.Net;
using System.Net.Sockets;

using Zebra.Miscellaneous;
using Zebra.PacketHandling;
using Zebra.PacketHandling.PacketCategories;

namespace Zebra.DataStructures
{
    /// <summary>
    /// Class to represent a client connected to our server.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// the client's connection
        /// </summary>
        public Socket socket;

        /// <summary>
        /// the IP address of this client
        /// </summary>
        public IPAddress ip;

        /// <summary>
        /// the client's current UID
        /// </summary>
        public UID uid;

        /// <summary>
        /// the account logged into the client
        /// </summary>
        public Account account;

        /// <summary>
        /// the key used for encryption/decryption
        /// when dealing with packets for this client
        /// </summary>
        public byte[] key;

        /// <summary>
        /// this client's current state in the channel
        /// </summary>
        public uint stageState;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="uid">client's UID</param>
        /// <param name="socket">socket to use</param>
        public Client(UID uid, Socket socket)
        {
            this.socket = socket;
            this.uid = uid;
            ip = ((IPEndPoint)socket.RemoteEndPoint).Address;
            key = initializeKey(sendHandshake());
            stageState = 0;

            byte[] buffer = new byte[1024];
            if (socket.Connected)
            {
                socket.BeginReceive(buffer, 0, 1024, SocketFlags.None,
                    new AsyncCallback(onDataReceived), (object)buffer);
            }
            else
            {
                socket.Close();
            }
        }

        /// <summary>
        /// Initalizes the encryption key for this client.
        /// </summary>
        /// <param name="handshake">the key exchange packet</param>
        /// <returns>the encryption key</returns>
        public static byte[] initializeKey(byte[] handshake)
        {
            byte[] staticKey = { 0x57,0x02,0x5B,0x04,0x34,0x06,0x01,0x08,
		                         0x37,0x0A,0x12,0x69,0x41,0x38,0x0F,0x78,
		                         0x37,0x04,0x5D,0x2E,0x43,0x38,0x49,0x53,
		                         0x50,0x05,0x13,0xC9,0x28,0xA4,0x4D,0x05 };

            byte[] outKey = new byte[32];
            byte[] xorKey = new byte[16];

            for (int i = 12; i < 16; i++)
                xorKey[i - 12] = handshake[i + 10];
            for (int i = 0; i < 12; i++)
                xorKey[i + 4] = handshake[i + 10];

            for (int i = 0; i < 32; i++)
                outKey[i] = staticKey[i];
            for (int i = 0; i < 16; i++)
                outKey[i] ^= xorKey[i];

            return outKey;
        }

        /// <summary>
        /// The callback for when data is received through 
        /// this client's socket.
        /// </summary>
        /// <param name="asyn"></param>
        private void onDataReceived(IAsyncResult asyn)
        {
            if (socket.Connected)
            {
                try
                {
                    socket.EndReceive(asyn);
                }
                catch
                {
                    socket.Close();
                    return;
                }

                byte[] buffer = (byte[])asyn.AsyncState;
                GunzPacket p = new GunzPacket(buffer);

                // Only decrypt the packet if it's encrypted,
                if (p.isEncrypted())
                {
                    p.decrypt(key);
                }

#if PRINT_INCOMING_DATA
                buffer = p.data;
                ConsoleOutput.writeWithTimeStamp("Receieved data (" 
                    + ip.ToString() + "): ");
                for (int i = 0; i < 100; i++)
                    Console.Write("{0:X} ", buffer[i]);
                Console.WriteLine();
#endif
                
                try
                {
                    HeaderResponses.respondToHeader(this, p);
                }
                catch(Exception e)
                {
                    ConsoleOutput.writeLineWithTimeStamp("Error: " + e.Message);
                    socket.Close();
                    return;
                }

                buffer = new byte[1024];
                socket.BeginReceive(buffer, 0, 1024, SocketFlags.None, 
                    new AsyncCallback(onDataReceived), (object)buffer);
            }
            else
            {
                socket.Close();
            }
        }

        /// <summary>
        /// Send data to this client.
        /// </summary>
        /// <param name="packet">packet to send</param>
        public void send(GunzPacket packet)
        {
            socket.Send(packet.getWrittenBuffer());
        }

        /// <summary>
        /// Sends the key exchange packet (handshake)
        /// to the client.
        /// </summary>
        private byte[] sendHandshake()
        {
            Random r = new Random();
            GunzPacket p = new GunzPacket(0x0A, 0x00, 26);

            // header
            p.writeVersion();
            p.writeShort(26);
            p.writeShort(0x00);
            p.writeInt(0x00);

            // payload
            p.writeInt(uid.first);
            p.writeInt(uid.second);

            for (int i = 0; i < 8; i++)
                p.writeByte((byte)r.Next(256));

            p.writeChecksum();

            // send it
            byte[] data = p.getWrittenBuffer();
            socket.Send(data);
            return data;
        }

        /// <summary>
        /// Closes the client's connection to the
        /// server.
        /// </summary>
        public void closeConnection()
        {
            socket.Close();
        }

        /// <summary>
        /// Says whether or not this client is an administrator.
        /// </summary>
        /// <returns>whether or not this client is an administrator</returns>
        public bool isAdmin()
        {
            return (account.uGradeID == 254 || account.uGradeID == 255);
        }
    }
}
