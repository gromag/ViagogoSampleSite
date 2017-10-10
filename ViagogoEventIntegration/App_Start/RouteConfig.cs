using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ViagogoEventIntegration
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Search",
                url: "Home/Search/{searchTerms}",
                defaults: new { controller = "Home", action = "Search", searchTerms = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GetListings",
                url: "Home/GetListings/{id}",
                defaults: new { controller = "Home", action = "GetListings" }
            );

            routes.MapRoute("Default", "", new { controller = "Home", action = "Index", searchTerms = UrlParameter.Optional });
        }
    }
}
