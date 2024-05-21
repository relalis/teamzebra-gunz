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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

using Zebra.DataStructures;
using Zebra.Miscellaneous;

namespace Zebra.Networking
{
    /// <summary>
    /// Class with the main network handling.
    /// </summary>
    public static class Networking
    {
        /// <summary>
        /// the main listening socket for the server
        /// </summary>
        private static Socket mainSocket;
        
        /// <summary>
        /// the list of connected users
        /// </summary>
        private static List<Client> userList;

        /// <summary>
        /// the current connection ID
        /// </summary>
        private static uint connID;

        /// <summary>
        /// Initialize the networking and begin listening.
        /// </summary>
        public static void initialize()
        {
            connID = 10;
            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mainSocket.Bind(new IPEndPoint(IPAddress.Parse(Globals.ipAddress), Globals.port));
            mainSocket.Listen(8);
            mainSocket.BeginAccept(new AsyncCallback(onClientConnect), null);
            userList = new List<Client>();
            ThreadPool.SetMinThreads(1000, 1000);
        }

        /// <summary>
        /// The callback for when a client connects
        /// to our server.
        /// </summary>
        /// <param name="asyn">the callback state</param>
        private static void onClientConnect(IAsyncResult asyn)
        {
            connID++;

            UID uid = new UID();
            uid.first = connID;
            uid.second = 0;

            Client c = new Client(uid, mainSocket.EndAccept(asyn));
            userList.Add(c);

            mainSocket.BeginAccept(new AsyncCallback(onClientConnect), null);
        }

        /// <summary>
        /// Shuts down the networking and closes
        /// the connections.
        /// </summary>
        public static void shutdown()
        {
            mainSocket.Close();

            foreach (Client c in userList)
                c.closeConnection();

            Console.WriteLine("   Networking closed successfully!\n");
        }

        /// <summary>
        /// Removes the given client from the userlist.
        /// </summary>
        /// <param name="c">the client to remove</param>
        public static void removeClient(Client c)
        {
            userList.Remove(c);
        }
    }
}