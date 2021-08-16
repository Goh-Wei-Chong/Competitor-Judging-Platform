using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;


namespace Assignment_WEB___Marcus_Hon_Wei_Yang.DAL
{
    public class CompetitionSubmissionDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        
        public CompetitionSubmissionDAL()
        {
            
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "CJP_DBString");
            
            conn = new SqlConnection(strConn);
        }

        //get all competition submissions

        public List<CompetitionSubmission> GetAllCompetitionSubmission()
        {
            
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"select CompetitionSubmission.CompetitionID, CompetitionSubmission.CompetitorID, CompetitionSubmission.FileSubmitted, CompetitionSubmission.DateTimeFileUpload, CompetitionSubmission.Appeal, CompetitionSubmission.VoteCount, CompetitionSubmission.Ranking,Competitor.CompetitorName from competitionsubmission
                                inner join competitor on competitor.CompetitorID = CompetitionSubmission.CompetitorID";

           
            conn.Open();           
            SqlDataReader reader = cmd.ExecuteReader();
            
            List<CompetitionSubmission> competitionsubmissionList = new List<CompetitionSubmission>();
            while (reader.Read())
            {
                competitionsubmissionList.Add(
                new CompetitionSubmission
                {
                    CompetitionId = reader.GetInt32(0),
                    CompetitorId = reader.GetInt32(1),
                    FileSubmitted = !reader.IsDBNull(2) ? reader.GetString(2) : null,
                    DateTimeFileUpload = !reader.IsDBNull(3) ? reader.GetDateTime(3) : (DateTime?)null,
                    Appeal = !reader.IsDBNull(4) ?
                                reader.GetString(4) : null,
                    VoteCount = reader.GetInt32(5),
                    Ranking = !reader.IsDBNull(6) ?
                                reader.GetInt32(6) : (int?)null,
                    CompetitorName = reader.GetString(7),
                }
               );               
            }           
            reader.Close();           
            conn.Close();
            return competitionsubmissionList;
        }



        //GET TOTAL SCORE FOR EACH COMPETITOR
        public CompetitionSubmission GetDetails(int competitionId, int competitorId )
        {
            
            SqlCommand cmd = conn.CreateCommand();
            CompetitionSubmission competitionsubmissions = new CompetitionSubmission();
            
            cmd.CommandText = @"SELECT  CompetitionScore.CompetitionID , CompetitionScore.CompetitorID,
                                SUM(Criteria.Weightage*CompetitionScore.Score/10)as 'TotalScore' from Criteria
                                 INNER JOIN CompetitionScore ON CompetitionScore.CriteriaID = Criteria.CriteriaID        
                                    WHERE CompetitionScore.CompetitorID = @selectCompetitor  AND CompetitionScore.CompetitionID = @selectCompetition
                                    GROUP BY CompetitionScore.CompetitionID, CompetitionScore.CompetitorID ";

            cmd.Parameters.AddWithValue("@selectCompetition", competitionId);
            cmd.Parameters.AddWithValue("@selectCompetitor", competitorId);
            
            conn.Open();    
            
            SqlDataReader reader = cmd.ExecuteReader();
           
            while (reader.Read())
            {
                
                {
                    competitionsubmissions.CompetitionId = competitionId;
                    competitionsubmissions.CompetitorId = competitorId;               
                    competitionsubmissions.TotalScore = reader.GetInt32(2);
                };
            }
        
            reader.Close();           
            conn.Close();
            return competitionsubmissions;
        }


        //edit ranking for competitor in competition
        public int Update(CompetitionSubmission competitionsubmission)
        {
            SqlCommand cmd = conn.CreateCommand();



            cmd.CommandText = @"UPDATE CompetitionSubmission SET Ranking = @ranking WHERE 
                                                                                  CompetitorId = @selectedCompetitorID AND
                                                                                    CompetitionId = @selectedCompetitionID";

            
            Debug.WriteLine(competitionsubmission.CompetitionId);
            Debug.WriteLine(competitionsubmission.CompetitorId);



            
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitionsubmission.CompetitorId);
            cmd.Parameters.AddWithValue("@selectedCompetitionID", competitionsubmission.CompetitionId);
            cmd.Parameters.AddWithValue("@ranking", competitionsubmission.Ranking);

            conn.Open();

            int count = cmd.ExecuteNonQuery();

            conn.Close();

            return count;

        }

    }
}
