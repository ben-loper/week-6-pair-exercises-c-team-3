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
        public decimal DailyFee { get; set; }

        private Dictionary<int, string> _months = new Dictionary<int, string>() {
                    { 1, "January" },
                    { 2, "Febuary" },
                    { 3, "March" },
                    { 4, "April" },
                    { 5, "May" },
                    { 6, "June" },
                    { 7, "July" },
                    { 8, "August" },
                    { 9, "September" },
                    { 10, "October" },
                    { 11, "November" },
                    { 12, "December" },
                };

        public string DisplayOpenToMonth
        {
            get
            {
                string result = _months[OpenToMonth];

                return result;
            }
        }

        public string DisplayOpenFromMonth
        {
            get
            {
                string result = _months[OpenFromMonth];

                return result;
            }
        }
    }
}
