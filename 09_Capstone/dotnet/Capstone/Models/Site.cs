using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        int ReservationId { get; set; }
        int SiteId { get; set; }
        string Name { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        DateTime BookDate { get; set; }
    }
}
