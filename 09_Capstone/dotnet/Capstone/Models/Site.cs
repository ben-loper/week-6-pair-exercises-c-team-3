using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        
        public int Id { get; set; }
        public int CampgroundId { get; set; }
        public int SiteNum { get; set; }
        public int SiteOccupancy { get; set; }
        public int Accessible { get; set; }
        public string DisplayAccessible
        {
           
            get
            {
                string result = "No";
                if (Accessible == 1)
                {
                    result = "Yes";
                }
                return result;
            }
        }
        public int MaxRVLength { get; set; }
        public string DisplayMaxRVLength
        {
            get
            {
                string result = "N/A";
                if (MaxRVLength >0)
                {
                    result = MaxRVLength.ToString();
                }
                return result;

            }
        }
        public int Utilities { get; set; }
        public string DisplayUtilities
        {
            get
            {
                string result = "N/A";
                if (Utilities == 1)
                {
                    result = "Yes";
                }
                return result;
            }
        }
    }
}
