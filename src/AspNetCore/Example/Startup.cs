using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.ResponseCompression;
using GraphQL;
using StarWars;
using StarWars.Types;

namespace Example
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            DependencyBootstrap.WireUpDependencies(services);
            var provider = services.BuildServiceProvider();
            BaseMiddleware.InitializeDependencies(provider);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHealthService();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings
            {
                Path = "/graphql",
                BuildUserContext = ctx => new GraphQLUserContext
                {
                    User = ctx.User
                },
                EnableMetrics = true
            });

            // add http for Schema at default url /graphql
            // app.UseGraphQL<ISchema>();

            // use graphql-playground at default url /ui/playground
            app.UseGraphQLPlayground();
        }
    }
}
