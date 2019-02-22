using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class CampgroundSqlDAL
    {
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";
        private string _connectionString;

        public CampgroundSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get the parks and park information from the database
        /// </summary>
        /// <returns></returns> Returns a dictionary of the parks and park information
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
        
        /// <summary>
        /// Get the campgrounds and campground information in a given park from the database
        /// </summary>
        /// <param name="parkId"></param>The Id of the park to get the campgounds from
        /// <returns></returns> Returns a dictionary of the campgrounds in given park and the campground information
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

        }/// <summary>
        
            /// Find the available sites in a given campground between two dates
        /// </summary>
        /// <param name="campgroundId"></param> The campground Id to look for available sites
        /// <param name="fromDate"></param>The date from which to look for available sites
        /// <param name="toDate"></param>The date which to look for available sites
        /// <returns></returns> Returns a dictionary of available sites and their information
        public Dictionary<int, Site> FindAvailableSites(int campgroundId, DateTime fromDate, DateTime toDate)
        {

            Dictionary<int, Site> results = new Dictionary<int, Site>();

            string sqlGetParks = @"SELECT *
                                    FROM site
                                    WHERE site.campground_id = @campgroundId AND site.site_id NOT IN
                                    (
                                    SELECT site.site_id
                                    FROM site
                                    JOIN reservation ON site.site_id = reservation.site_id
                                    WHERE (reservation.from_date BETWEEN @fromDate AND @toDate) OR
                                    (reservation.to_date BETWEEN @fromDate AND @toDate)
                                    );";
            // create my connection object
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // open connection
                conn.Open();

                // create my command object
                SqlCommand cmd = new SqlCommand(sqlGetParks, conn);

                cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);

                // execute command
                SqlDataReader reader = cmd.ExecuteReader();
                

                // if applicable loop through result set
                while (reader.Read())
                {
                    // populate object(s) to return
                    Site s = new Site();
                    s.Id = Convert.ToInt32(reader["site_id"]);
                    s.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                    s.SiteNum = Convert.ToInt32(reader["site_number"]);
                    s.SiteOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                    s.Accessible = Convert.ToInt32(reader["accessible"]);
                    s.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                    s.Utilities = Convert.ToInt32(reader["utilities"]);


                    results.Add(s.Id, s);
                }
                return results;
            }
        }

        /// <summary>
        /// Add a reservation for a campsite
        /// </summary>
        /// <param name="siteId"></param> The site Id to add the reservation.
        /// <param name="reservationName"></param> The name of the person reserving the campsite.
        /// <param name="fromDate"></param> The date from which the reservation starts
        /// <param name="toDate"></param>The date to which the reservation ends
        /// <returns></returns> Returns the reservation Id
        public int AddReservation(int siteId, string reservationName, DateTime fromDate, DateTime toDate)
        {
            int result = 0;

            string SqlAddReservation = @"INSERT INTO reservation(site_id, name, from_date, to_date)
                                       VALUES(@siteId, @reservationName, @fromDate, @toDate);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // open connection
                conn.Open();

                // create my command object
                SqlCommand cmd = new SqlCommand(SqlAddReservation + _getLastIdSQL, conn);

                cmd.Parameters.AddWithValue("@siteId", siteId);
                cmd.Parameters.AddWithValue("@reservationName", reservationName);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);

                // execute command
                result = (int)cmd.ExecuteScalar();

                return result;
            }
        }
    }
}
