using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class CapstoneCLI
    {
        private CampgroundSqlDAL _db;

        public CapstoneCLI(CampgroundSqlDAL db)
        {
            _db = db;
        }

        public void Start()
        {
            Console.WriteLine("Welcome");
            Console.ReadKey();
        }
    }
}
