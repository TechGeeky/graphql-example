using GraphQL.Types;
using StarWars.Types;

namespace StarWars
{
    public class LandingNavigationAggregationRequestType : InputObjectGraphType<LandingNavigationAggregationRequest>
    {
        public LandingNavigationAggregationRequestType()
        {
            Name = "LandingNavigationAggregationRequest";
            Field(x => x.LandingNavigationAggregationKey);
        }
    }
}
