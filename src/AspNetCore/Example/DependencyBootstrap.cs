using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StarWars;
using StarWars.Types;
using System;

namespace Example
{
    public static class DependencyBootstrap
    {
        public static void WireUpDependencies(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            // ...
            // old legacy code base here which uses provider
            // ...
            try
            {
                WireUpCore(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void WireUpCore(IServiceCollection services)
        {
            var localProvider = services.BuildServiceProvider();
            //..........
            // old legacy code here which uses localProvider

            services.AddSingleton<IData, DataImpl>();
            services.AddSingleton<ICatalogService, CatalogServiceImpl>();
            services.AddSingleton<ICustomerService, CustomerServiceImpl>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, GraphQL.SystemTextJson.DocumentWriter>();

            services.AddSingleton<StarWarsData>();
            services.AddSingleton<StarWarsQuery>();
            services.AddSingleton<StarWarsMutation>();
            services.AddSingleton<HumanType>();
            services.AddSingleton<HumanInputType>();
            services.AddSingleton<LandingNavigationInputType>();
            services.AddSingleton<FilterType>();
            services.AddSingleton<LandingNavigationAggregationRequestType>();
            // services.AddSingleton<DroidType>();
            // services.AddSingleton<CharacterInterface>();
            // services.AddSingleton<EpisodeEnum>();
            services.AddSingleton<ISchema, StarWarsSchema>();

            services.AddLogging(builder => builder.AddConsole());
            services.AddHttpContextAccessor();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
            })
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
            .AddSystemTextJson()
            .AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User });
        }
    }
}
