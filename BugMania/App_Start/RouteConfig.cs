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

            //routes.MapRoute(
            //    name: "BugReport",
            //    url: "{controller}/{id}",
            //    defaults: new { controller = "ViewAllBugReport", action = "View", id = UrlParameter.Optional},
            //    namespaces: new[] { "BugMania.BugReportControllers" }
            //);
            //routes.MapRoute(
            //    name: "Group",
            //    url: "Group/{controller}/{action}/{id}",
            //    defaults: new { controller = "Group", action = "Index", id = UrlParameter.Optional }
            //);
            //routes.MapRoute(
            //    name: "Comment",
            //    url: "Comment/{controller}/{action}/{id}",
            //    defaults: new { controller = "Comment", action = "Index", id = UrlParameter.Optional }
            //);
            //routes.MapRoute(
            //    name: "Tag",
            //    url: "Tag/{controller}/{action}/{id}",
            //    defaults: new { controller = "Tag", action = "Index", id = UrlParameter.Optional }
            //);
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "DefaultPath",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ViewAllBugReport", action = "View", id = UrlParameter.Optional },
                namespaces: new[] { "BugMania.BugReportControllers", "BugMania.Controllers" }
            );
        }
    }
}
