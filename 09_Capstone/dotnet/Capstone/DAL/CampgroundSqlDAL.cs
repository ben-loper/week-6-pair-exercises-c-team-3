using System;
using System.Collections.Generic;
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
    }
}
