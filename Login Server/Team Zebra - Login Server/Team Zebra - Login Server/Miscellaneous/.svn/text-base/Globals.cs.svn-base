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
using System.Collections;

using Zebra.Configuration;
using Zebra.DatabaseInteraction;

namespace Zebra.Miscellaneous
{
    /// <summary>
    /// Global variables. I know, shoot me.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// public access to any class to the config file, without
        /// each class having to instantiate their own
        /// </summary>
        public static IniFile config = new IniFile(System.IO.Path.GetDirectoryName(
            System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]
            .FullyQualifiedName) + "\\config.ini");

        /// <summary>
        /// the version number of our server
        /// </summary>
        public static string version = "v1.0";

        /// <summary>
        /// the login name for the SQL server. Only needed if
        /// you're not using Windows authentification
        /// </summary>
        public static string SQLlogin = config.readValue("SQL ENVIRONMENT", "LOGIN");

        /// <summary>
        /// the password for the SQL server. Only needed if
        /// you're not using Windows authentification
        /// </summary>
        public static string SQLpassword = config.readValue("SQL ENVIRONMENT", "PASSWORD");

        /// <summary>
        /// specifies wether or not to use Windows authentification
        /// to login to the SQL server
        /// </summary>
        public static string SQLwindowsauth = config.readValue("SQL ENVIRONMENT", "WINDOWS AUTH");

        /// <summary>
        /// the name of the database on the SQL server
        /// to be using for our server
        /// </summary>
        public static string SQLdatabase = config.readValue("SQL ENVIRONMENT", "DATABASE");

        /// <summary>
        /// the name of the SQL server to be logging 
        /// into
        /// </summary>
        public static string SQLserver = config.readValue("SQL ENVIRONMENT", "SERVER");

        /// <summary>
        /// the number of seconds for our server to wait
        /// before it declares the SQL login attempts
        /// as unsuccessful
        /// </summary>
        public static string SQLtimeout = config.readValue("SQL ENVIRONMENT", "TIMEOUT");

        /// <summary>
        /// the ID of this current server
        /// </summary>
        public static byte serverID = byte.Parse(config.readValue("SERVER", "ID"));

        /// <summary>
        /// the IP address for the server to listen on
        /// </summary>
        public static string ipAddress = config.readValue("SERVER", "IP ADDRESS");
    }
}
