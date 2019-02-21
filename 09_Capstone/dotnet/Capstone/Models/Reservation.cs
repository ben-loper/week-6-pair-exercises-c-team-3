using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CampgroundId { get; set; }
        public int SiteNum { get; set; }
        public int SiteOccupancy { get; set; }
        public bool Accessible { get; set; }
        public int MaxRVLength { get; set; }
        public bool Utilities { get; set; }
    }
}
