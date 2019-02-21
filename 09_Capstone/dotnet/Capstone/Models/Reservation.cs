using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        int Id { get; set; }
        int CampgroundId { get; set; }
        int SiteNum { get; set; }
        int SiteOccupancy { get; set; }
        bool Accessible { get; set; }
        int MaxRVLength { get; set; }
        bool Utilities { get; set; }
    }
}
