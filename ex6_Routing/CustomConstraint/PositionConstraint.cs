using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ex6_Routing.CustomConstraint
{
    public class PositionConstraint : IRouteConstraint
    {
        private string[] positions = new[] { "director", "admin", "employee"};

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return positions.Contains(values[routeKey].ToString().ToLower().Trim('/'));
        }
    }
}
