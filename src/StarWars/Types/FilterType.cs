using GraphQL.Types;
using StarWars.Types;

namespace StarWars
{
    public class FilterType : InputObjectGraphType<Filter>
    {
        public FilterType()
        {
            Name = "Filter";
            Field(x => x.FilterKey);
            Field(x => x.FilterValueId);
        }
    }
}
