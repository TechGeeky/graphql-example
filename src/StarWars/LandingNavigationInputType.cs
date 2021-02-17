using GraphQL.Types;
using StarWars.Types;

namespace StarWars
{
    public class LandingNavigationInputType : InputObjectGraphType<HorusContainerRequest>
    {
        public LandingNavigationInputType()
        {
            Name = "LandingNavigationInput";
            // Field(x => x.Id);
            // Field(x => x.Name);
            // Field(x => x.HomePlanet, nullable: true);
            Field(x => x.SiteId);
            Field<ListGraphType<FilterType>>("filters", "list of filters");
            // Field(x => x.Filters, nullable: true);
            Field(x => x.AppId, nullable: true);
            Field(x => x.CurrencyId, nullable: true);
            Field(x => x.LanguageId, nullable: true);
            // Field(x => x.LandingNavigationAggregationRequest.LandingNavigationAggregationKey, nullable: true);
            Field<NonNullGraphType<LandingNavigationAggregationRequestType>>("landingNavigationAggregationRequest", "list of land aggr type");
            // Field(x => x.QueryTemplate, nullable: true);
        }
    }
}
