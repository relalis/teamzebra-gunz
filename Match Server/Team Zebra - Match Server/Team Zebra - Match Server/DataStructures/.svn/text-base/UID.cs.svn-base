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

namespace Zebra.DataStructures
{
    /// <summary>
    /// Struct to represent a Gunz MUID.
    /// </summary>
    public struct UID
    {
        /// <summary>
        /// the MUID's high ID
        /// </summary>
        public uint first;

        /// <summary>
        /// the MUID's low ID
        /// </summary>
        public uint second;

        /// <summary>
        /// Override the == operator
        /// </summary>
        /// <param name="one">first UID to compare</param>
        /// <param name="two">second UID to compare</param>
        /// <returns>whether the UIDs are equal</returns>
        public static bool operator ==(UID one, UID two)
        {
            return ((one.first == two.first) && (one.second == two.second)); 
        }

        /// <summary>
        /// Override the != operator
        /// </summary>
        /// <param name="one">first UID to compare</param>
        /// <param name="two">second UID to compare</param>
        /// <returns>whether the UIDs are unequal</returns>
        public static bool operator !=(UID one, UID two)
        {
            return !(one == two);
        }

        /// <summary>
        /// Overrides the Equals method
        /// </summary>
        /// <param name="o">the object to compare this to</param>
        /// <returns>whether they're equal</returns>
        public override bool Equals(object o)
        {
            if (!(o is UID))
                return false;

            return (this == (UID)o);
        }

        /// <summary>
        /// Overrides the GetHashCode method
        /// </summary>
        /// <returns>the hash code of this object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
