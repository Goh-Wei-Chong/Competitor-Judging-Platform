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
    public class CompetitionDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public CompetitionDAL()
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

        public List<Competition> GetAllCompetitions()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all competitions
            cmd.CommandText = @"SELECT * FROM Competition";
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a branch list
            List<Competition> competitionList = new List<Competition>();
            while (reader.Read())
            {
                competitionList.Add(
                    new Competition
                    {
                        CompetitionID = reader.GetInt32(0),
                        AreaInterestID = reader.GetInt32(1), 
                        CompetitionName = reader.GetString(2),
                        StartDate = reader.GetDateTime(3),
                        EndDate = reader.GetDateTime(4),
                        ResultReleasedDate = reader.GetDateTime(5),
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();

            return competitionList;
        }

        public Competition GetDetails(int competitionId)
        {
            Competition competition = new Competition();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that 
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM Competition
                                WHERE CompetitionID = @selectedCompetitionID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectedCompetitionID", competitionId);
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
                    competition.CompetitionID = competitionId;
                    competition.AreaInterestID = (int)(!reader.IsDBNull(1) ? reader.GetInt32(1) : (int?)null);
                    competition.CompetitionName = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    competition.StartDate = !reader.IsDBNull(3) ? reader.GetDateTime(3) : (DateTime?)null;
                    competition.EndDate = !reader.IsDBNull(4) ? reader.GetDateTime(4) : (DateTime?)null;
                    competition.ResultReleasedDate = !reader.IsDBNull(5) ? reader.GetDateTime(5) : (DateTime?)null;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return competition;
        }

        public int Add(Competition competition)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Competition (AreaInterestID, CompetitionName, StartDate, 
                                EndDate, ResultReleasedDate) 
                                OUTPUT INSERTED.CompetitionID
                                VALUES(@areaInterestID, @competitionName, @startDate, 
                                @endDate, @resultReleaseDate)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@areaInterestID", competition.AreaInterestID);
            cmd.Parameters.AddWithValue("@competitionName", competition.CompetitionName);
            cmd.Parameters.AddWithValue("@startDate", competition.StartDate);
            cmd.Parameters.AddWithValue("@endDate", competition.EndDate);
            cmd.Parameters.AddWithValue("@resultReleaseDate", competition.ResultReleasedDate);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            competition.CompetitionID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return competition.CompetitionID;
        }

        public int Update(Competition competition)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE Competition SET
                                AreaInterestID=@areaInterestID, CompetitionName=@competitionName,
                                StartDate=@startDate, EndDate=@endDate, ResultReleasedDate=@resultReleaseDate 
                                WHERE CompetitionID = @selectedCompetitionID";
            cmd.Parameters.AddWithValue("@areaInterestID", competition.AreaInterestID);
            cmd.Parameters.AddWithValue("@competitionName", competition.CompetitionName);
            cmd.Parameters.AddWithValue("@startDate", competition.StartDate);
            cmd.Parameters.AddWithValue("@endDate", competition.EndDate);
            cmd.Parameters.AddWithValue("@resultReleaseDate", competition.ResultReleasedDate);
            cmd.Parameters.AddWithValue("@selectedCompetitionID", competition.CompetitionID);
            conn.Open();
            int count = cmd.ExecuteNonQuery();
            conn.Close();
            return count;
        }

        public bool IsCompetitionNameExist(string competitionName, int competitionID)
        {
            bool nameFound = false;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT CompetitionID FROM Competition 
                                WHERE competitionName=@selectedCompetitionName";
            cmd.Parameters.AddWithValue("@selectedCompetitionName", competitionName);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != competitionID)
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
