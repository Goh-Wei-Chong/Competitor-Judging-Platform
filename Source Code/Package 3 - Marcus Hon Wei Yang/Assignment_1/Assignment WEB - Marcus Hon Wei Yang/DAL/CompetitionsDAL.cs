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
    public class CompetitionsDAL
    {
        private IConfiguration Configuration { get; set; }
        

        private SqlConnection conn;
        //Constructor
        public CompetitionsDAL()
        {
            
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "CJP_DBString");
            
            conn = new SqlConnection(strConn);
        }

        public List<Competitions> GetAllCompetitions()
        {
            
            SqlCommand cmd = conn.CreateCommand();

            
            cmd.CommandText = @"SELECT * FROM Competition ORDER BY CompetitionID";

            
            conn.Open();

            
            SqlDataReader reader = cmd.ExecuteReader();

            
            List<Competitions> competitionsList = new List<Competitions>();
            while (reader.Read())
            {
                competitionsList.Add(
                new Competitions
                {
                    CompetitionId = reader.GetInt32(0),
                    CompetitionName = reader.GetString(2),
                    StartDate = reader.GetDateTime(3),
                    EndDate = reader.GetDateTime(4),
                    ResultReleasedDate = reader.GetDateTime(5),
                }
                ) ;
            }
            
            reader.Close();
            
            conn.Close();

            return competitionsList;
        }

        public List<CompetitionSubmission> GetCompetitionsCompetitionSubmission(int competitionId)
        {
            
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"select  CompetitionSubmission.CompetitorID, CompetitionSubmission.FileSubmitted, CompetitionSubmission.DateTimeFileUpload, CompetitionSubmission.Appeal,CompetitionSubmission.VoteCount,CompetitionSubmission.Ranking, Competitor.CompetitorName
                                    from competitionsubmission
                                    inner join competitor on CompetitionSubmission.CompetitorID = Competitor.CompetitorID
                                    WHERE competitionID = @selectedCompetition";
            
            cmd.Parameters.AddWithValue("@selectedCompetition", competitionId);

            
            conn.Open();
            
            SqlDataReader reader = cmd.ExecuteReader();
            List<CompetitionSubmission> competitionsubmissionList = new List<CompetitionSubmission>();
            while (reader.Read())
            {
                competitionsubmissionList.Add(
                new CompetitionSubmission
                {
                    CompetitorId = reader.GetInt32(0),
                    FileSubmitted = !reader.IsDBNull(1) ? reader.GetString(1) : null,
                    DateTimeFileUpload = !reader.IsDBNull(2) ? reader.GetDateTime(2) : (DateTime?)null,
                    Appeal = !reader.IsDBNull(3) ?
                                reader.GetString(3) : null,
                    VoteCount = reader.GetInt32(4),
                    Ranking = !reader.IsDBNull(5) ?
                                reader.GetInt32(5) : (int?)null,
                    CompetitorName = reader.GetString(6),
                }
                );
            }
           
            reader.Close();
            
            conn.Close();
            return competitionsubmissionList;
        }
    }
}
