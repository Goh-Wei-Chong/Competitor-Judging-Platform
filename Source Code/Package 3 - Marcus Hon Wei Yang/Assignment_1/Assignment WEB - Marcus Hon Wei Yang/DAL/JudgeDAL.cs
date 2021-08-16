using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;
using System.Data.Common;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.DAL
{
    public class JudgeDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        
        public JudgeDAL()
        {
            
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "CJP_DBString");
            
            conn = new SqlConnection(strConn);
        }

        //DISPLAY ALL JUDGE
        public List<Judge> GetAllJudge()
        {
            
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"select Judge.JudgeID, Judge.JudgeName, Judge.Salutation, Judge.AreaInterestID, Judge.EmailAddr, Judge.Password, AreaInterest.Name as AreaInterestName 
                                from Judge 
                                inner join areainterest on Judge.AreaInterestID = AreaInterest.AreaInterestID";

            
            conn.Open();
            
            SqlDataReader reader = cmd.ExecuteReader();
            
            List<Judge> judgeList = new List<Judge>();
            while (reader.Read())
            {
                judgeList.Add(
                new Judge
                {
                    JudgeId = reader.GetInt32(0), 
                    JudgeName = reader.GetString(1), 
                                                
                    Salutation = reader.GetString(2), 
                    AreaInterestId = reader.GetInt32(3),
                    
                    EmailAddr = reader.GetString(4), 
                    AreaInterestName = reader.GetString(6),
                }
                );
            }
            
            reader.Close();
            
            conn.Close();
            return judgeList;
        }


        //CREATE A NEW JUDGE 
        public int Add(Judge judge)
        {
            
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"INSERT INTO Judge ( JudgeName, Salutation, AreaInterestId,
                                EmailAddr, Password)
                                OUTPUT INSERTED.JudgeID
                               VALUES( @judgename, @salutation, @areainterestId,
                                @emailaddr, @password)" ;
            
            cmd.Parameters.AddWithValue("@judgename", judge.JudgeName);
            cmd.Parameters.AddWithValue("@salutation", judge.Salutation);
            cmd.Parameters.AddWithValue("@areainterestId", judge.AreaInterestId);
            cmd.Parameters.AddWithValue("@emailaddr", judge.EmailAddr);
            cmd.Parameters.AddWithValue("@password", judge.Password);
            
            
            conn.Open();           
            judge.JudgeId = (int)cmd.ExecuteScalar();                     
            conn.Close();           
            return judge.JudgeId;          
        }

      

        //CHECK IF THE ENTERED EMAIL ALREADY EXIST IN THE DATABASE
        public bool IsEmailExist(string email, int judgeId)
        {
            bool emailFound = false;
            
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT JudgeID FROM Judge
                                    WHERE EmailAddr=@selectedEmail";
            cmd.Parameters.AddWithValue("@selectedEmail", email);           
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != judgeId)
                        
                        emailFound = true;
                }
            }
            else
            { //No record
                emailFound = false; 
            }
            reader.Close();
            conn.Close();

            return emailFound;
        }

        


    }

}



