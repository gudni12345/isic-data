using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;
using WebMatrix.WebData;

namespace ISIC_DATA
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: false);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            BootstrapSupport.BootstrapBundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);

            //Add administrators
            if (Membership.GetUser("admin2") == null)
            {
                WebSecurity.CreateUserAndAccount("admin2", "password.123");
            }

            if (Membership.GetUser("test") == null)
            {
                WebSecurity.CreateUserAndAccount("test", "password.123");
            }
            if (!Roles.RoleExists("Administrator"))
            {
                Roles.CreateRole("Administrator");
                Roles.AddUserToRole("admin2", "Administrator");
            }


            
   /*         if (!Roles.RoleExists("Administrator"))
            {
                Roles.CreateRole("Administrator");
            }
            if (Membership.GetUser("Admin") == null)
            {
                Membership.CreateUser("Admin", "Hundur12345", "isey@hive.is");
                Roles.AddUserToRole("Admin", "Administrator");
            }
    */
        }
    }
}