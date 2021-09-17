using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Applications
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to our bank");
            do
            {
                Console.WriteLine("\nChoose below Required Services.");
                Console.WriteLine("Enter 1 for Create \n      2 for Update \n      3 for Fetch \n      4 for Delete");
                // Read User Inputs
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter your Detail.");
                        var customer = new Customer()
                        {
                            CustomerName = Console.ReadLine(),
                            CustomerAddress = Console.ReadLine(),
                            BillAmount = double.Parse(Console.ReadLine()),
                            BillDate = DateTime.Now,
                        }
                        ;
                        if (DataAccess.Create(customer))
                        {
                            Console.WriteLine("Created Successfully");

                        }
                        else
                        {
                            Console.WriteLine("Created Failure");
                        }
                        break;
                    case "2":
                        //DataAccess.Update();
                        Console.WriteLine("Updated Successfully");
                        break;
                    case "3":
                        Console.WriteLine("Fetch Successfully");
                        break;
                    case "4":
                        Console.WriteLine("Deleted Successfully");
                        break;
                    case "5":
                        foreach (var item in DataAccess.FetchAll())
                        {
                            Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                        }
                        break;

                }
            }
            while (true);
        }
    }

    class DataAccess
    {
        private static List<Customer> customers = new List<Customer>();
        public static bool Create(Customer customer)
        {
            var lastId = 0;
            if (customers.Count > 0)
            {
                lastId = customers.Last().CustomerID;
            }
            customer.CustomerID = lastId + 1;
            customers.Add(customer);
            return true;
        }
        public static List<Customer> FetchAll()
        {
            return customers;
        }
        //public static bool Update()
        //{

        //}
        //public static bool Fetch()
        //{

        //}
        //public static bool Delete()
        //{

        //}

    }
    class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime BillDate { get; set; }
        public double BillAmount { get; set; }
    }

}
