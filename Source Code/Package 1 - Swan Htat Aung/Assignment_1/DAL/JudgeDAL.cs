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
    public class JudgeDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public JudgeDAL()
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

        public List<Judge> GetAllJudge()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Judge ORDER BY JudgeID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Judge> judgeList = new List<Judge>();
            while (reader.Read())
            {
                judgeList.Add(
                    new Judge
                    {
                        JudgeID = reader.GetInt32(0), //0: 1st column
                        JudgeName = reader.GetString(1), //1: 2nd column
                        Salutation = reader.GetString(2), //2: 3rd column
                        AreaInterestID = reader.GetInt32(3),
                        EmailAddr = reader.GetString(4), //3: 4th column
                        Password = reader.GetString(5), //5: 6th column
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();

            return judgeList;
        }
    }
}
