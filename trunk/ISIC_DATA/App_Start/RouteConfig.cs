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

            routes.MapNavigationRoute("Home-navigation", "Home", "Home", new { controller = "Home", action = "Index" });

            routes.MapNavigationRoute("Dog-navigation", "Database", "Dog", new { controller = "Dog", action = "Index" });

            routes.MapNavigationRoute("TestMate-navigation", "Test Mate", "TestMate", new { controller = "TestMate", action = "Index" });
            
            routes.MapNavigationRoute("News-navigation", "News", "News", new { controller = "Home", action = "News" });
            //This is the second Navigation route
            //                          RouteName        DisplayName    Url   Defaults
            routes.MapNavigationRoute("About-navigation", "About", "About", new { controller = "Home", action = "About" });

            routes.MapNavigationRoute("Contact-navigation", "Contact", "Contact", new { controller = "Home", action = "Contact" });

          
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}