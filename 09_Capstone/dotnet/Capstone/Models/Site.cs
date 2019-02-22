using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    /// <summary>
    /// The site class stores the information about the camp site
    /// </summary>
    public class Site
    {
        public int Id { get; set; }                 //The site Id
        public int CampgroundId { get; set; }       //The campground Id
        public int SiteNum { get; set; }            //The site number
        public int SiteOccupancy { get; set; }      //The maximum occupancy of the site
        public int Accessible { get; set; }         //Is the site accessible
        public string DisplayAccessible             //A derived variable to display the accessible string message
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
        public int MaxRVLength { get; set; }        //The maximum RV length
        public string DisplayMaxRVLength            //A derived variable to display the maximum RV length message
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
        public int Utilities { get; set; }          //Does the site have utilities
        public string DisplayUtilities              //A derived variable to display the string message if the site has utilities
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
