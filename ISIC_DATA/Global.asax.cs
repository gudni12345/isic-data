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
using ISIC_DATA.DataAccess;
using System.Data.Entity;
using WebMatrix.WebData;
using ISIC_DATA.Migrations;
using ISIC_DATA.Models;

namespace ISIC_DATA
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "UserId", "UserName", autoCreateTables: false);
           // WebSecurity.InitializeDatabaseConnection("DogContext", "Users", "Id", "UserName", autoCreateTables: false);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            BootstrapSupport.BootstrapBundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DogContext, Configuration>());
            new DogContext().Users.Find(1);


            string superName = "superadmin";
            string superPassword = "kopurfrafitjamyri"; 
            string superRole = "SuperAdministrator";
            string Role = "Administrator";

            var superUser = new { UserEmail = "superadmin@gmail.com", Name = "Super administrator", RegisterDate = DateTime.Now };

            //Add administrators
            if (!Roles.RoleExists(superRole))
                Roles.CreateRole(superRole);

            if (!Roles.RoleExists(Role))
                Roles.CreateRole(Role);

            if (!WebSecurity.UserExists(superName))
                WebSecurity.CreateUserAndAccount(superName, superPassword, superUser);

            if (!Roles.IsUserInRole(superName, superRole))
                Roles.AddUserToRole(superName, superRole);


    /*
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }               
        public string Name { get; set; }
        public Nullable<int> CountryId { get; set; }
    */
           // WebSecurity.CreateUserAndAccount(model.UserName, model.Password,
   //propertyValues: new { FirstName = model.FirstName, LastName = model.LastName }, false);

            




        }
    }
}
