using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models

{/// <summary>
 /// The information about the reservation
 /// </summary>
    public class Reservation
    {
        public int ReservationId { get; set; }              // The reservation Id
        public int SiteId { get; set; }                     // The site Id on the reservation
        public string Name { get; set; }                    // The name of the person making the reservation
        public DateTime FromDate { get; set; }              // The date the reservation starts
        public DateTime ToDate { get; set; }                // The date the reservation ends
        public DateTime BookDate { get; set; }              // The date the reservation was booked
        
    }
}
