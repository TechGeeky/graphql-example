using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Primitives;

namespace Example
{
    public class GraphQLUserContext : Dictionary<string, object>
    {
        public ClaimsPrincipal User { get; set; }
        // public IDictionary<string, StringValues> Headers { get; set; }
    }
}
