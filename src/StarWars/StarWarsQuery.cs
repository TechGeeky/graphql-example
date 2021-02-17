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
            // Console.WriteLine("F");
            // Field<CharacterInterface>("hero", resolve: context => data.GetDroidByIdAsync("3"));
            Field<HumanType>(
                "landingNavigationRequest",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LandingNavigationInputType>> { Name = "landingNavigation", Description = "id of the human" }
                ),
                // resolve: context => data.GetHumanByIdAsync(context.GetArgument<HorusContainerRequest>("landingNavigation"))
                resolve: context =>
                {
                    var human = context.GetArgument<HorusContainerRequest>("landingNavigation");
                    var headers = (IDictionary<string, object>)context.UserContext;
                    // Console.WriteLine("G" + headers);
                    // Console.WriteLine("Data-1: " + ObjectDumper.Dump(human));
                    return data.GetHumanByIdAsync(human);
                }
            );

            // Func<IResolveFieldContext, string, object> func = (context, id) => data.GetDroidByIdAsync(id);

            // FieldDelegate<DroidType>(
            //     "droid",
            //     arguments: new QueryArguments(
            //         new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the droid" }
            //     ),
            //     resolve: func
            // );
        }
    }
}
