using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BugMania
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "DefaultPath",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ViewAllBugReport", action = "ViewAllReports", id = UrlParameter.Optional },
                namespaces: new[] { "BugMania.Controllers" }
            );
        }
    }
}
