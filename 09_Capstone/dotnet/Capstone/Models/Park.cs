using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Park
    {
        int Id { get; set; }
        int ParkId { get; set; }
        string Name { get; set; }
        int OpenFromMonth { get; set; }
        int OpenToMonth { get; set; }
        double DailyFee { get; set; }
    }
}
