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
    public class CompetitionSubmissionDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public CompetitionSubmissionDAL()
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

        public List<CompetitionSubmission> GetAllCompetitionSubmission()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all competitions
            cmd.CommandText = @"SELECT * FROM CompetitionSubmission";
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a branch list
            List<CompetitionSubmission> competitionSubmissionList = new List<CompetitionSubmission>();
            while (reader.Read())
            {
                competitionSubmissionList.Add(
                    new CompetitionSubmission
                    {
                        CompetitionID = reader.GetInt32(0),
                        CompetitorID = reader.GetInt32(1),
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();

            return competitionSubmissionList;
        }
    }
}
