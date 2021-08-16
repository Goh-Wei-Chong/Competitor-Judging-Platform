using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;
using System.Diagnostics;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.DAL
{
    public class CompetitionScoreDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        

        public CompetitionScoreDAL()
        {
            
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "CJP_DBString");

            
            conn = new SqlConnection(strConn);
        }
        public List<CompetitionScore> GetAllCompetitionScore()
        {
            
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"select Criteria.CriteriaName, Criteria.CriteriaID, Competition.CompetitionName,Competition.CompetitionID, Competitor.CompetitorName,Competitor.CompetitorID, Score
                                from competitionscore

                inner join criteria on competitionscore.criteriaid = criteria.CriteriaID
                inner join competition on competitionscore.CompetitionID = Competition.CompetitionID
                inner join competitor on competitionscore.CompetitorID = competitor.CompetitorID ";

            

            
            conn.Open();

            
            SqlDataReader reader = cmd.ExecuteReader();
            
            List<CompetitionScore> competitionscoreList = new List<CompetitionScore>();
            while (reader.Read())
            {
                competitionscoreList.Add(
                new CompetitionScore
                {
                    CriteriaName = reader.GetString(0), 
                    CompetitorName  = reader.GetString(4), 
                    CompetitionId = reader.GetInt32(3),     
                    CriteriaId = reader.GetInt32(1),
                    CompetitorId = reader.GetInt32(5),
                    Score = reader.GetInt32(6), 

                }
                );

            }

           

            
            reader.Close();
            
            conn.Close();
            return competitionscoreList;
        }

        //get competitionscore details to edit 
        public CompetitionScore GetDetails(int criteriaId, int competitorId, int competitionId)
        {
            

            CompetitionScore competitionscore = new CompetitionScore();

            SqlCommand cmd = conn.CreateCommand();

                  
            cmd.CommandText = @"SELECT * FROM CompetitionScore WHERE CriteriaID = @selectedCriteriaID 
                                                                AND CompetitorID = @selectedCompetitorID 
                                                                AND CompetitionID = @selectedCompetitionID";


            cmd.Parameters.AddWithValue("@selectedCriteriaID", criteriaId);
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitorId);
            cmd.Parameters.AddWithValue("@selectedCompetitionID", competitionId);

            
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    competitionscore.CriteriaId = criteriaId;
                    competitionscore.CompetitorId = competitorId;
                    competitionscore.CompetitionId = competitionId;


                }
            }
            reader.Close();

            conn.Close();

            Debug.WriteLine(competitionscore.CriteriaId.ToString());
            Debug.WriteLine(competitionscore.CompetitorId.ToString());
            Debug.WriteLine(competitionscore.CompetitorId.ToString());
            return competitionscore;
        }

        //UPDATE COMPETITION SCORE FOR SELECTED COMPETITOR
        public int Update(CompetitionScore competitionscore)
        {
            SqlCommand cmd = conn.CreateCommand();

           

            cmd.CommandText = @"UPDATE CompetitionScore SET Score = @score WHERE CriteriaId = @selectedCriteriaID AND
                                                                                  CompetitorId = @selectedCompetitorID AND
                                                                                    CompetitionId = @selectedCompetitionID";

            Debug.WriteLine(competitionscore.CriteriaId);
            Debug.WriteLine(competitionscore.CompetitionId);
            Debug.WriteLine(competitionscore.CompetitorId);


            
            cmd.Parameters.AddWithValue("@selectedCriteriaID", competitionscore.CriteriaId);
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitionscore.CompetitorId);
            cmd.Parameters.AddWithValue("@selectedCompetitionID", competitionscore.CompetitionId);
            cmd.Parameters.AddWithValue("@score", competitionscore.Score);

            conn.Open();

            int count = cmd.ExecuteNonQuery();

            conn.Close();

            return count;

        }

        
    }
}

