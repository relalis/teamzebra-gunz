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
using System.Threading;
using System.Net;
using System.Net.Sockets;

using Zebra.DatabaseInteraction;
using Zebra.DataStructures;
using Zebra.Miscellaneous;

namespace Zebra.Networking
{
    /// <summary>
    /// The class containing most of the
    /// networking code.
    /// </summary>
    public static class Networking
    {
        /// <summary>
        /// The port for the UDP socket
        /// to listen on.
        /// </summary>
        private static int udpPort;

        /// <summary>
        /// The thread for the UDP socket
        /// to listen on.
        /// </summary>
        private static Thread listenThread;

        /// <summary>
        /// The UDP socket to listen for
        /// connections on.
        /// </summary>
        private static Socket listenSock;

        /// <summary>
        /// Initializes all the networking and
        /// starts listening for connections.
        /// </summary>
        /// <returns>whether or not the initialization was successful</returns>
        public static bool initialize()
        {
            try
            {
                udpPort = 8900;
                listenThread = new Thread(new ThreadStart(startReceiveFrom));
                listenThread.Start();

                Console.WriteLine("   Networking initialized successfully!");
                return true;
            }
            catch
            {
                Console.WriteLine("   Networking initialization failed!");
                listenThread.Abort();
                return false;
            }
        }

        /// <summary>
        /// Starts listening on a new thread
        /// for incoming connections and sends
        /// the server information to any
        /// clients that connect.
        /// </summary>
        public static void startReceiveFrom()
        {
            try
            {
                listenSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint localIPEndPoint = new IPEndPoint(IPAddress.Parse(Globals.ipAddress), udpPort);

                listenSock.Bind(localIPEndPoint);

                for (; ; )
                {
                    byte[] recv = new byte[11];
                    IPEndPoint tmpIpEndPoint = new IPEndPoint(IPAddress.Parse(Globals.ipAddress), udpPort);
                    EndPoint remoteEP = (tmpIpEndPoint);
                    int bytesReceived = listenSock.ReceiveFrom(recv, ref remoteEP);
                    listenSock.SendTo(buildServerListInfoPacket(), remoteEP);
                }
            }
            catch
            {
                // do nothing
            }
        }

        /// <summary>
        /// Builds a packet containing
        /// the current server list
        /// information.
        /// </summary>
        /// <returns></returns>
        private static byte[] buildServerListInfoPacket()
        {
            string[] ipAddress = Globals.ipAddress.Split(".".ToCharArray());
            GunzPacket p = new GunzPacket(0x64, 0x9C42, 38);

            // packet header
            p.writeVersion();
            p.writeInt(0x26);
            p.writeShort(0x20);
            p.writeByte(0x42);
            p.writeShort(0x9C);

            p.writeInt(0x17);
            p.writeInt(0x0F);
            p.writeInt(0x01);
            p.writeByte(byte.Parse(ipAddress[0]));
            p.writeByte(byte.Parse(ipAddress[1]));
            p.writeByte(byte.Parse(ipAddress[2]));
            p.writeByte(byte.Parse(ipAddress[3]));
            p.writeShort((ushort)(SQLConnector.executeScalarShort("SELECT Port FROM ServerProperties " +
                "WHERE ServerID = " + Globals.serverID)));
            p.writeShort(0x00);
            p.writeByte(Globals.serverID);
            p.writeShort((ushort)(SQLConnector.executeScalarShort("SELECT MaxPlayers FROM ServerProperties " +
                "WHERE ServerID = " + Globals.serverID)));
            p.writeShort((ushort)(SQLConnector.executeScalarShort("SELECT CurrPlayers FROM ServerProperties " +
                "WHERE ServerID = " + Globals.serverID)));
            p.writeByte(SQLConnector.executeScalarByte("SELECT Type FROM ServerProperties WHERE " +
                "ServerID = " + Globals.serverID));
            p.writeByte(SQLConnector.executeScalarByte("SELECT [Open] FROM ServerProperties WHERE " +
                "ServerID = " + Globals.serverID));

            p.writeChecksum();
            return p.returnData();
        }

        /// <summary>
        /// Shuts down all the networking, closes
        /// all connections, etc.
        /// </summary>
        /// <returns></returns>
        public static bool shutdown()
        {
            if (listenSock == null || listenThread == null)
            {
                return false;
            }

            try
            {
                listenSock.Close();
                listenThread.Abort();
                Console.WriteLine("   Networking finalized successfully!\n");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("   Error in finalizing networking.\n\n" + e.Message);
                return false;
            }
        }
    }
}
