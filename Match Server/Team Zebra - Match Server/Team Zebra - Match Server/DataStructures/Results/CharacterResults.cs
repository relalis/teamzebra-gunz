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

namespace Zebra.DataStructures.Results
{
    /// <summary>
    /// Enumeration of all results dealing with character
    /// creation and deletion.
    /// </summary>
    public enum CharacterResults : uint
    {
        OK = 0,
        nameInUse = 10100,
        invalidCharacters = 10101,
        nameTooShort = 10102,
        nameTooLong = 10103,
        blankName = 10104,
        nameDoesNotExist = 10110,
        cannotDeleteCharacter = 10111
    }
}
