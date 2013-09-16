namespace ISIC_DATA.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<ISIC_DATA.DataAccess.DogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ISIC_DATA.DataAccess.DogContext context)
        {
            WebSecurity.InitializeDatabaseConnection(
                        "DogContext",
                        "UserProfile",
                        "UserId",
                        "UserName", autoCreateTables: true);

            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!Roles.RoleExists("Disabled"))
                Roles.CreateRole("Disabled");

            if (!WebSecurity.UserExists("Admin"))
                WebSecurity.CreateUserAndAccount(
                    "Admin",
                    "password"
                   );
        

            if (!Roles.GetRolesForUser("Admin").Contains("Administrator"))
                Roles.AddUsersToRoles(new[] { "Admin" }, new[] { "Administrator" });
        }
    }
}
