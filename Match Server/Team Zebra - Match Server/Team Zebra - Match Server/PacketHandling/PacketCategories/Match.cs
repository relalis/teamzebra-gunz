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
using System.Collections;
using System.Threading;
using System.Xml;

using Zebra.DatabaseInteraction;
using Zebra.DataStructures;
using Zebra.DataStructures.Results;
using Zebra.Miscellaneous;

namespace Zebra.PacketHandling.PacketCategories
{
    /// <summary>
    /// Contains the functions for all the Match.___
    /// packets.
    /// </summary>
    public static class Match
    {
        /// <summary>
        /// the packet that contains the shop item list.
        /// </summary>
        private static GunzPacket shopItemList;

        /// <summary>
        /// Sends the Match.ResponseLogin packet to
        /// the given client.
        /// </summary>
        /// <param name="c">the client to send to</param>
        /// <param name="p">the packet that has the user/pass</param>
        public static void responseLogin(Client c, GunzPacket p)
        {
            p.setIndex(11);

            string username;
            string password;
            byte uGradeID;

            username = p.readStringFromLength();
            p.readByte();
            password = p.readStringFromLength();
            uGradeID = DynamicSQL.executeScalarByte("SELECT UGradeID FROM Accounts " +
                "WHERE UserID = '" + username + "'");

            GunzPacket response = new GunzPacket(0x64, 0x3EA, 110);
            response.startHeader();

            byte[] encryptMsg = new byte[] { 0x1C, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 
                                             0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                             0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                             0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            if (!checkPassword(username, password))
                response.writeInt((uint)LoginResults.invalidPassword);
            else if (uGradeID == (uint)UGradeIDs.banned)
                response.writeInt((uint)LoginResults.banned);
            else
                response.writeInt((uint)LoginResults.OK);

            response.writeString(Globals.serverName);
            response.writeByte(0x04);
            response.writeByte(Globals.serverType);
            response.writeString(username);
            response.writeByte(uGradeID);
            response.writeShort(0x00);
            response.writeInt(c.uid.first);
            response.writeInt(c.uid.second);
            response.writeArray(encryptMsg);

            response.finishHeader();
            c.send(response);
            c.account = new Account(username, password, uGradeID);
        }

        /// <summary>
        /// Checks whether the test password is the account's
        /// actual password.
        /// </summary>
        /// <param name="name">the account to check</param>
        /// <param name="testPass">the pass to compare</param>
        /// <returns>whether the passwords match</returns>
        private static bool checkPassword(string name, string testPass)
        {
            string realPass = DynamicSQL.executeScalarString("SELECT Password FROM " +
                "Accounts WHERE UserID = '" + name + "'");

            return (realPass == testPass);
        }

        /// <summary>
        /// Sends the character list to the given client.
        /// </summary>
        /// <param name="c">the client to send the list to</param>
        public static void responseAccountCharList(Client c)
        {
            c.account.clearCharacterList();
            GunzPacket response = new GunzPacket(0x64, 0x6A6);
            ArrayList charNames = DynamicSQL.executeScalarArray("SELECT Name FROM " +
                "Characters WHERE AID = " + c.account.AID);

            response.startHeader();

            response.writeInt((uint)(8 + (34 * charNames.Count)));
            response.writeInt(0x22);
            response.writeInt((uint)charNames.Count);

            foreach(string s in charNames)
            {
                byte charIndex = DynamicSQL.executeScalarByte("SELECT CharIndex FROM " +
                    "Characters WHERE Name = '" + s + "'");
                byte level = DynamicSQL.executeScalarByte("SELECT Level FROM " +
                    "Characters WHERE Name = '" + s + "'");
                Character character = new Character(s, level);
                character.loadCharacterFromDatabase();
                c.account.addCharacter(character);

                response.writeStringNoLen(s);
                response.bytePad(0x00, 31 - s.Length);
                response.writeByte(charIndex);
                response.writeByte(level);
            }

            response.finishHeader();
            c.send(response);
        }

        /// <summary>
        /// Sends the details on a character specified in the packet.
        /// </summary>
        /// <param name="c">the client to send it to</param>
        /// <param name="p">the packet with the info</param>
        public static void responseAccountCharInfo(Client c, GunzPacket p)
        {
            p.setIndex(11);
            byte charIndex = p.readByte();

            GunzPacket response = new GunzPacket(0x64, 0x6B8, 170);
            Character character = c.account.getCharacter(charIndex);

            response.startHeader();

            response.writeByte(charIndex);

            // unknown values
            response.writeInt(0x9A);
            response.writeInt(0x92);
            response.writeInt(0x01);

            response.writeStringNoLen(character.name);
            response.bytePad(0x00, 31 - character.name.Length);

            // clan name. zero it for now
            response.bytePad(0x00, 16);

            // clan grade. zero it for now
            response.writeInt(0x00);
            response.writeShort(0x00);

            response.writeByte(charIndex);
            response.writeShort(character.level);
            response.writeByte(character.sex);
            response.writeByte(character.hair);
            response.writeByte(character.face);
            response.writeInt(character.XP);
            response.writeInt(character.BP);

            // bonus rate. zero it for now
            response.writeInt(0x00);
            // prize (whatever that is). zero it for now.
            response.writeShort(0x00);

            response.writeShort(character.HP);
            response.writeShort(character.AP);

            // max weight. client calculates it for us.
            response.writeShort(0x00);
            // safefalls. zero it out.
            response.writeShort(0x00);
            // resistances. zero them out.
            response.writeInt(0x00);
            response.writeInt(0x00);

            response.writeInt(character.equippedItemIDs.head);
            response.writeInt(character.equippedItemIDs.chest);
            response.writeInt(character.equippedItemIDs.hands);
            response.writeInt(character.equippedItemIDs.legs);
            response.writeInt(character.equippedItemIDs.feet);
            response.writeInt(character.equippedItemIDs.ring1);
            response.writeInt(character.equippedItemIDs.ring2);
            response.writeInt(character.equippedItemIDs.melee);
            response.writeInt(character.equippedItemIDs.primaryWeapon);
            response.writeInt(character.equippedItemIDs.secondaryWeapon);
            response.writeInt(character.equippedItemIDs.item1);
            response.writeInt(character.equippedItemIDs.item2);

            response.writeInt((uint)c.account.uGradeID);
            response.writeInt(character.CLID);

            response.finishHeader();
            c.send(response);
        }

        /// <summary>
        /// Tells the client the results of trying to select a character.
        /// </summary>
        /// <param name="client">the client to send to</param>
        /// <param name="p">the packet with the charindex</param>
        public static void responseSelectChar(Client c, GunzPacket p)
        {
            p.setIndex(19);
            int charIndex = p.readInt();

            GunzPacket response = new GunzPacket(0x64, 0x6A8);
            Character character = c.account.getCharacter(charIndex);

            response.startHeader();

            response.writeInt(0x00);

            // unknown values
            response.writeInt(0x9A);
            response.writeInt(0x92);
            response.writeInt(0x01);

            response.writeStringNoLen(character.name);
            response.bytePad(0x00, 31 - character.name.Length);

            // clan name. zero it for now
            response.bytePad(0x00, 16);

            // clan grade. zero it for now
            response.writeInt(0x00);
            response.writeShort(0x00);

            response.writeByte((byte)charIndex);
            response.writeShort(character.level);
            response.writeByte(character.sex);
            response.writeByte(character.hair);
            response.writeByte(character.face);
            response.writeInt(character.XP);
            response.writeInt(character.BP);

            // bonus rate. zero it for now
            response.writeInt(0x00);
            // prize (whatever that is). zero it for now.
            response.writeShort(0x00);

            response.writeShort(character.HP);
            response.writeShort(character.AP);

            // max weight. client calculates it for us.
            response.writeShort(0x00);
            // safefalls. zero it out.
            response.writeShort(0x00);
            // resistances. zero them out.
            response.writeInt(0x00);
            response.writeInt(0x00);

            response.writeInt(character.equippedItemIDs.head);
            response.writeInt(character.equippedItemIDs.chest);
            response.writeInt(character.equippedItemIDs.hands);
            response.writeInt(character.equippedItemIDs.legs);
            response.writeInt(character.equippedItemIDs.feet);
            response.writeInt(character.equippedItemIDs.ring1);
            response.writeInt(character.equippedItemIDs.ring2);
            response.writeInt(character.equippedItemIDs.melee);
            response.writeInt(character.equippedItemIDs.primaryWeapon);
            response.writeInt(character.equippedItemIDs.secondaryWeapon);
            response.writeInt(character.equippedItemIDs.item1);
            response.writeInt(character.equippedItemIDs.item2);

            response.writeInt((uint)c.account.uGradeID);
            response.writeInt(character.CLID);

            // unknown values
            response.writeInt(0x09);
            response.writeInt(0x01);
            response.writeInt(0x01);
            response.writeByte(0x00);

            response.finishHeader();
            c.send(response);
            c.account.currentCharacter = character;
        }

        /// <summary>
        /// Attempts to create a character based on the given packet.
        /// </summary>
        /// <param name="c">the client requesting the creation</param>
        /// <param name="p">the packet with the character info</param>
        public static void responseCreateChar(Client c, GunzPacket p)
        {
            p.setIndex(19);

            int AID = c.account.AID;
            byte charIndex = (byte)p.readInt();
            string name = p.readStringFromLength();
            byte unknown = p.readByte();
            byte sex = (byte)p.readInt();
            byte hair = (byte)p.readInt();
            byte face = (byte)p.readInt();
            byte costume = (byte)p.readInt();

            GunzPacket response = new GunzPacket(0x64, 0x6B0);
            response.startHeader();

            uint result;

            if (name == "")
                result = (uint)CharacterResults.blankName;
            else if (name.Length > 12)
                result = (uint)CharacterResults.nameTooLong;
            else if (DynamicSQL.executeScalarInt("SELECT CID FROM Characters WHERE Name = '" + name + "'") != 0)
                result = (uint)CharacterResults.nameInUse;
            else
            {
                result = (uint)CharacterResults.OK;
                DynamicSQL.executeQuery("EXEC dbo.CreateCharacter " + AID + ", " + charIndex + ", '" + name + "', " +
                    sex + ", " + hair + ", " + face + ", " + costume);
                Character newChar = new Character(name, DynamicSQL.executeScalarByte("SELECT [Level] " +
                    "FROM Characters WHERE Name = '" + name + "'"));
                newChar.loadCharacterFromDatabase();
                c.account.addCharacter(newChar);
            }

            response.writeInt(result);
            response.writeString(name);
            response.writeByte(unknown);

            response.finishHeader();
            c.send(response);
        }

        /// <summary>
        /// Attempts to create a character based on the given packet.
        /// </summary>
        /// <param name="c">the client requesting the creation</param>
        /// <param name="p">the packet with the character info</param>
        public static void responseDeleteChar(Client c, GunzPacket p)
        {
            // compare the sender UID to the one we're currently dealing with to
            // make sure it's not a spoofed packet. no locust plague.
            UID sentUID = new UID();

            p.setIndex(11);
            sentUID.first = (uint)p.readInt();
            sentUID.second = (uint)p.readInt();

            if (sentUID != c.uid)
                return;

            // not spoofed? let's delete the character.
            byte charIndex = (byte)p.readInt();
            string name = p.readStringFromLength();
            uint result;

            GunzPacket response = new GunzPacket(0x64, 0x6B2);
            response.startHeader();

            if (DynamicSQL.executeScalarInt("SELECT CID FROM Characters WHERE Name = '" + name + "'") == 0)
                result = (uint)CharacterResults.nameDoesNotExist;
            else
            {
                result = (uint)CharacterResults.OK;
                DynamicSQL.executeQuery("EXEC dbo.DeleteCharacter " + c.account.AID + ", " + 
                    charIndex + ", '" + name + "'");
            }

            response.writeInt(result);

            response.finishHeader();
            c.send(response);
        }

        /// <summary>
        /// Initializes the shop item list packet.
        /// </summary>
        public static void initializeShopList()
        {
            shopItemList = new GunzPacket(0x64, 0x718);
            shopItemList.startHeader();

            ArrayList items = StaticSQL.executeScalarArray("SELECT ItemID FROM Shop");
            uint itemCount = (uint)items.Count;
            uint totalSize = (itemCount * 4) + 8;

            shopItemList.writeInt(totalSize);
            shopItemList.writeInt(0x04);
            shopItemList.writeInt(itemCount);

            foreach (int itemID in items)
                shopItemList.writeInt((uint)itemID);

            shopItemList.finishHeader();
        }

        /// <summary>
        /// Sends the shop item list to a given client.
        /// </summary>
        /// <param name="c">the client to send the list to</param>
        /// <param name="p">the request packet</param>
        public static void responseShopItemList(Client c, GunzPacket p)
        {
            c.send(shopItemList);
        }

        /// <summary>
        /// Sends the equipment list to the given client.
        /// </summary>
        /// <param name="c">the client that's requesting the list</param>
        /// <param name="p">the request packet</param>
        public static void responseCharacterItemList(Client c, GunzPacket p)
        {
            p.setIndex(11);

            UID uidPlayer = new UID();
            uidPlayer.first = (uint)p.readInt();
            uidPlayer.second = (uint)p.readInt();

            if (uidPlayer != c.uid)
                return;

            GunzPacket response = new GunzPacket(0x64, 0x71E);
            response.startHeader();

            // bounty
            response.writeInt((uint)DynamicSQL.executeScalarInt("SELECT BP FROM Characters WHERE Name = '" +
                c.account.currentCharacter.name + "'"));

            // equipped items
            response.writeInt(104);
            response.writeInt(8);
            response.writeInt(12);

            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.head);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.chest);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.hands);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.legs);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.feet);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.ring1);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.ring2);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.melee);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.primaryWeapon);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.secondaryWeapon);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.item1);
            response.writeInt(0x00);
            response.writeInt(c.account.currentCharacter.equippedCIIDs.item2);

            // equipment list
            ArrayList itemIDs = DynamicSQL.executeScalarArray("SELECT ItemID FROM Inventories WHERE CID = " +
                c.account.currentCharacter.CID);
            ArrayList CIIDs = DynamicSQL.executeScalarArray("SELECT CIID FROM Inventories WHERE CID = " +
                c.account.currentCharacter.CID);

            uint count = (uint)itemIDs.Count;
            uint totalSize = ((uint)((count * 16) + 8));

            response.writeInt(totalSize);
            response.writeInt(16);
            response.writeInt(count);

            for (int i = 0; i < count; i++)
            {
                response.writeInt(0x00);
                response.writeInt(Convert.ToUInt32(CIIDs[i]));
                response.writeInt(Convert.ToUInt32(itemIDs[i]));
                response.writeInt(0x20050800);
            }

            response.finishHeader();
            c.send(response);
        }

        /// <summary>
        /// Attempts to buy an item from the shop.
        /// </summary>
        /// <param name="c">the client attempting to buy</param>
        /// <param name="p">the request packet</param>
        public static void responseBuyItem(Client c, GunzPacket p)
        {
            p.setIndex(11);

            UID uidPlayer = new UID();
            uidPlayer.first = (uint)p.readInt();
            uidPlayer.second = (uint)p.readInt();

            if (uidPlayer != c.uid)
                return;

            int itemID = p.readInt();
            int price = StaticSQL.executeScalarInt("SELECT Price FROM Items WHERE ItemID = " + itemID);
            uint newBounty;
            uint result;

            if (price > c.account.currentCharacter.BP)
            {
                result = (uint)ShopResults.insufficientBounty;
            }
            else
            {
                result = (uint)ShopResults.OK;
                newBounty = (uint)(c.account.currentCharacter.BP - price);
                DynamicSQL.executeQuery("INSERT INTO Inventories(CID, ItemID) VALUES(" +
                    c.account.currentCharacter.CID + ", " + itemID + ")");
                DynamicSQL.executeQuery("UPDATE Characters SET BP = " + newBounty + " WHERE CID = " +
                    c.account.currentCharacter.CID);
                c.account.currentCharacter.BP = newBounty;
            }

            GunzPacket response = new GunzPacket(0x64, 0x714);
            response.startHeader();

            response.writeInt(result);

            response.finishHeader();
            c.send(response);
        }

        /// <summary>
        /// Attempts to sell an item from the given item.
        /// </summary>
        /// <param name="c">the client requesting</param>
        /// <param name="p">the request packet</param>
        public static void responseSellItem(Client c, GunzPacket p)
        {
            p.setIndex(11);

            UID uidPlayer;
            uidPlayer.first = (uint)p.readInt();
            uidPlayer.second = (uint)p.readInt();

            if (uidPlayer != c.uid)
                return;

            p.readInt();
            int CIID = p.readInt();
            int itemID = DynamicSQL.executeScalarInt("SELECT ItemID FROM Inventories WHERE CIID = " + CIID);
            uint bountyToAdd = (uint)(StaticSQL.executeScalarInt("SELECT Price FROM Items WHERE ItemID = " + itemID) / 4);

            c.account.currentCharacter.BP += bountyToAdd;
            DynamicSQL.executeQuery("DELETE FROM Inventories WHERE CIID = " + CIID);
            DynamicSQL.executeQuery("UPDATE Characters SET BP =  " + c.account.currentCharacter.BP + 
                " WHERE CID = " + c.account.currentCharacter.CID);

            GunzPacket response = new GunzPacket(0x64, 0x716);
            response.startHeader();
            response.writeInt(0x00);
            response.finishHeader();
            c.send(response);
        }

        /// <summary>
        /// Unequips an item from a character.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="p"></param>
        public static void responseTakeOffItem(Client c, GunzPacket p)
        {
            p.setIndex(11);

            UID uidPlayer = new UID();
            uidPlayer.first = (uint)p.readInt();
            uidPlayer.second = (uint)p.readInt();

            if (uidPlayer != c.uid)
                return;

            string[] slots = { "Head", "Chest", "Hands", "Legs", "Feet", "Ring1", "Ring2", "Melee", 
                               "Primary", "Secondary", "Custom1", "Custom2" };
            int itemSlot = p.readInt();

            switch (itemSlot)
            {
                case 0:
                    c.account.currentCharacter.equippedCIIDs.head = 0;
                    c.account.currentCharacter.equippedItemIDs.head = 0;
                    break;
                case 1:
                    c.account.currentCharacter.equippedCIIDs.chest = 0;
                    c.account.currentCharacter.equippedItemIDs.chest = 0;
                    break;
                case 2:
                    c.account.currentCharacter.equippedCIIDs.hands = 0;
                    c.account.currentCharacter.equippedItemIDs.hands = 0;
                    break;
                case 3:
                    c.account.currentCharacter.equippedCIIDs.legs = 0;
                    c.account.currentCharacter.equippedItemIDs.legs = 0;
                    break;
                case 4:
                    c.account.currentCharacter.equippedCIIDs.feet = 0;
                    c.account.currentCharacter.equippedItemIDs.feet = 0;
                    break;
                case 5:
                    c.account.currentCharacter.equippedCIIDs.ring1 = 0;
                    c.account.currentCharacter.equippedItemIDs.ring1 = 0;
                    break;
                case 6:
                    c.account.currentCharacter.equippedCIIDs.ring2 = 0;
                    c.account.currentCharacter.equippedItemIDs.ring2 = 0;
                    break;
                case 7:
                    c.account.currentCharacter.equippedCIIDs.melee = 0;
                    c.account.currentCharacter.equippedItemIDs.melee = 0;
                    break;
                case 8:
                    c.account.currentCharacter.equippedCIIDs.primaryWeapon = 0;
                    c.account.currentCharacter.equippedItemIDs.primaryWeapon = 0;
                    break;
                case 9:
                    c.account.currentCharacter.equippedCIIDs.secondaryWeapon = 0;
                    c.account.currentCharacter.equippedItemIDs.secondaryWeapon = 0;
                    break;
                case 10:
                    c.account.currentCharacter.equippedCIIDs.item1 = 0;
                    c.account.currentCharacter.equippedItemIDs.item1 = 0;
                    break;
                case 11:
                    c.account.currentCharacter.equippedCIIDs.item2 = 0;
                    c.account.currentCharacter.equippedItemIDs.item2 = 0;
                    break;
            }

            DynamicSQL.executeQuery("UPDATE Characters SET " + slots[itemSlot] + "ItemID = 0, " +
                slots[itemSlot] + "CIID = 0 WHERE CID = " + c.account.currentCharacter.CID);

            GunzPacket response = new GunzPacket(0x64, 0x722);
            response.startHeader();
            response.writeInt(0x00);
            response.finishHeader();
            c.send(response);
        }

        /// <summary>
        /// Attempts to equip the item onto the given character.
        /// </summary>
        /// <param name="c">the client requesting</param>
        /// <param name="p">the request packet</param>
        public static void responseEquipItem(Client c, GunzPacket p)
        {
            p.setIndex(11);

            UID uidPlayer = new UID();
            uidPlayer.first = (uint)p.readInt();
            uidPlayer.second = (uint)p.readInt();

            if (uidPlayer != c.uid)
                return;

            p.readInt();
            uint CIID = (uint)p.readInt();
            uint itemID = (uint)DynamicSQL.executeScalarInt("SELECT ItemID FROM Inventories WHERE CIID = " + CIID);

            string[] slots = { "Head", "Chest", "Hands", "Legs", "Feet", "Ring1", "Ring2", "Melee", 
                               "Primary", "Secondary", "Custom1", "Custom2" };
            int itemSlot = p.readInt();

            switch (itemSlot)
            {
                case 0:
                    c.account.currentCharacter.equippedCIIDs.head = CIID;
                    c.account.currentCharacter.equippedItemIDs.head = itemID;
                    break;
                case 1:
                    c.account.currentCharacter.equippedCIIDs.chest = CIID;
                    c.account.currentCharacter.equippedItemIDs.chest = 0;
                    break;
                case 2:
                    c.account.currentCharacter.equippedCIIDs.hands = CIID;
                    c.account.currentCharacter.equippedItemIDs.hands = itemID;
                    break;
                case 3:
                    c.account.currentCharacter.equippedCIIDs.legs = CIID;
                    c.account.currentCharacter.equippedItemIDs.legs = itemID;
                    break;
                case 4:
                    c.account.currentCharacter.equippedCIIDs.feet = CIID;
                    c.account.currentCharacter.equippedItemIDs.feet = itemID;
                    break;
                case 5:
                    c.account.currentCharacter.equippedCIIDs.ring1 = CIID;
                    c.account.currentCharacter.equippedItemIDs.ring1 = itemID;
                    break;
                case 6:
                    c.account.currentCharacter.equippedCIIDs.ring2 = CIID;
                    c.account.currentCharacter.equippedItemIDs.ring2 = itemID;
                    break;
                case 7:
                    c.account.currentCharacter.equippedCIIDs.melee = CIID;
                    c.account.currentCharacter.equippedItemIDs.melee = itemID;
                    break;
                case 8:
                    c.account.currentCharacter.equippedCIIDs.primaryWeapon = CIID;
                    c.account.currentCharacter.equippedItemIDs.primaryWeapon = itemID;
                    break;
                case 9:
                    c.account.currentCharacter.equippedCIIDs.secondaryWeapon = CIID;
                    c.account.currentCharacter.equippedItemIDs.secondaryWeapon = itemID;
                    break;
                case 10:
                    c.account.currentCharacter.equippedCIIDs.item1 = CIID;
                    c.account.currentCharacter.equippedItemIDs.item1 = itemID;
                    break;
                case 11:
                    c.account.currentCharacter.equippedCIIDs.item2 = CIID;
                    c.account.currentCharacter.equippedItemIDs.item2 = itemID;
                    break;
            }

            DynamicSQL.executeQuery("UPDATE Characters SET " + slots[itemSlot] + "ItemID = " + itemID + ", " +
                slots[itemSlot] + "CIID = " + CIID + " WHERE CID = " + c.account.currentCharacter.CID);

            GunzPacket response = new GunzPacket(0x64, 0x722);
            response.startHeader();
            response.writeInt(0x00);
            response.finishHeader();
            c.send(response);
        }
    }
}
