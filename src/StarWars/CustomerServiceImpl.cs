using System;
using System.Collections.Generic;

namespace StarWars
{
    public class CustomerServiceImpl : ICustomerService
    {

        public CustomerServiceImpl()
        {
            Console.WriteLine("Inside CustomerServiceImpl");
        }

        public IList<string> GetCustomers()
        {
            return null;
        }
    }
}