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
    /// Class to represent an in-game character.
    /// </summary>
    public class Character
    {
        /// <summary>
        /// the character's name
        /// </summary>
        public string name;

        /// <summary>
        /// the character's level
        /// </summary>
        public byte level;

        /// <summary>
        /// the character's CID
        /// </summary>
        public uint CID;

        /// <summary>
        /// the character's currently equipped item IDs
        /// </summary>
        public EquippedItemIDs equippedItemIDs;

        /// <summary>
        /// the character's currently equipped CIIDs
        /// </summary>
        public EquippedCIIDs equippedCIIDs;

        /// <summary>
        /// the character's index
        /// </summary>
        public byte charIndex;

        /// <summary>
        /// the character's CLID
        /// </summary>
        public uint CLID;

        /// <summary>
        /// the character's sex
        /// </summary>
        public byte sex;

        /// <summary>
        /// the character's hair
        /// </summary>
        public byte hair;

        /// <summary>
        /// the character's face
        /// </summary>
        public byte face;

        /// <summary>
        /// the character's XP level
        /// </summary>
        public uint XP;

        /// <summary>
        /// the character's bounty level
        /// </summary>
        public uint BP;

        /// <summary>
        /// the character's HP
        /// </summary>
        public ushort HP;

        /// <summary>
        /// the character's AP
        /// </summary>
        public ushort AP;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">character's name</param>
        /// <param name="level">character's level</param>
        public Character(string name, byte level)
        {
            this.name = name;
            this.level = level;
        }

        /// <summary>
        /// Fills all the character's information with the
        /// values from the database.
        /// </summary>
        public void loadCharacterFromDatabase()
        {
            CID = (uint)DynamicSQL.executeScalarInt("SELECT CID FROM Characters WHERE " +
                "Name = '" + name + "'");
            CLID = (uint)DynamicSQL.executeScalarInt("SELECT CLID FROM Characters WHERE " +
                "Name = '" + name + "'");
            sex = DynamicSQL.executeScalarByte("SELECT Sex FROM Characters WHERE " +
                "Name = '" + name + "'");
            hair = DynamicSQL.executeScalarByte("SELECT Hair FROM Characters WHERE " +
                "Name = '" + name + "'");
            face = DynamicSQL.executeScalarByte("SELECT Face FROM Characters WHERE " +
                "Name = '" + name + "'");
            XP = (uint)DynamicSQL.executeScalarInt("SELECT XP FROM Characters WHERE " +
                "Name = '" + name + "'");
            BP = (uint)DynamicSQL.executeScalarInt("SELECT BP FROM Characters WHERE " +
                "Name = '" + name + "'");
            HP = (ushort)DynamicSQL.executeScalarShort("SELECT HP FROM Characters WHERE " +
                "Name = '" + name + "'");
            AP = (ushort)DynamicSQL.executeScalarShort("SELECT AP FROM Characters WHERE " +
                "Name = '" + name + "'");

            equippedItemIDs.head = (uint)DynamicSQL.executeScalarInt("SELECT HeadItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.chest = (uint)DynamicSQL.executeScalarInt("SELECT ChestItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.hands = (uint)DynamicSQL.executeScalarInt("SELECT HandsItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.legs = (uint)DynamicSQL.executeScalarInt("SELECT LegsItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.feet = (uint)DynamicSQL.executeScalarInt("SELECT FeetItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.ring1 = (uint)DynamicSQL.executeScalarInt("SELECT Ring1ItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.ring2 = (uint)DynamicSQL.executeScalarInt("SELECT Ring2ItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.melee = (uint)DynamicSQL.executeScalarInt("SELECT MeleeItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.primaryWeapon = (uint)DynamicSQL.executeScalarInt("SELECT PrimaryItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.secondaryWeapon = (uint)DynamicSQL.executeScalarInt("SELECT SecondaryItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.item1 = (uint)DynamicSQL.executeScalarInt("SELECT Custom1ItemID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedItemIDs.item2 = (uint)DynamicSQL.executeScalarInt("SELECT Custom2ItemID FROM " +
                "Characters WHERE Name = '" + name + "'");

            equippedCIIDs.head = (uint)DynamicSQL.executeScalarInt("SELECT HeadCIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.chest = (uint)DynamicSQL.executeScalarInt("SELECT ChestCIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.hands = (uint)DynamicSQL.executeScalarInt("SELECT HandsCIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.legs = (uint)DynamicSQL.executeScalarInt("SELECT LegsCIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.feet = (uint)DynamicSQL.executeScalarInt("SELECT FeetCIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.ring1 = (uint)DynamicSQL.executeScalarInt("SELECT Ring1CIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.ring2 = (uint)DynamicSQL.executeScalarInt("SELECT Ring2CIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.melee = (uint)DynamicSQL.executeScalarInt("SELECT MeleeCIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.primaryWeapon = (uint)DynamicSQL.executeScalarInt("SELECT PrimaryCIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.secondaryWeapon = (uint)DynamicSQL.executeScalarInt("SELECT SecondaryCIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.item1 = (uint)DynamicSQL.executeScalarInt("SELECT Custom1CIID FROM " +
                "Characters WHERE Name = '" + name + "'");
            equippedCIIDs.item2 = (uint)DynamicSQL.executeScalarInt("SELECT Custom2CIID FROM " +
                "Characters WHERE Name = '" + name + "'");
        }
    }
}
