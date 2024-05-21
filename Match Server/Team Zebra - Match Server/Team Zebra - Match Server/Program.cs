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

//#define LOGPORT

using System;
using System.IO;
using System.Threading;
using System.Timers;

using Zebra.Configuration;
using Zebra.DatabaseInteraction;
using Zebra.DataStructures;
using Zebra.Miscellaneous;
using Zebra.Networking;
using Zebra.PacketHandling.PacketCategories;

namespace Zebra
{
    /// <summary>
    /// The main class of our application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entrypoint for our program.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.Title = "Team Zebra - Match Server";
            printSplash();


            // Check if there's a config file. If there isn't
            // one, let's go ahead and create one.
            Console.WriteLine("-Checking for config.ini...");
            if (File.Exists("config.ini"))
            {
                Console.WriteLine("   Config loaded successfully!");
            }
            else
            {
                Console.WriteLine("   Error: Config file doesn't exist! A config file has" +
                                  "\n   been created for you. Edit it and restart the server.");
                IniFile.createNewConfig();
                exit();
            }


            // Attempt to connect to the dynamic database.
            Console.WriteLine("\n-Connecting to dynamic SQL database...");
            if (!DynamicSQL.startConnection())
            {
                exit();
            }

            // Attempt to connect to the static database.
            Console.WriteLine("\n-Connecting to static SQL database...");
            if (!StaticSQL.startConnection())
            {
                exit();
            }


            // Initialize the networking.
            Console.WriteLine("\n-Initializing networking...");
            Networking.Networking.initialize();
            Console.WriteLine("   Networking initialized successfully!");

            // Load the channel list
            Console.WriteLine("\n-Initializing channel list...");
            if (!Lobby.createGlobalChannelList())
            {
                exit();
            }

            // Initialize the shop item list.
            Console.WriteLine("\n-Initializing shop...");
            Match.initializeShopList();
            Console.WriteLine("   Shop initialized successfully!");


            // Finally just begin to listen for connections on
            // the network and input from the user through the
            // console.
            Console.WriteLine("\n-Initialization complete! Server is now " +
                    "listening for \n   incoming connections! Type \"!quit\" " +
                    "to exit the program, \n   or type \"!commands\" for a list " +
                    "of commands you can use \n   on this server.\n");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("---------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;

            Thread inputThread = new Thread(new ThreadStart(getUserInput));
            inputThread.Start();

            ConsoleOutput.writeLineWithTimeStamp("Server initialized! Listening for connections.");

            System.Timers.Timer updateTimer = new System.Timers.Timer(300000);
            updateTimer.Elapsed += new ElapsedEventHandler(timerEvent);
            updateTimer.Start();
            updateServerStatus();
        }

        /// <summary>
        /// Prints the splash screen.
        /// </summary>
        public static void printSplash()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("                         _________________________");
            Console.WriteLine("                 _______/                         \\_______");
            Console.WriteLine("                /                                         \\");
            Console.WriteLine(" +-------------+                                           +-------------+");
            Console.WriteLine(" |               Team Zebra - \"GunZ: The Duel\" Match Server              |");
            Console.WriteLine(" |                                                                       |");
            Console.WriteLine(" |			    Release Date: 05/11/09                       |");
            Console.WriteLine(" |		            Written by: Team Zebra                       |");
            Console.WriteLine(" |                              Version: " + Globals.version + "                            |");
            Console.WriteLine(" |                                                                       |");
            Console.WriteLine(" |        \"You may stop this individual, but you can't stop us all       |");
            Console.WriteLine(" |		      ...after all, we're all alike.\"                    |");
            Console.WriteLine(" +-------------+                                           +-------------+");
            Console.WriteLine("                \\_______                           _______/");
            Console.WriteLine("                        \\_________________________/");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===========================");
            Console.WriteLine("        -SERVER LOG-       ");
            Console.WriteLine("===========================");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Basically loops forever until the user types and enters !quit.
        /// </summary>
        public static void getUserInput()
        {
            string input;
            input = Console.ReadLine();

            while (input.ToLower() != "!quit")
            {
                string[] command = input.Split();

                switch (command[0].ToLower())
                {
                    case "!commands":
                        ConsoleOutput.writeLineWithTimeStamp("Commands: !quit, !commands, !rawsql <query>");
                        break;
                    case "!rawsql":
                        string query = input.Substring(8, input.Length - 8);
                        DynamicSQL.executeQuery(query);
                        break;
                    default:
                        ConsoleOutput.writeLineWithTimeStamp("Invalid command!");
                        break;
                }

                input = Console.ReadLine();
            }

            exit();
        }

        /// <summary>
        /// Prints out another spiffy banner thing, and finalizes all connections, 
        /// ends the SQL session, etc.
        /// </summary>
        public static void exit()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("\n===========================");
            Console.WriteLine("      -SERVER EXITING-     ");
            Console.WriteLine("===========================");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("-Closing connection to SQL server...");
            DynamicSQL.closeConnection();

            Console.WriteLine("-Finalizing the networking...");
            Networking.Networking.shutdown();

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Displays the number of connected clients each
        /// time the timer ticks.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void timerEvent(object source, ElapsedEventArgs e)
        {
            ConsoleOutput.writeLineWithTimeStamp("Server running! Connected players: " +
                DynamicSQL.executeScalarShort("SELECT CurrPlayer FROM ServerStatus WHERE ServerID = " +
                Globals.serverID));
        }

        /// <summary>
        /// Updates the ServerProperties table in the DB.
        /// </summary>
        public static void updateServerStatus()
        {
#if LOGPORT
            DynamicSQL.executeQuery("UPDATE ServerProperties SET ServerName = '" +
                Globals.serverName + "', CurrPlayers = 0, MaxPlayers = " +
                Globals.maxPlayers + ", IP = '" + Globals.ipAddress + "', Type = " +
                Globals.serverType + ", PORT = 6016 WHERE ServerID = " + 
                Globals.serverID);
#else
            DynamicSQL.executeQuery("UPDATE ServerProperties SET ServerName = '" +
                Globals.serverName + "', CurrPlayers = 0, MaxPlayers = " +
                Globals.maxPlayers + ", IP = '" + Globals.ipAddress + "', Type = " +
                Globals.serverType + ", Port = " + Globals.port + " WHERE ServerID = " + 
                Globals.serverID);
#endif
        }
    }
}
