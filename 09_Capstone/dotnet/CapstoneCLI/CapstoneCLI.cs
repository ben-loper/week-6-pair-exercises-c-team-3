using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class CapstoneCLI
    {
        private Dictionary<int, Park> _parks = new Dictionary<int, Park>();

        private CampgroundSqlDAL _db;

        public CapstoneCLI(CampgroundSqlDAL db)
        {
            _db = db;
        }

        public void Start()
        {
            bool quit = false;

            _parks = _db.GetParks();

            while (!quit)
            {
                Console.Clear();
                Console.WriteLine("Select a Park for Further Details");

                foreach (var park in _parks)
                {
                    Console.WriteLine($"{park.Value.Id}) {park.Value.Name}");
                }
                Console.WriteLine("Q) quit");

                string userChoice = Console.ReadLine();

                if(userChoice.ToLower() == "q")
                {
                    Console.Clear();
                    Console.WriteLine("Thanks for using our app!");
                    Console.ReadKey();
                    quit = true;
                }
                else
                {
                    try
                    {
                        int choice = int.Parse(userChoice);
                        if (_parks.ContainsKey(choice))
                        {
                            ViewParkInfoMenu(choice);
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter only valid park numbers");
                        Console.Write("Press any key to return to the park list...");
                        Console.ReadKey();
                    }
                }

            }
        }

        private void ViewParkInfoMenu(int id)
        {
            bool quit = false;

            while (!quit)
            {
                Console.Clear();
                Console.WriteLine(_parks[id].Name + " National Park");
                Console.WriteLine($"Location:".PadRight(18) + _parks[id].Location);
                Console.WriteLine($"Established:".PadRight(18) + _parks[id].EstablishDate.ToString("MM/dd/yyyy"));
                Console.WriteLine($"Area:".PadRight(18) + string.Format("{0:n0}", _parks[id].Area) + " sq km");
                Console.WriteLine($"Annual Visitors:".PadRight(18) + string.Format("{0:n0}",_parks[id].AnnualVisitors) + "\n");
                Console.WriteLine(_parks[id].Description + "\n");
                Console.WriteLine("1) View Campgrounds");
               // Console.WriteLine("2) Search for Reservation");
                Console.WriteLine("3) Return to Previous Screen");


                string userChoice = Console.ReadLine();


                try
                {
                    int choice = int.Parse(userChoice);
                    if (choice == 1)
                    {
                        ViewCampground(_parks[id].Id);
                    }
                    else if (choice == 2)
                    {
                        ;
                    }
                    else if (choice == 3)
                    {
                        quit = true;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Please enter only valid command");
                    Console.Write("Press any key to return to the park info screen...");
                    Console.ReadKey();
                }
            }

        }

        private void ViewCampground(int parkId)
        {
            Dictionary<int, Campground> campgrounds = new Dictionary<int, Campground>();

            campgrounds = _db.GetCampgroundByPark(parkId);

            bool quit = false;

            while (!quit)
            {
                Console.Clear();

                DisplayCampgroundsByPark(parkId, campgrounds);

                Console.WriteLine("\nSelect a command\n");
                Console.WriteLine("1) Search for Available Reservation");
                Console.WriteLine("2) Return to Previous Screen");

                string userChoice = Console.ReadLine();


                try
                {
                    int choice = int.Parse(userChoice);
                    
                    if (choice == 1)
                    {
                        SearchForCampgroundReservation(parkId, campgrounds);
                    }
                    else if (choice == 2)
                    {
                        quit = true;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Please enter only valid command");
                    Console.Write("Press any key to return to the campgrounds screen...");
                    Console.ReadKey();
                }
            }
        }        

        private void SearchForCampgroundReservation(int parkId, Dictionary<int, Campground> campgrounds)
        {
            bool quit = false;

            while (!quit)
            {
                Console.Clear();
                DisplayCampgroundsByPark(parkId, campgrounds);

                try
                { 
                    //Need to add verification
                    Console.WriteLine("\nWhich campground (enter 0 to cancel)?");
                    string userCampgroundChoiceString = Console.ReadLine();
                    
                    Console.WriteLine("What is the arrival date? (MM/DD/YYYY)");
                    string arrivalDateString = Console.ReadLine();
                
                    Console.WriteLine("What is the departure date? (MM/DD/YYYY)");
                    string departureDateString = Console.ReadLine();

                    DateTime arrivalDate = Convert.ToDateTime(arrivalDateString);
                    DateTime departureDate = Convert.ToDateTime(departureDateString);
                    int userCampgroundChoice = int.Parse(userCampgroundChoiceString);
                    
                    if (userCampgroundChoice == 0)
                    {
                        quit = true;
                    }
                    else
                    {
                        Dictionary<int, Site> sites = _db.FindAvailableSites(userCampgroundChoice, arrivalDate, departureDate);

                        Console.WriteLine("Results Matching Your Search Criteria");
                        Console.WriteLine("Site No.".PadRight(10) + "Max Occup.".PadRight(12) + "Accessible?".PadRight(12) + "Max RV Length".PadRight(15) + "Utility".PadRight(10) + "Cost");

                        foreach(var site in sites)
                        {
                            decimal costOfReservation = campgrounds[site.Value.CampgroundId].DailyFee * 
                                                        (decimal)(departureDate - arrivalDate).TotalDays;

                            Console.WriteLine($"{site.Value.SiteNum}".PadRight(10) + $"{site.Value.SiteOccupancy}".PadRight(12) +
                                                $"{site.Value.DisplayAccessible}".PadRight(12) + $"{site.Value.DisplayMaxRVLength}".PadRight(15) +
                                                $"{site.Value.DisplayUtilities}".PadRight(10) + costOfReservation.ToString("C"));
                           
                        }
                        Console.WriteLine("Which site should be reserved (enter 0 to cancel)?");
                        string userChoice = Console.ReadLine();


                        try
                        {
                            int siteId = int.Parse(userChoice);

                            if (sites.ContainsKey(siteId))
                            {
                                Console.WriteLine("What name should the reservation be made under?");
                                string reservationName = Console.ReadLine();
                                int reservationNumber = _db.AddReservation(siteId, reservationName, arrivalDate, departureDate);
                                Console.WriteLine($"\nThe reservation has been made and the confirmation id is {reservationNumber}");
                                Console.ReadKey();
                                quit = true;
                            }
                            else if (siteId == 0)
                            {
                                quit = true;
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter only valid command");
                            Console.Write("Press any key to return to the campgrounds screen...");
                            Console.ReadKey();
                        }
                        Console.ReadKey();
                    }
                }
                catch
                {
                    Console.WriteLine("Something went wrong!");
                    Console.ReadKey();
                }

            }
        }

        private void DisplayCampgroundsByPark(int parkId, Dictionary<int, Campground> campgrounds)
        {
            Console.WriteLine(_parks[parkId].Name + " National Park Campgrounds" + "\n");
            Console.WriteLine("".PadRight(5) + "Name".PadRight(30) +
                                "Open".PadRight(15) + "Close".PadRight(15) + "Daily Fee");

            foreach (var campground in campgrounds)
            {
                Console.WriteLine($"#{campground.Value.Id}".PadRight(5) + $"{campground.Value.Name}".PadRight(30) +
                                    $"{campground.Value.DisplayOpenFromMonth}".PadRight(15) +
                                    $"{campground.Value.DisplayOpenToMonth}".PadRight(15) +
                                    campground.Value.DailyFee.ToString("C"));
            }
        }
    }
}
