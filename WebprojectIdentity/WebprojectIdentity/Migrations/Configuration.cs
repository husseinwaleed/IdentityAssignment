namespace WebprojectIdentity.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebprojectIdentity.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebprojectIdentity.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //



            var store = new RoleStore<IdentityRole>(context);
            var userstor = new UserStore<ApplicationUser>(context);
            var roleManager = new RoleManager<IdentityRole>(store);
            var UserManager = new UserManager<ApplicationUser>(userstor);


            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("User"));
            roleManager.Create(new IdentityRole("Teachr"));

            ApplicationUser hussine = new ApplicationUser();

            hussine.Email = "a@a.a";
            hussine.UserName = "hussein";

            var result = UserManager.Create(hussine, "Password!123");
            context.SaveChanges();

            string useridentity = context.Users.Single(p => p.UserName == "hussein").Id;
            UserManager.AddToRole(useridentity, "Admin");
        }
    }
}
