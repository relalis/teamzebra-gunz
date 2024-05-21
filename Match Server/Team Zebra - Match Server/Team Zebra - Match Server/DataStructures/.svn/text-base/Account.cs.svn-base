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

using Zebra.DatabaseInteraction;

namespace Zebra.DataStructures
{
    /// <summary>
    /// Class to represent an in-game account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// this account's username
        /// </summary>
        public string username;

        /// <summary>
        /// this account's password
        /// </summary>
        public string password;

        /// <summary>
        /// this account's UGradeID
        /// </summary>
        public byte uGradeID;

        /// <summary>
        /// this account's AID
        /// </summary>
        public int AID;

        /// <summary>
        /// this account's characters
        /// </summary>
        public Character[] characters;

        /// <summary>
        /// this account's current character
        /// </summary>
        public Character currentCharacter;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="username">this account's username</param>
        /// <param name="password">this account's password</param>
        /// <param name="uGradeID">this account's UGradeID</param>
        public Account(string username, string password, byte uGradeID)
        {
            this.username = username;
            this.password = password;
            this.uGradeID = uGradeID;
            this.AID = DynamicSQL.executeScalarInt("SELECT AID From Accounts " +
                "WHERE UserID = '" + username + "'");
            characters = new Character[4];
            currentCharacter = null;
        }

        /// <summary>
        /// Adds a character to this account.
        /// </summary>
        /// <param name="c">the character to add</param>
        public void addCharacter(Character c)
        {
            for (int i = 0; i < 4; i++)
            {
                if (characters[i] == null)
                {
                    characters[i] = c;
                    return;
                }
            }
        }

        /// <summary>
        /// Returns the specified character.
        /// </summary>
        /// <param name="index">the character's index</param>
        /// <returns>the character</returns>
        public Character getCharacter(int index)
        {
            return characters[index];
        }

        /// <summary>
        /// Returns the specified character.
        /// </summary>
        /// <param name="name">the character's name</param>
        /// <returns>the character</returns>
        public Character getCharacter(string name)
        {
            for (int i = 0; i < 4; i++)
            {
                if (characters[i].name == name)
                {
                    return characters[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Clears this account's character list.
        /// </summary>
        public void clearCharacterList()
        {
            for (int i = 0; i < 4; i++)
            {
                characters[i] = null;
            }
        }

        /// <summary>
        /// Remove a character from this account.
        /// </summary>
        /// <param name="c">the character to remove</param>
        public void removeCharacter(Character c)
        {
            int index = -1;

            for (int i = 0; i < 4; i++)
            {
                if (characters[i] == c)
                {
                    index = i;
                    characters[i] = null;
                    break;
                }
            }

            if (index != -1)
            {
                for (int i = index; i < 4; i++)
                {
                    characters[index - 1] = characters[index];
                }

                characters[3] = null;
            }
        }
    }
}
