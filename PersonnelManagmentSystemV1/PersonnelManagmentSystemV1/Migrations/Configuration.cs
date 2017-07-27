namespace PersonnelManagmentSystemV1.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PersonnelManagmentSystemV1.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PersonnelManagmentSystemV1.DataAccess;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            #region ExampleSeed
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
            #endregion

            #region SeedingRoles
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);

                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!context.Roles.Any(r => r.Name == "Boss"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);

                roleManager.Create(new IdentityRole("Boss"));
            }

            if (!context.Roles.Any(r => r.Name == "Worker"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);

                roleManager.Create(new IdentityRole("Worker"));
            }

            if (!context.Roles.Any(r => r.Name == "Searcher"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);

                roleManager.Create(new IdentityRole("Searcher"));
            }
            #endregion

            #region SeedingDefaultUsers

            if (!context.Users.Any(u => u.UserName == "admin@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "admin@mail.com", Email = "admin@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "boss@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "boss@mail.com", Email = "boss@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Boss");
            }

            if (!context.Users.Any(u => u.UserName == "worker@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "worker@mail.com", Email = "worker@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Worker");
            }

            if (!context.Users.Any(u => u.UserName == "some@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "searcher@mail.com", Email = "searcher@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Searcher");
            }
            #endregion
        }
    }
}
