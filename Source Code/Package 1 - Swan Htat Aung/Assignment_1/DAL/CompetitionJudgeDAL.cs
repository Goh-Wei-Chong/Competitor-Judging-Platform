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
    public class CompetitionJudgeDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public CompetitionJudgeDAL()
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

        public List<CompetitionJudge> GetAllCompetitionJudge()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all competitions
            cmd.CommandText = @"SELECT * FROM CompetitionJudge";
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a branch list
            List<CompetitionJudge> competitionJudgeList = new List<CompetitionJudge>();
            while (reader.Read())
            {
                competitionJudgeList.Add(
                    new CompetitionJudge
                    {
                        CompetitionID = reader.GetInt32(0),
                        JudgeID = reader.GetInt32(1),
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();

            return competitionJudgeList;
        }

        public int Add(CompetitionJudge competitionJudge)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO CompetitionJudge (CompetitionID, JudgeID) 
            VALUES(@competitionId,@judgeId)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@competitionId", competitionJudge.CompetitionID);
            cmd.Parameters.AddWithValue("@judgeId", competitionJudge.JudgeID);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //A connection should be closed after operations.
            cmd.ExecuteNonQuery();
            conn.Close();
            //Return id when no error occurs.
            return competitionJudge.CompetitionID;
        }

        public CompetitionJudge CreateTemp(int id)
        {
            CompetitionJudge competitionJudge = new CompetitionJudge();
            competitionJudge.CompetitionID = id;
            competitionJudge.JudgeID = null;
            return competitionJudge;
        }
    }
}
