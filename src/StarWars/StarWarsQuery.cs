using System;
using GraphQL;
using GraphQL.Types;
using StarWars.Types;
using System.Collections.Generic;

namespace StarWars
{
    public class StarWarsQuery : ObjectGraphType<object>
    {
        public StarWarsQuery(StarWarsData data, ICatalogService catalogService, ICustomerService customerService)
        {
            Name = "Query";
            Field<HumanType>(
                "landingNavigationRequest",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LandingNavigationInputType>> { Name = "landingNavigation", Description = "id of the human" }
                ),
                resolve: context =>
                {
                    var human = context.GetArgument<HorusContainerRequest>("landingNavigation");
                    var headers = (IDictionary<string, object>)context.UserContext;
                    // use catalogService and customerService here
                    // ....
                    return data.GetHumanByIdAsync(human);
                }
            );
        }
    }
}
