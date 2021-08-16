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
    public class AreaInterestDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public AreaInterestDAL()
        {
            
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "CJP_DBString");

            
            conn = new SqlConnection(strConn);
        }

        public List<AreaInterest> GetAllAreaInterest()
        {
            
            SqlCommand cmd = conn.CreateCommand();
            
            cmd.CommandText = @"SELECT * FROM AreaInterest";
            
            conn.Open();
            
            SqlDataReader reader = cmd.ExecuteReader();
            
            List<AreaInterest> areainterestList = new List<AreaInterest>();
            while (reader.Read())
            {
                areainterestList.Add(
                new AreaInterest
                {
                    AreaInterestId = reader.GetInt32(0), //0: 1st column
                    AreaInterestName = reader.GetString(1), //1: 2nd column                                               
                }
                );
            }
            
            reader.Close();
            
            conn.Close();
            return areainterestList;
        }
    }
}
