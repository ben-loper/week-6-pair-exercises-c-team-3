using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    /// <summary>
    /// Park class holds information about the park
    /// </summary>
    public class Park
    {
        public int Id { get; set; }                         // The park Id
        public string Name { get; set; }                    // The park name
        public string Location { get; set; }                // The park location
        public DateTime EstablishDate { get; set; }         // The date the park was established
        public int Area { get; set; }                       // The area of the park in square kilometers
        public int AnnualVisitors { get; set; }             // The number of annual visitors
        public string Description { get; set; }             // The description of the park
    }
}
