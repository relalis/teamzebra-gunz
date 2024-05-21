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
using System.Data.SqlClient;

using Zebra.Miscellaneous;

namespace Zebra.DatabaseInteraction
{
    /// <summary>
    /// Class that contains all the code necessary to
    /// interact with the SQL database.
    /// </summary>
    public static class SQLConnector
    {
        /// <summary>
        /// the object used to connect to the SQL server
        /// </summary>
        public static SqlConnection mainConnection;

        /// <summary>
        /// the object used to execute the current 
        /// query
        /// </summary>
        public static SqlCommand myQuery;

        /// <summary>
        /// starts the SQL server connection
        /// </summary>
        /// <returns>whether the connection was successful</returns>
        public static bool startConnection()
        {
            string connectionString = "user id=" + Globals.SQLlogin +
                        ";password=" + Globals.SQLpassword +
                        ";server=" + Globals.SQLserver +
                        ";Trusted_Connection=" + Globals.SQLwindowsauth +
                        ";database=" + Globals.SQLdatabase +
                        ";connection timeout=" + Globals.SQLtimeout;

            mainConnection = new SqlConnection(connectionString);

            try
            {
                mainConnection.Open();

                myQuery = new SqlCommand("", mainConnection);
                myQuery.Connection = mainConnection;

                Console.WriteLine("   Connected to SQL server successfully!");
                Console.WriteLine("\tServer: " + Globals.SQLserver);
                Console.WriteLine("\tDatabase: " + Globals.SQLdatabase);

                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine("   Error in connecting to SQL server!\n" +
                    error.Message);
                return false;
            }
        }

        /// <summary>
        /// Closes the SQL server connection.
        /// </summary>
        /// <returns>whether the disconnect was successful</returns>
        public static bool closeConnection()
        {
            try
            {
                mainConnection.Close();
                Console.WriteLine("   Connection closed successfully!\n");
                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine("   Error in closing SQL connection:\n\n" +
                    error.Message);
                return false;
            }
        }

        /// <summary>
        /// Executes the given SQL query, can't execute scalars.
        /// </summary>
        /// <param name="query">the query to be executed</param>
        /// <returns>whether or not the query was successfully executed</returns>
        public static bool executeQuery(string query)
        {
            try
            {
                myQuery.CommandText = query;
                myQuery.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Executes a given query and returns the integer
        /// returned by it.
        /// </summary>
        /// <param name="query">the query to be executed</param>
        /// <returns>integer returned by the query</returns>
        public static int executeScalarInt(string query)
        {
            Int32 ret = 0x00;

            try
            {
                myQuery.CommandText = query;
                ret = (Int32)myQuery.ExecuteScalar();
            }
            catch
            {
                return 0;
            }

            return (int)ret;
        }

        /// <summary>
        /// Executes a given query and returns the string
        /// returned by it.
        /// </summary>
        /// <param name="query">the query to be executed</param>
        /// <returns>string returned by the query</returns>
        public static string executeScalarString(string query)
        {
            string ret = "";

            try
            {
                myQuery.CommandText = query;
                ret = (string)myQuery.ExecuteScalar();
            }
            catch
            {
                return "";
            }

            return ret;
        }

        /// <summary>
        /// Executes a given query and returns the byte
        /// returned by it.
        /// </summary>
        /// <param name="query">the query to be executed</param>
        /// <returns>byte returned by the query</returns>
        public static byte executeScalarByte(string query)
        {
            byte ret = 0x00;

            try
            {
                myQuery.CommandText = query;
                ret = (byte)myQuery.ExecuteScalar();
            }
            catch
            {
                return 0;
            }

            return ret;
        }

        /// <summary>
        /// Executes a given query and returns the short
        /// returned by it.
        /// </summary>
        /// <param name="query">the query to be executed</param>
        /// <returns>short returned by the query</returns>
        public static short executeScalarShort(string query)
        {
            short ret = 0x00;

            try
            {
                myQuery.CommandText = query;
                ret = (short)myQuery.ExecuteScalar();
            }
            catch
            {
                return 0;
            }

            return ret;
        }
    }
}
