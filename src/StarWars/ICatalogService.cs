using System.Collections.Generic;

namespace StarWars
{
    public interface ICatalogService
    {
        IList<string> GetCatalog();
    }
}