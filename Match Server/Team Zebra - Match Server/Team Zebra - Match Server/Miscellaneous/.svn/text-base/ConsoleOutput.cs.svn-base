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

namespace Zebra.Miscellaneous
{
    /// <summary>
    /// Static class, contains custom console output
    /// methods.
    /// </summary>
    public static class ConsoleOutput
    {
        /// <summary>
        /// Writes text to the screen without a newline at the end, 
        /// includes a timestamp.
        /// </summary>
        /// <param name="output">the string to be printed</param>
        public static void writeWithTimeStamp(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[" + DateTime.Now.ToString() + "] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(output);
        }

        /// <summary>
        /// Writes text to the screen with a newline at the end, 
        /// includes a timestamp.
        /// </summary>
        /// <param name="output">the string to be printed</param>
        public static void writeLineWithTimeStamp(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[" + DateTime.Now.ToString() + "] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(output);
        }
    }
}
