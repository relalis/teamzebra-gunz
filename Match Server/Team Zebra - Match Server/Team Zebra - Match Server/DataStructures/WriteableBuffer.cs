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
using System.Collections.Generic;
using System.Text;

namespace Zebra.DataStructures
{
    /// <summary>
    /// Represents a writeable buffer of bytes.
    /// </summary>
    public class WriteableBuffer
    {
        /// <summary>
        /// the underlying array of bytes for this buffer
        /// </summary>
        public byte[] data;

        /// <summary>
        /// the current index of the buffer's array
        /// </summary>
        public int index;

        /// <summary>
        /// the length of the buffer
        /// </summary>
        public int length;

        /// <summary>
        /// Constructor.
        /// </summary>
        public WriteableBuffer()
        {
            data = new byte[15];
            index = 0;
            length = data.GetLength(0);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size">size of the buffer to be made</param>
        public WriteableBuffer(int size)
        {
            data = new byte[size];
            index = 0;
            length = data.GetLength(0);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="arr">an array of bytes to make the buffer</param>
        public WriteableBuffer(byte[] arr)
        {
            data = new byte[arr.Length];
            arr.CopyTo(data, 0);
            index = data.Length;
            length = data.Length;
        }

        /// <summary>
        /// Resize the buffer.
        /// </summary>
        /// <param name="deltaSize">the amount to add to the buffer's length</param>
        private void resize()
        {
            byte[] newbuffer = new byte[length * 2];
            data.CopyTo(newbuffer, 0);
            data = newbuffer;
            length = data.Length;
        }

        /// <summary>
        /// Returns the underlying array of the buffer.
        /// </summary>
        /// <returns>the buffer's byte array</returns>
        protected byte[] getBuffer()
        {
            return data;
        }

        /// <summary>
        /// Returns the part of the array that's been written to.
        /// </summary>
        /// <returns>the written block of data</returns>
        public byte[] getWrittenBuffer()
        {
            try
            {
                if (index > length)
                {
                    byte[] result = new byte[index - 1];
                    System.Buffer.BlockCopy(data, 0, result, 0, index - 1);
                    return result;
                }
                else
                {
                    byte[] result = new byte[index];
                    System.Buffer.BlockCopy(data, 0, result, 0, index);
                    return result;
                }
            }
            catch
            {
                Console.WriteLine("Index is	out	of bound. Index	= {0} Length = {1} ", index, length);
                return new byte[1];
            }
        }

        /// <summary>
        /// Set the buffer's current index.
        /// </summary>
        /// <param name="index">the desired index</param>
        /// <returns>whether or not the reset was valid</returns>
        public bool setIndex(int index)
        {
            if (index > length) return false;
            this.index = index;
            return true;
        }

        /// <summary>
        /// Add the given value to the buffer's current index.
        /// </summary>
        /// <param name="index">the desired value to be added</param>
        /// <returns>whether or not the reset was successful</returns>
        public bool addIndex(int index)
        {
            return setIndex(this.index + index);
        }

        /// <summary>
        /// Reset's the buffer's current index to 0.
        /// </summary>
        public void resetIndex()
        {
            setIndex(0);
        }

        /// <summary>
        /// Writes a byte to the buffer.
        /// </summary>
        /// <param name="b">the byte to be written</param>
        public void writeByte(byte b)
        {
            if (index >= length)
                resize();
            data[index] = b;
            ++index;
        }

        /// <summary>
        /// Writes an unsigned short to the buffer.
        /// </summary>
        /// <param name="s">the value to be written</param>
        public void writeShort(ushort s)
        {
            if (index + 2 >= length)
                resize();
            data[index] = (byte)(s);
            ++index;
            data[index] = (byte)(s / 256);
            ++index;
        }

        /// <summary>
        /// Writes an unsigned integer to the buffer.
        /// </summary>
        /// <param name="i">the value to be written</param>
        public void writeInt(uint i)
        {
            if (index + 4 >= length)
                resize();

            data[index] = (byte)(i);
            index++;

            data[index] = (byte)(i / 256);
            index++;

            data[index] = (byte)(i / 65536);
            index++;

            data[index] = (byte)(i / 16777216);
            index++;
        }

        /// <summary>
        /// Writes an unsigned long integer to the buffer.
        /// </summary>
        /// <param name="l">the value to be written</param>
        public void writeLong(ulong l)
        {
            byte[] bytes = BitConverter.GetBytes(l);
            writeArray(bytes);
        }

        /// <summary>
        /// Pad the buffer with a given value a given number of times.
        /// </summary>
        /// <param name="pad">the byte to pad with</param>
        /// <param name="len">the amount of padding</param>
        public void bytePad(byte pad, int len)
        {
            if ((index + len) >= length)
                resize();

            for (int i = 0; i < len; i++)
            {
                data[index + i] = pad;
            }

            index += len;
        }

        /// <summary>
        /// Write a string to the buffer.
        /// </summary>
        /// <param name="s">the string to be written</param>
        public void writeStringNoLen(string s)
        {
            if ((index + s.Length + 1) >= length)
                resize();

            for (int i = 0; i < s.Length; i++)
            {
                data[index + i] = (byte)s[i];
            }

            index += s.Length;
            writeByte(0x00);
        }

        /// <summary>
        /// Write a string to the buffer followed by a ushort
        /// indicating its length.
        /// </summary>
        /// <param name="s">the string to be written</param>
        public void writeString(string s)
        {
            if ((index + s.Length + 3) >= length)
                resize();

            writeShort((ushort)(s.Length + 2));
            writeStringNoLen(s);
        }

        /// <summary>
        /// Write a byte array to the buffer.
        /// </summary>
        /// <param name="arr">the array to be written</param>
        public void writeArray(byte[] arr)
        {
            if ((index + arr.Length) >= length)
                resize();

            for (int i = 0; i < arr.Length; i++)
            {
                data[index + i] = arr[i];
            }

            index += arr.Length;
        }

        /// <summary>
        /// Read a byte from the buffer.
        /// </summary>
        /// <returns>the next byte in the buffer</returns>
        public byte readByte()
        {
            try
            {
                byte result = data[index];
                index++;
                return result;
            }
            catch
            {
                Console.WriteLine("Trying to read out of data boundaries. Size = {0} ReadIndex = {1} ReadSize =	2", length, index);
                return 0;
            }
        }

        /// <summary>
        /// Read a short from the buffer.
        /// </summary>
        /// <returns>the next short (2 bytes) of the buffer</returns>
        public ushort readShort()
        {
            try
            {
                ushort result = (ushort)((data[index]) + (data[index + 1] * 256));
                index += 2;
                return result;
            }
            catch
            {
                Console.WriteLine("Trying to read out of data boundaries. Size = {0} ReadIndex = {1} ReadSize =	2", length, index);
                return 0;
            }
        }

        /// <summary>
        /// Read a short from a given position.
        /// </summary>
        /// <param name="pos">the position to read from</param>
        /// <returns>the short at the given position</returns>
        public ushort readShort(ushort pos)
        {
            int tmpIndex = index;
            index = pos;
            ushort result = readShort();
            index = tmpIndex;

            return result;
        }

        /// <summary>
        /// Reads the next integer from the buffer.
        /// </summary>
        /// <returns>the next integer (4 bytes) from the buffer</returns>
        public int readInt()
        {
            try
            {
                int result = (data[index]) + (data[index + 1] * 256) + 
                    (data[index + 2] * 65536) + (data[index + 3] * 16777216);
                index += 4;
                return result;
            }
            catch
            {
                Console.WriteLine("Trying to read out of data boundaries. Size = {0} ReadIndex = {1} ReadSize =	4", length, index);
                return 0;
            }
        }

        /// <summary>
        /// Reads the next integer at the given position.
        /// </summary>
        /// <param name="pos">the position to read from</param>
        /// <returns>the integer at the given position</returns>
        public int readInt(int pos)
        {
            int tmpIndex = index;
            index = pos;
            int result = readInt();
            index = tmpIndex;
            return result;
        }

        /// <summary>
        /// Returns a string of the given length from the buffer.
        /// </summary>
        /// <param name="len">the length to read</param>
        /// <returns>the string with the given length</returns>
        public string readString(int len)
        {
            char[] newbuf = new char[len];

            try
            {
                for (int i = 0; i < len; i++)
                {
                    newbuf[i] = (char)data[index + i];
                }

                index += len + 1;
                return new string(newbuf);
            }
            catch
            {
                Console.WriteLine("Trying to read out of data boundaries. Size = {0} ReadIndex = {1} ReadSize =	{2}", length, index, len);
                return "";
            }
        }

        /// <summary>
        /// Reads and returns a string based on the next short
        /// in the buffer as its length.
        /// </summary>
        /// <returns>the string that was read</returns>
        public string readStringFromLength()
        {
            int len = readShort() - 2;
            return readString(len);
        }

        /// <summary>
        /// Reads and returns a string based on the next short
        /// in the buffer as its length from the given position.
        /// </summary>
        /// <param name="pos">the position to read from</param>
        /// <returns>the string that was read</returns>
        public string readStringFromLength(int pos)
        {
            int tmpIndex = index;
            index = pos;
            string result = readStringFromLength();
            index = tmpIndex;
            return result;
        }

        /// <summary>
        /// Reads a string from the buffer given its position
        /// and length.
        /// </summary>
        /// <param name="pos">position to read from</param>
        /// <param name="len">length of the string to be read</param>
        /// <returns>the string that was read</returns>
        public string readStringSpecify(int pos, int len)
        {
            int TempIndex = index;
            index = pos;
            string result = readString(len);
            index = TempIndex;
            return result;
        }
    }
}
