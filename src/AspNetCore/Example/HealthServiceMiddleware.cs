using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Example
{
    public class HealthServiceMiddleware : BaseMiddleware
    {
        public HealthServiceMiddleware(RequestDelegate next) : base(next) { }

        public override bool IsCorrectEndpoint(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/health");
        }

        public override string GetEndpoint(HttpContext context) => "/health";

        public override async Task HandleRequest(HttpContext context)
        {
            Console.WriteLine("ABC: " + catalogService.GetCatalog());
            Console.WriteLine("DEF: " + customerService.GetCustomers());
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("Hello World!");
        }
    }

    public static class HealthServiceMiddlewareExtension
    {
        public static IApplicationBuilder UseHealthService(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HealthServiceMiddleware>();
        }
    }
}