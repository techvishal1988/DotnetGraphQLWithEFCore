using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

#nullable enable
namespace Geography.Service
{
    public class GraphQLRequestContext : Dictionary<string, object?>
    {
        public ClaimsPrincipal User { get; set; }

        public HttpRequest Request { get; set; }

        public GraphQLRequestContext(ClaimsPrincipal user, HttpRequest request)
        {
            User = user;
            Request = request;
        }
    }
}