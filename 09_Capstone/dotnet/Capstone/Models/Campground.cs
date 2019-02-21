using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OpenFromMonth { get; set; }
        public int OpenToMonth { get; set; }
        public double DailyFee { get; set; }
    }
}
