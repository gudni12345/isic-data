using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NavigationRoutes;

namespace ISIC_DATA
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapNavigationRoute("Home-navigation", "Home", "", new { controller = "Home", action = "Index" });

            routes.MapNavigationRoute("Dog-navigation", "Database", "dog", new { controller = "Dog", action = "Index" });
            //This is the second Navigation route
            //                          RouteName        DisplayName    Url   Defaults
            routes.MapNavigationRoute("About-navigation", "About", "about", new { controller = "Home", action = "About" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}