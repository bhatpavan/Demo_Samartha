using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to KSRTC");
            var bookOnceAgain = false;
            var busService = new BusService();
            do
            {
                Console.WriteLine("Want to login as Admin then Enter your Code, if not Just press enter");
                if (Console.ReadLine() == "1234")
                {
                    string selectedOption = ShowServices(busService);
                    foreach (var customer in busService.FetchCustomers(selectedOption))
                    {
                        Console.WriteLine($"Name: {customer.Name}, Phno: {customer.Phno}");
                    }
                    Console.WriteLine("Want to Check other Service Y or N ?");
                    bookOnceAgain = Console.ReadLine().ToLower() == "y" ? true : false;
                }
                else
                {
                    string selectedOption = ShowServices(busService);
                    var isAvailable = busService.SelectBus(selectedOption);
                    if (!isAvailable)
                    {
                        Console.WriteLine("No seats Available.");
                    }
                    else
                    {
                        Console.WriteLine("Enter Name and Phone Number, press Enter after Each Prop.");
                        var customer = new Customer()
                        {
                            Name = Console.ReadLine(),
                            Phno = Console.ReadLine()
                        };
                        busService.BookTicket(customer, selectedOption);
                        Console.WriteLine("Successfully Booked");
                    }

                    Console.WriteLine("Want to Book another Service Y or N ?");
                    bookOnceAgain = Console.ReadLine().ToLower() == "y" ? true : false;
                }
            }
            while (bookOnceAgain);
        }

        private static string ShowServices(BusService busService)
        {
            // Fetch All Buses
            Console.WriteLine("Select from below Services");

            foreach (var bus in busService.FetchAllBuses())
            {
                Console.WriteLine(JsonConvert.SerializeObject(bus));
            }

            // Select and Book Ticket
            var selectedOption = Console.ReadLine();
            return selectedOption;
        }
    }
    class BusService
    {
        public IEnumerable<Bus> FetchAllBuses()
        {
            return DataBase.Buses;
        }
        public bool SelectBus(string selectedOption)
        {
            var bus = DataBase.Buses.Find(x => x.Id == UInt32.Parse(selectedOption));
            return bus.AvailableSeats > 0 ? true : false;
        }
        public void BookTicket(Customer customer, string selectedOption)
        {
            var bus = DataBase.Buses.Find(x => x.Id == UInt32.Parse(selectedOption));
            bus.Customers.Add(customer);
            bus.AvailableSeats = bus.AvailableSeats - 1;
        }

        /// <summary>
        /// This Method for Fetching Customer
        /// </summary>
        /// <param name="selectedOption">"Used for Specifying the bus."</param>
        /// <returns></returns>
        public List<Customer> FetchCustomers(string selectedOption)
        {
            return DataBase.Buses.Find(x => x.Id == UInt32.Parse(selectedOption)).Customers;
        }
    }
    class DataBase
    {
        // To save Bus Details
        public static List<Bus> Buses = new List<Bus>()
        {
            new Bus()
            {
                AvailableSeats=4,
                Id=1,
                PlaceFromTo="BlrToSgr",
                DepartureTime="8:30 PM",
            },
             new Bus()
            {
                AvailableSeats=4,
                Id=2,
                PlaceFromTo="BlrToSgr",
                DepartureTime="10:30 PM",
            },
              new Bus()
            {
                AvailableSeats=4,
                Id=3,
                PlaceFromTo="BlrToHubli",
                DepartureTime="8:30 PM",
            },
               new Bus()
            {
                AvailableSeats=4,
                Id=4,
                PlaceFromTo="BlrToHubli",
                DepartureTime="10:30 PM",
            },
                new Bus()
            {
                AvailableSeats=4,
                Id=5,
                PlaceFromTo="BlrToMlr",
                DepartureTime="8:30 PM",
            },
                 new Bus()
            {
                AvailableSeats=4,
                Id=6,
                PlaceFromTo="BlrToMlr",
                DepartureTime="10:30 PM",
            },
        };
    }
    class Customer
    {
        public string Name { get; set; }
        public string Phno { get; set; }
    }
    class Bus
    {
        public uint Id { get; set; }
        public uint AvailableSeats { get; set; }
        public string PlaceFromTo { get; set; }
        public string DepartureTime { get; set; }

        [JsonIgnore]
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
