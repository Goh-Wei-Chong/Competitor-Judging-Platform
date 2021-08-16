using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using Assignment_WEB___Marcus_Hon_Wei_Yang;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.DAL
{
    public class CriteriaDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        
        public CriteriaDAL()
        {
            
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "CJP_DBString");
            
            conn = new SqlConnection(strConn);
        }

        //GET ALL CRITERIA for display
        public List<Criteria> GetAllCriteria()
        {
            
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"SELECT * FROM Criteria ORDER BY CriteriaID";
            
            conn.Open();
            
            SqlDataReader reader = cmd.ExecuteReader();
           
            List<Criteria> criteriaList = new List<Criteria>();
            while (reader.Read())
            {
                criteriaList.Add(
                new Criteria
                {
                    CriteriaId = reader.GetInt32(0), 
                    CompetitionId = reader.GetInt32(1), 
                                
                    CriteriaName = reader.GetString(2), 
                    Weightage = reader.GetInt32(3), 
                    
                }
                );
            }
            
            reader.Close();
            
            conn.Close();
            return criteriaList;
        }

        //Add Criteria
        public int Add(Criteria criteria)
        {
            
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"INSERT INTO Criteria (CriteriaName, CompetitionID, Weightage)
            OUTPUT INSERTED.CriteriaID
            VALUES(@criterianame,@competitionid, @weightage)";

            
            cmd.Parameters.AddWithValue("@criterianame", criteria.CriteriaName);
            cmd.Parameters.AddWithValue("@competitionid", criteria.CompetitionId);
            cmd.Parameters.AddWithValue("@weightage", criteria.Weightage);
           
            
            conn.Open();
            
            criteria.CriteriaId = (int)cmd.ExecuteScalar();
            
            conn.Close();
            
            return criteria.CriteriaId;
        }

        public Criteria GetDetails(int criteriaId)
        {
            Criteria criteria = new Criteria();
           
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"SELECT * FROM Criteria
            WHERE CriteriaID = @selectedCriteriaID";
            
            cmd.Parameters.AddWithValue("@selectedCriteriaID", criteriaId);
            
            conn.Open();
            
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                
                while (reader.Read())
                {
                    
                    criteria.CriteriaId = criteriaId;
                    criteria.CompetitionId = reader.GetInt32(1);
                    criteria.CriteriaName = reader.GetString(2);
                    criteria.Weightage = reader.GetInt32(3);
                    criteria.CompetitionId = 
                        reader.GetInt32(1) ;
                }
            }
            
            reader.Close();
            
            conn.Close();
 
            return criteria;
        }

        //validate for entering weightage over 100
        public bool OverWeightage (int weightage, int competitionId)
        {
            bool over = false;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"select sum(weightage) from criteria
                                where competitionid = 1
                                group by competitionid";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) >= 100)
                        over = true;
                }
            }
            else
            {
                over = false;
            }
            reader.Close();
            conn.Close();

            return over;
        }
    }


}

