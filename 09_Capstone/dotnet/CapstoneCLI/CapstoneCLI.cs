using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class CapstoneCLI
    {
        private Dictionary<int, Park> _parks = new Dictionary<int, Park>();

        private CampgroundSqlDAL _db;

        public CapstoneCLI(CampgroundSqlDAL db)
        {
            _db = db;
        }

        public void Start()
        {
            _parks = _db.GetParks();

            foreach(var park in _parks)
            {
                Console.WriteLine(park.Value.Name);
            }
            Console.WriteLine("Welcome");

            Console.ReadKey();
        }
    }
}
