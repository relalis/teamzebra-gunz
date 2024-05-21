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
using System.Data.SqlClient;

using Zebra.Miscellaneous;

namespace Zebra.DatabaseInteraction
{
    /// <summary>
    /// Class that contains all the code necessary to
    /// interact with the SQL database.
    /// </summary>
    public static class StaticSQL
    {
        /// <summary>
        /// the object used to connect to the SQL server
        /// </summary>
        private static SqlConnection mainConnection;

        /// <summary>
        /// the object used to execute the current 
        /// query
        /// </summary>
        private static SqlCommand currQuery;

        /// <summary>
        /// reads the data from the database
        /// </summary>
        private static SqlDataReader dataReader;

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
                        ";database=" + Globals.SQLdatabaseStatic +
                        ";connection timeout=" + Globals.SQLtimeout;

            mainConnection = new SqlConnection(connectionString);

            try
            {
                mainConnection.Open();

                currQuery = new SqlCommand("", mainConnection);
                currQuery.Connection = mainConnection;

                Console.WriteLine("   Connected to static database successfully!");
                Console.WriteLine("\tServer: " + Globals.SQLserver);
                Console.WriteLine("\tDatabase: " + Globals.SQLdatabaseStatic);

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
        /// <returns>the number of rows affected</returns>
        public static int executeQuery(string query)
        {
            try
            {
                currQuery.CommandText = query;
                return currQuery.ExecuteNonQuery();
            }
            catch
            {
                return 0;
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
            int ret = 0;

            try
            {
                currQuery.CommandText = query;
                ret = (int)currQuery.ExecuteScalar();
            }
            catch
            {
                return 0;
            }

            return ret;
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
                currQuery.CommandText = query;
                ret = (string)currQuery.ExecuteScalar();
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
                currQuery.CommandText = query;
                ret = (byte)currQuery.ExecuteScalar();
            }
            catch (Exception e)
            {
                ConsoleOutput.writeLineWithTimeStamp(e.Message);
                return 0x00;
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
                currQuery.CommandText = query;
                ret = (short)currQuery.ExecuteScalar();
            }
            catch
            {
                return 0x00;
            }

            return ret;
        }

        /// <summary>
        /// Selects multiple values from the database. Should select
        /// from only one column at a time.
        /// </summary>
        /// <param name="query">the query to be executed</param>
        /// <returns>array returned by the query</returns>
        public static ArrayList executeScalarArray(string query)
        {
            ArrayList ret = new ArrayList();

            try
            {
                currQuery.CommandText = query;
                dataReader = currQuery.ExecuteReader();

                while (dataReader.Read())
                {
                    ret.Add(dataReader[0]);
                }

                dataReader.Close();
            }
            catch
            {
                return null;
            }

            return ret;
        }
    }
}
