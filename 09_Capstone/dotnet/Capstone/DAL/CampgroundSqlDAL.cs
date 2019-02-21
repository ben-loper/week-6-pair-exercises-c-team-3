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
            string sqlGetParks = @"SELECT * FROM park
                                   ORDER BY park.name;";
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
        public Dictionary<int, Campground> GetCampgroundByPark(int parkId)
        {
            Dictionary<int, Campground> result = new Dictionary<int, Campground>();
            string sqlGetParks = @"SELECT * FROM campground
                                   WHERE campground.park_id = @parkId
                                   ORDER BY campground.name;";
            // create my connection object
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // open connection
                conn.Open();
                
                // create my command object
                SqlCommand cmd = new SqlCommand(sqlGetParks, conn);

                cmd.Parameters.AddWithValue("@parkId", parkId);

                // execute command
                SqlDataReader reader = cmd.ExecuteReader();

                // if applicable loop through result set
                while (reader.Read())
                {
                    // populate object(s) to return
                    Campground c = new Campground();
                    c.Id = Convert.ToInt32(reader["campground_id"]);
                    c.Name = Convert.ToString(reader["name"]);
                    c.OpenFromMonth = Convert.ToInt32(reader["open_from_mm"]);
                    c.OpenToMonth = Convert.ToInt32(reader["open_to_mm"]);
                    c.DailyFee = Convert.ToDecimal(reader["daily_fee"]);

                    result.Add(c.Id, c);
                }
                return result;
            }

        }
    }
}
