using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime EstablishDate { get; set; }
        int Area { get; set; }
        int AnnualVisitors { get; set; }
        string Description { get; set; }
    }
}
