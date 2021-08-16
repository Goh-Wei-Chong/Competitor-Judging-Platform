using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.DAL
{
    public class CompetitorDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        
        public CompetitorDAL()
        {
            
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "CJP_DBString");
            
            conn = new SqlConnection(strConn);
        }
        public List<Competitor> GetAllCompetitor()
        {
            
            SqlCommand cmd = conn.CreateCommand();
            

            cmd.CommandText = @"SELECT * FROM Competitor";
            
            conn.Open();

            
            SqlDataReader reader = cmd.ExecuteReader();
            
            List<Competitor> competitorList = new List<Competitor>();
            while (reader.Read())
            {
                competitorList.Add(
                new Competitor
                {
                    CompetitorId = reader.GetInt32(0), 
                    CompetitorName = reader.GetString(1),
                                                     
                }
                );
            }

            
            reader.Close();
            
            conn.Close();
            return competitorList;
        }
    }
}

