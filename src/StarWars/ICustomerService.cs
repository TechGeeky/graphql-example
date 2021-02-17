using System.Collections.Generic;

namespace StarWars
{
    public interface ICustomerService
    {
        IList<string> GetCustomers();
    }
}