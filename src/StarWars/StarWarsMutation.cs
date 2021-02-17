using System;
using GraphQL;
using GraphQL.Types;
using StarWars.Types;

namespace StarWars
{
    /// <example>
    /// This is an example JSON request for a mutation
    /// {
    ///   "query": "mutation ($human:HumanInput!){ createHuman(human: $human) { id name } }",
    ///   "variables": {
    ///     "human": {
    ///       "name": "Boba Fett"
    ///     }
    ///   }
    /// }
    /// </example>
    public class StarWarsMutation : ObjectGraphType
    {
        public StarWarsMutation(StarWarsData data)
        {
            Name = "Mutation";

            Field<HumanType>(
                "landingNavigation",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LandingNavigationInputType>> { Name = "human" }
                ),
                resolve: context =>
                {
                    var human = context.GetArgument<Human>("human");
                    Console.WriteLine("Data-1: " + human);
                    return data.AddHuman(human);
                });
        }
    }
}
