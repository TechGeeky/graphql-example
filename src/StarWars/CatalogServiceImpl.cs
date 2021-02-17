using System;
using System.Collections.Generic;

namespace StarWars
{
    public class CatalogServiceImpl : ICatalogService
    {
        private readonly IData data;

        public CatalogServiceImpl(IData data)
        {
            this.data = data;
            Console.WriteLine("Inside CatalogServiceImpl");
        }

        public IList<string> GetCatalog()
        {
            return null;
        }
    }
}