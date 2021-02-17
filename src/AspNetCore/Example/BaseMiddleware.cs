using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using StarWars;

namespace Example
{
    public abstract class BaseMiddleware
    {
        private const string LOGGING_TITLE = nameof(BaseMiddleware);
        protected static ICatalogService catalogService;
        protected static ICustomerService customerService;
        private static IDictionary<string, Object> requiredServices;

        private readonly RequestDelegate _next;

        public abstract bool IsCorrectEndpoint(HttpContext context);
        public abstract string GetEndpoint(HttpContext context);
        public abstract Task HandleRequest(HttpContext context);

        public BaseMiddleware(RequestDelegate next)
        {
            var builder = new StringBuilder("");
            var isMissingService = false;
            foreach (var service in requiredServices)
            {
                if (service.Value == null)
                {
                    isMissingService = true;
                    builder.Append(service.Key).Append(", ");
                }
            }

            if (isMissingService)
            {
                var errorMessage = builder.Append("cannot start server.").ToString();
                throw new Exception(errorMessage);
            }

            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsCorrectEndpoint(context))
            {
                try
                {
                    await HandleRequest(context);
                }
                catch (Exception ex)
                {
                    // handle exception here
                    Console.WriteLine(ex);
                    return;
                }
                return;
            }

            await _next.Invoke(context);
        }

        public static void InitializeDependencies(IServiceProvider provider)
        {
            requiredServices = new Dictionary<string, Object>();

            var catalogServiceTask = Task.Run(() => provider.GetService<ICatalogService>());
            var customerServiceTask = Task.Run(() => provider.GetService<ICustomerService>());

            Task.WhenAll(catalogServiceTask, customerServiceTask).Wait();

            requiredServices[nameof(catalogService)] = catalogService = catalogServiceTask.Result;
            requiredServices[nameof(customerService)] = customerService = customerServiceTask.Result;
        }
    }
}