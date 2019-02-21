using Capstone.DAL;
using System;

namespace Capstone
{
    public class Program
    {
        static void Main(string[] args)
        {

            string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=npcampground;Integrated Security=True";

            CampgroundSqlDAL db = new CampgroundSqlDAL(connectionString);

            CapstoneCLI capstoneCli = new CapstoneCLI(db);

            capstoneCli.Start();
        }
    }
}
