using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using Assignment_1.Models;

namespace Assignment_1.DAL
{
    public class AreaInterestDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public AreaInterestDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "CJP_DBString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }

        public List<AreaInterest> GetAllAreaInterest()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all competitions
            cmd.CommandText = @"SELECT * FROM AreaInterest";
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a branch list
            List<AreaInterest> areaInterestList = new List<AreaInterest>();
            while (reader.Read())
            {
                areaInterestList.Add(
                    new AreaInterest
                    {
                        AreaInterestID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();

            return areaInterestList;
        }

        public AreaInterest GetDetails(int areaInterestId)
        {
            AreaInterest areaInterest = new AreaInterest();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that 
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM AreaInterest
                                WHERE AreaInterestID = @selectedAreaInterestID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectedAreaInterestID", areaInterestId);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill staff object with values from the data reader 
                    areaInterest.AreaInterestID = areaInterestId;
                    areaInterest.Name = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return areaInterest;
        }

        public int Add(AreaInterest areaInterest)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO AreaInterest (Name) 
                                OUTPUT INSERTED.AreaInterestID 
                                VALUES(@name)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@name", areaInterest.Name);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            areaInterest.AreaInterestID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return areaInterest.AreaInterestID;
        }

        public int Delete(int areaInterestId)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM AreaInterest
                                WHERE AreaInterestID = @selectAreaInterestID";
            cmd.Parameters.AddWithValue("@selectAreaInterestID", areaInterestId);
            conn.Open();
            int rowAffected = 0;
            rowAffected += cmd.ExecuteNonQuery();
            conn.Close();
            return rowAffected;
        }

        public bool IsAreaInterestNameExist(string name, int id)
        {
            bool nameFound = false;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT AreaInterestID FROM AreaInterest 
                                WHERE Name=@selectedName";
            cmd.Parameters.AddWithValue("@selectedName", name);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != id)
                        //The email address is used by another staff
                        nameFound = true;
                }
            }
            else
            { //No record
                nameFound = false; // The email address given does not exist
            }
            reader.Close();
            conn.Close();

            return nameFound;
        }
    }
}
