using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class CampgroundSqlDAL
    {
        private string _connectionString;

        public CampgroundSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Dictionary<int, Park> GetParks()
        {
            Dictionary<int, Park> result = new Dictionary<int, Park>();
            string sqlGetParks = @"SELECT * FROM park;";
            // create my connection object
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // open connection
                conn.Open();

                // create my command object
                SqlCommand cmd = new SqlCommand(sqlGetParks, conn);


                // execute command
                SqlDataReader reader = cmd.ExecuteReader();

                // if applicable loop through result set
                while (reader.Read())
                {
                    // populate object(s) to return
                    Park p = new Park();
                    p.Id = Convert.ToInt32(reader["park_id"]);
                    p.Name = Convert.ToString(reader["name"]);
                    p.Location = Convert.ToString(reader["location"]);
                    p.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                    p.Area = Convert.ToInt32(reader["area"]);
                    p.AnnualVisitors = Convert.ToInt32(reader["visitors"]);
                    p.Description = Convert.ToString(reader["description"]);

                    result.Add(p.Id, p);
                }
                return result;

            }
        }
    }
}
