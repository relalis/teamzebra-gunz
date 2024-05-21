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
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace Zebra.Configuration
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    public class IniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Value"></PARAM>
        public void writeValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string readValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, this.path);
            return temp.ToString();
        }

        /// <summary>
        /// Called if no config file exists. Creates a blank one.
        /// </summary>
        static public void createNewConfig()
        {
            StreamWriter configCreator;

            configCreator = File.CreateText("config.ini");
            configCreator.WriteLine("[SQL ENVIRONMENT]");
            configCreator.WriteLine(";SQL Server login name");
            configCreator.WriteLine("LOGIN=");
            configCreator.WriteLine();
            configCreator.WriteLine(";SQL Server login password");
            configCreator.WriteLine("PASSWORD=");
            configCreator.WriteLine();
            configCreator.WriteLine(";Use Windows Authentification? If yes, name and password aren't really needed. If no, provide a name and pass.");
            configCreator.WriteLine("WINDOWS AUTH=");
            configCreator.WriteLine();
            configCreator.WriteLine(";Name of the default database");
            configCreator.WriteLine("DATABASE=");
            configCreator.WriteLine();
            configCreator.WriteLine(";Server name, for example <machine name>\\SQLEXPRESS. Refer to SQL Server Management Studio for the name you need to use.");
            configCreator.WriteLine("SERVER=");
            configCreator.WriteLine();
            configCreator.WriteLine(";How many seconds to wait before declaring the connection timed out. 15 is usually pretty good.");
            configCreator.WriteLine("TIMEOUT=");
            configCreator.WriteLine();
            configCreator.WriteLine();
            configCreator.WriteLine("[SERVER]");
            configCreator.WriteLine(";The IP address to listen for connections on");
            configCreator.WriteLine("IP ADDRESS=");
            configCreator.WriteLine();
            configCreator.WriteLine(";The ID of the current server");
            configCreator.Write("ID=");

            configCreator.Close();
        }
    }
}
