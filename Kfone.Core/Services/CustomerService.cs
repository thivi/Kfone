using Kfone.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kfone.Core.Services
{
    public class CustomerService
    {
        public static List<Customer> customers = new List<Customer>(new Customer[]
        {
            new Customer{name="John Doe", address="20, PG, Colombo-03", contactNo="74373876847", dob=DateTime.Parse("03/03/1995"), profilePic="https://th.bing.com/th/id/R.569493641bff31b6ee9a484586487b10?rik=KsmvdhoyrlKC7g&pid=ImgRaw&r=0", tier=Tiers.Gold}
        });

        public static List<Customer> GetCustomers()
        {
            return customers;
        }

        public static void SetCustomer(Customer device)
        {
            customers.Add(device);
        }

        public static void DeleteCustomer(Customer device)
        {
            customers.Remove(device);
        }
    }
}
