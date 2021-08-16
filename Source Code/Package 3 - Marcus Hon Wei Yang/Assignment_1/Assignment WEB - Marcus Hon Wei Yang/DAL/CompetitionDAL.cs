using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.DAL
{
    public class CompetitionDAL
    {
        private IConfiguration Configuration { get; set; }
        private SqlConnection conn;
        //Constructor
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

        public List<Competition> GetAllCompetition()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the SQL statement that select all branches
            cmd.CommandText = @"SELECT * FROM Competition ORDER BY CompetitionID";

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
                    CompetitionId = reader.GetInt32(0), // 0 - 1st column
                    CompetitionName = reader.GetString(2), // 1 - 2nd column
                    
                    ResultReleasedDate = reader.GetDateTime(5), // 2 - 3rd column
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();

            return competitionList;
        }

        public List<Criteria> GetCompetitionCriteria(int competitionId)
        {
            
            SqlCommand cmd = conn.CreateCommand();
           
            cmd.CommandText = @"SELECT * FROM Criteria WHERE CompetitionID = @selectCompetition";

            cmd.Parameters.AddWithValue("@selectCompetition", competitionId);

            //Open a database connection
            conn.Open();
            
            SqlDataReader reader = cmd.ExecuteReader();
            

            List < Criteria > criteriaList = new List<Criteria>();
            while (reader.Read())
            {
                criteriaList.Add(
                new Criteria
                {
                    CriteriaId = reader.GetInt32(0), 
                    CriteriaName = reader.GetString(2), 
                    Weightage = reader.GetInt32(3), 
                    CompetitionId = reader.GetInt32(1) ,
                }
              );
            }
            
            reader.Close();
            
            conn.Close();

            return criteriaList;
        }

        

        public List<CompetitionScore> GetCompetitionCompetitionScore(int competitionId)
        {
            
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"select Criteria.criterianame,Competition.Competitionname, Competition.CompetitionID, Competitor.CompetitorName, Competitor.CompetitorID, Score 
                                from competitionscore

								

                inner join criteria on competitionscore.criteriaid = criteria.CriteriaID
                inner join competition on competitionscore.CompetitionID = Competition.CompetitionID
                inner join competitor on competitionscore.CompetitorID = competitor.CompetitorID
				where CompetitionScore.CompetitionID = @selectCompetition";

            cmd.Parameters.AddWithValue("@selectCompetition", competitionId);

            
            conn.Open();
            
            SqlDataReader reader = cmd.ExecuteReader();
            

            List<CompetitionScore> competitionscoreList = new List<CompetitionScore>();
            while (reader.Read())
            {
                competitionscoreList.Add(
                new CompetitionScore
                {
                    CompetitorId = reader.GetInt32(2), 
                    CompetitorName = reader.GetString(3), 
                    CriteriaName = reader.GetString(0),
                    Score = reader.GetInt32(5),                   
                }
              );
            }
            
            reader.Close();
            
            conn.Close();

            return competitionscoreList;
        }
    }
}
