namespace PersonnelManagmentSystemV1.Migratio7ns
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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
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
                userManager.Create(user, "Password1!");

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

            if (!context.Users.Any(u => u.UserName == "searcher@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "searcher@mail.com", Email = "searcher@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Searcher");
            }
            #region Jocke Seed Workers

            if (!context.Users.Any(u => u.UserName == "accountant1@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "accountant1@mail.com", Email = "accountant1@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Worker");
            }

            if (!context.Users.Any(u => u.UserName == "hr1@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "hr1@mail.com", Email = "hr1@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Worker");
            }

            if (!context.Users.Any(u => u.UserName == "it1@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "it1@mail.com", Email = "it1@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Worker");
            }

            if (!context.Users.Any(u => u.UserName == "tech1@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "tech1@mail.com", Email = "tech1@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Worker");
            }

            if (!context.Users.Any(u => u.UserName == "researcher1@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "researcher1@mail.com", Email = "researcher1@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Worker");
            }

            if (!context.Users.Any(u => u.UserName == "developer1@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "developer1@mail.com", Email = "developer1@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Worker");
            }

            #endregion

            #region Jocke Seed Mangers
            if (!context.Users.Any(u => u.UserName == "ceo@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "ceo@mail.com", Email = "ceo@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Boss");
            }

            if (!context.Users.Any(u => u.UserName == "cfo@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "cfo@mail.com", Email = "cfo@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Boss");
            }

            if (!context.Users.Any(u => u.UserName == "cro@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "cro@mail.com", Email = "cro@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Boss");
            }

            if (!context.Users.Any(u => u.UserName == "cto@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "cto@mail.com", Email = "cto@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Boss");
            }
            if (!context.Users.Any(u => u.UserName == "cio@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "cio@mail.com", Email = "cio@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Boss");
            }

            if (!context.Users.Any(u => u.UserName == "cdo@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "cdo@mail.com", Email = "cdo@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Boss");
            }

            if (!context.Users.Any(u => u.UserName == "chro@mail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser { UserName = "chro@mail.com", Email = "chro@mail.com" };
                userManager.Create(user, "Password");

                userManager.AddToRole(user.Id, "Boss");
            }

#endregion
            context.SaveChanges();
            #endregion

            #region Departments
            //if (!context.Departments.Any())
            //{
            //    ApplicationUser manager = context.Users.SingleOrDefault(u => u.UserName == "boss@mail.com");
            //    Department dep1 = new Department() { ID = 1, Name = "Test department", Manager = manager };
            //    context.Departments.AddOrUpdate(dep1);
            //};
            


            #endregion

#region Jocke seed Departments

            if (!context.Departments.Any(d => d.Name == "Management"))
            {
                ApplicationUser manager = context.Users.SingleOrDefault(u => u.UserName == "ceo@mail.com");
                Department dep1 = new Department() { ID = 1, Name = "Mangement", Manager = manager };
                context.Departments.AddOrUpdate(dep1);
                context.Users.SingleOrDefault(u => u.UserName == "cfo@mail.com").Department = dep1;
                context.Users.SingleOrDefault(u => u.UserName == "chro@mail.com").Department = dep1;
                context.Users.SingleOrDefault(u => u.UserName == "cio@mail.com").Department = dep1;
                context.Users.SingleOrDefault(u => u.UserName == "cto@mail.com").Department = dep1;
                context.Users.SingleOrDefault(u => u.UserName == "cro@mail.com").Department = dep1;
                context.Users.SingleOrDefault(u => u.UserName == "cdo@mail.com").Department = dep1;
            };

            if (!context.Departments.Any(d => d.Name == "Finance"))
            {
                ApplicationUser manager = context.Users.SingleOrDefault(u => u.UserName == "cfo@mail.com");
                Department dep1 = new Department() { ID = 1, Name = "Finance", Manager = manager };
                context.Departments.AddOrUpdate(dep1);
                context.Users.SingleOrDefault(u => u.UserName == "accountant1@mail.com").Department = dep1;

            };

            if (!context.Departments.Any(d => d.Name == "Human Resources"))
            {
                ApplicationUser manager = context.Users.SingleOrDefault(u => u.UserName == "chro@mail.com");
                Department dep1 = new Department() { ID = 1, Name = "Human Resources", Manager = manager };
                context.Departments.AddOrUpdate(dep1);
                context.Users.SingleOrDefault(u => u.UserName == "hr1@mail.com").Department = dep1;

            };

            if (!context.Departments.Any(d => d.Name == "IT"))
            {
                ApplicationUser manager = context.Users.SingleOrDefault(u => u.UserName == "cio@mail.com");
                Department dep1 = new Department() { ID = 1, Name = "IT", Manager = manager };
                context.Departments.AddOrUpdate(dep1);
                context.Users.SingleOrDefault(u => u.UserName == "it1@mail.com").Department = dep1;

            };

            if (!context.Departments.Any(d => d.Name == "Technology"))
            {
                ApplicationUser manager = context.Users.SingleOrDefault(u => u.UserName == "cto@mail.com");
                Department dep1 = new Department() { ID = 1, Name = "Technology", Manager = manager };
                context.Departments.AddOrUpdate(dep1);
                context.Users.SingleOrDefault(u => u.UserName == "tech1@mail.com").Department = dep1;

            };

            if (!context.Departments.Any(d => d.Name == "Research"))
            {
                ApplicationUser manager = context.Users.SingleOrDefault(u => u.UserName == "cro@mail.com");
                Department dep1 = new Department() { ID = 1, Name = "Research", Manager = manager };
                context.Departments.AddOrUpdate(dep1);
                context.Users.SingleOrDefault(u => u.UserName == "researcher1@mail.com").Department = dep1;

            };

            if (!context.Departments.Any(d => d.Name == "Development"))
            {
                ApplicationUser manager = context.Users.SingleOrDefault(u => u.UserName == "cdo@mail.com");
                Department dep1 = new Department() { ID = 1, Name = "Development", Manager = manager };
                context.Departments.AddOrUpdate(dep1);
                context.Users.SingleOrDefault(u => u.UserName == "developer1@mail.com").Department = dep1;

            };
            context.SaveChanges();
#endregion

#region Jocke seed Calender
            var calenders = new Calender[]{
                new Calender{DepartmentID=1,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-14 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-14 10:00:00")},
                new Calender{DepartmentID=1,CalTitle="Monthly Meeting",CalContent="Monthly Meet. Be There!!!",CalenderStart=DateTime.Parse("2017-08-30 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-30 16:00:00")},
                new Calender{DepartmentID=1,CalTitle="Kaizen Meeting",CalContent="Monthly Quality and Improve Meet",CalenderStart=DateTime.Parse("2017-08-22 15:00:00"),CalenderEnd=DateTime.Parse("2017-08-22 16:00:00")},
                new Calender{DepartmentID=1,CalTitle="Workshop",CalContent="How to Employ",CalenderStart=DateTime.Parse("2017-08-16 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-16 16:00:00")},
                new Calender{DepartmentID=1,CalTitle="Workshop",CalContent="How to Dismiss",CalenderStart=DateTime.Parse("2017-08-23 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-23 12:00:00")},
                new Calender{DepartmentID=1,CalTitle="Board Meeting",CalContent="YOU WILL BE THERE!!!!!",CalenderStart=DateTime.Parse("2017-08-17 10:00:00"),CalenderEnd=DateTime.Parse("2017-08-17 14:00:00")},
                new Calender{DepartmentID=1,CalTitle="End of Month Reward",CalContent="Have We Fullfiled Our Goals",CalenderStart=DateTime.Parse("2017-09-01 13:00:00"),CalenderEnd=DateTime.Parse("2017-09-01 16:00:00")},
                new Calender{DepartmentID=1,CalTitle="Team Buildning",CalContent="We Build Our Group",CalenderStart=DateTime.Parse("2017-08-25 12:00:00"),CalenderEnd=DateTime.Parse("2017-08-25 16:00:00")},

                new Calender{DepartmentID=2,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-14 10:15:00"),CalenderEnd=DateTime.Parse("2017-08-14 12:00:00")},
                new Calender{DepartmentID=2,CalTitle="Monthly Meeting",CalContent="Monthly Meet. Be There!!!",CalenderStart=DateTime.Parse("2017-08-31 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-31 12:00:00")},
                new Calender{DepartmentID=2,CalTitle="Kaizen Meeting",CalContent="Monthly Quality and Improve Meet",CalenderStart=DateTime.Parse("2017-08-22 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-22 09:00:00")},
                new Calender{DepartmentID=2,CalTitle="Monthly Report Meeting",CalContent="Monthly Meet. Be There!!!",CalenderStart=DateTime.Parse("2017-08-14 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-14 16:00:00")},
                new Calender{DepartmentID=2,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-24 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-24 10:00:00")},
                new Calender{DepartmentID=2,CalTitle="Monthly Report TO Board",CalContent="Must be FINISHED",CalenderStart=DateTime.Parse("2017-08-17 11:00:00"),CalenderEnd=DateTime.Parse("2017-08-17 12:00:00")},
                new Calender{DepartmentID=2,CalTitle="Team Buildning",CalContent="We Build Our Group",CalenderStart=DateTime.Parse("2017-08-25 12:00:00"),CalenderEnd=DateTime.Parse("2017-08-25 16:00:00")},
                new Calender{DepartmentID=2,CalTitle="Kick Of",CalContent="We kick of the new finance year",CalenderStart=DateTime.Parse("2017-08-18 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-18 16:00:00")},
               
                new Calender{DepartmentID=3,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-14 10:15:00"),CalenderEnd=DateTime.Parse("2017-08-14 12:00:00")},
                new Calender{DepartmentID=3,CalTitle="Monthly Meeting",CalContent="Monthly Meet. Be There!!!",CalenderStart=DateTime.Parse("2017-08-31 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-31 12:00:00")},
                new Calender{DepartmentID=3,CalTitle="Kaizen Meeting",CalContent="Monthly Quality and Improve Meet",CalenderStart=DateTime.Parse("2017-08-22 09:00:00"),CalenderEnd=DateTime.Parse("2017-08-22 10:00:00")},
                new Calender{DepartmentID=3,CalTitle="Monthly Report Meeting",CalContent="Monthly Meet. Be There!!!",CalenderStart=DateTime.Parse("2017-08-14 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-14 16:00:00")},
                new Calender{DepartmentID=3,CalTitle="Needs More Manpower",CalContent="More Manpower to Mars Plant",CalenderStart=DateTime.Parse("2017-08-24 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-24 16:00:00")},
                new Calender{DepartmentID=3,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-29 10:00:00"),CalenderEnd=DateTime.Parse("2017-08-29 12:00:00")},
                new Calender{DepartmentID=3,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-24 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-24 10:00:00")},
                new Calender{DepartmentID=3,CalTitle="Team Buildning",CalContent="We Build Our Group",CalenderStart=DateTime.Parse("2017-08-25 12:00:00"),CalenderEnd=DateTime.Parse("2017-08-25 16:00:00")},

                new Calender{DepartmentID=4,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-14 10:15:00"),CalenderEnd=DateTime.Parse("2017-08-14 12:00:00")},
                new Calender{DepartmentID=4,CalTitle="Monthly Meeting",CalContent="Monthly Meet. Be There!!!",CalenderStart=DateTime.Parse("2017-08-31 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-31 12:00:00")},
                new Calender{DepartmentID=4,CalTitle="Kaizen Meeting",CalContent="Monthly Quality and Improve Meet",CalenderStart=DateTime.Parse("2017-08-22 10:00:00"),CalenderEnd=DateTime.Parse("2017-08-22 11:00:00")},
                new Calender{DepartmentID=4,CalTitle="New Oversight Systems",CalContent="Best Survillance for Mars Plant",CalenderStart=DateTime.Parse("2017-08-28 09:35:00"),CalenderEnd=DateTime.Parse("2017-08-28 11:45:00")},
                new Calender{DepartmentID=4,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-29 10:00:00"),CalenderEnd=DateTime.Parse("2017-08-29 12:00:00")},
                new Calender{DepartmentID=4,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-24 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-24 10:00:00")},
                new Calender{DepartmentID=4,CalTitle="Team Buildning",CalContent="We Build Our Group",CalenderStart=DateTime.Parse("2017-08-25 12:00:00"),CalenderEnd=DateTime.Parse("2017-08-25 16:00:00")},
                new Calender{DepartmentID=4,CalTitle="Scrum Meeting",CalContent="SCRUM..",CalenderStart=DateTime.Parse("2017-08-18 15:00:00"),CalenderEnd=DateTime.Parse("2017-08-18 16:00:00")},

                new Calender{DepartmentID=5,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-14 10:15:00"),CalenderEnd=DateTime.Parse("2017-08-14 12:00:00")},
                new Calender{DepartmentID=5,CalTitle="Monthly Meeting",CalContent="Monthly Meet. Be There!!!",CalenderStart=DateTime.Parse("2017-08-31 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-31 12:00:00")},
                new Calender{DepartmentID=5,CalTitle="Kaizen Meeting",CalContent="Monthly Quality and Improve Meet",CalenderStart=DateTime.Parse("2017-08-22 11:00:00"),CalenderEnd=DateTime.Parse("2017-08-22 12:00:00")},
                new Calender{DepartmentID=5,CalTitle="Hove to solve Connection",CalContent="Hove can we sovle the connection to Mars plant",CalenderStart=DateTime.Parse("2017-08-21 14:00:00"),CalenderEnd=DateTime.Parse("2017-08-21 16:00:00")},
                new Calender{DepartmentID=5,CalTitle="Hove to solve Connection",CalContent="Hove can we sovle the connection to Mars plant",CalenderStart=DateTime.Parse("2017-08-21 10:30:00"),CalenderEnd=DateTime.Parse("2017-08-21 12:00:00")},
                new Calender{DepartmentID=5,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-29 10:00:00"),CalenderEnd=DateTime.Parse("2017-08-29 12:00:00")},
                new Calender{DepartmentID=5,CalTitle="Team Buildning",CalContent="We Build Our Group",CalenderStart=DateTime.Parse("2017-08-25 12:00:00"),CalenderEnd=DateTime.Parse("2017-08-25 16:00:00")},
                new Calender{DepartmentID=5,CalTitle="Scrum Meeting",CalContent="SCRUM..",CalenderStart=DateTime.Parse("2017-08-18 15:00:00"),CalenderEnd=DateTime.Parse("2017-08-18 16:00:00")},

                new Calender{DepartmentID=6,CalTitle="Week Meting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-14 10:15:00"),CalenderEnd=DateTime.Parse("2017-08-14 12:00:00")},
                new Calender{DepartmentID=6,CalTitle="Monthly Meeting",CalContent="Monthly Meet. Be There!!!",CalenderStart=DateTime.Parse("2017-08-31 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-31 12:00:00")},
                new Calender{DepartmentID=6,CalTitle="Kaizen Meetin",CalContent="Monthly Quality and Improve Meet",CalenderStart=DateTime.Parse("2017-08-22 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-22 14:00:00")},
                new Calender{DepartmentID=6,CalTitle="Reasacrh Disaster",CalContent="New invention corrupt",CalenderStart=DateTime.Parse("2017-08-15 10:00:00"),CalenderEnd=DateTime.Parse("2017-08-15 12:00:00")},
                new Calender{DepartmentID=6,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-29 10:00:00"),CalenderEnd=DateTime.Parse("2017-08-29 12:00:00")},
                new Calender{DepartmentID=6,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-24 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-24 10:00:00")},
                new Calender{DepartmentID=6,CalTitle="Team Buildning",CalContent="We Build Our Group",CalenderStart=DateTime.Parse("2017-08-25 12:00:00"),CalenderEnd=DateTime.Parse("2017-08-25 16:00:00")},
                new Calender{DepartmentID=6,CalTitle="Scrum Meeting",CalContent="SCRUM..",CalenderStart=DateTime.Parse("2017-08-18 15:00:00"),CalenderEnd=DateTime.Parse("2017-08-18 16:00:00")},

                new Calender{DepartmentID=7,CalTitle="Week Meeting",CalContent="Week Meet",CalenderStart=DateTime.Parse("2017-08-14 10:15:00"),CalenderEnd=DateTime.Parse("2017-08-14 12:00:00")},
                new Calender{DepartmentID=7,CalTitle="Monthly Meeting",CalContent="Monthly Meet. Be There!!!",CalenderStart=DateTime.Parse("2017-08-31 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-31 12:00:00")},
                new Calender{DepartmentID=7,CalTitle="Kaizen Meeting",CalContent="Monthly Quality and Improve Meet",CalenderStart=DateTime.Parse("2017-08-22 14:00:00"),CalenderEnd=DateTime.Parse("2017-08-22 15:00:00")},
                new Calender{DepartmentID=7,CalTitle="Development ScrewUp",CalContent="This have gone to ....",CalenderStart=DateTime.Parse("2017-08-15 09:15:00"),CalenderEnd=DateTime.Parse("2017-08-15 16:00:00")},
                new Calender{DepartmentID=7,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-29 10:00:00"),CalenderEnd=DateTime.Parse("2017-08-29 12:00:00")},
                new Calender{DepartmentID=7,CalTitle="Week Meeting",CalContent="Week Meeting",CalenderStart=DateTime.Parse("2017-08-24 08:00:00"),CalenderEnd=DateTime.Parse("2017-08-24 10:00:00")},
                new Calender{DepartmentID=7,CalTitle="Team Buildning",CalContent="We Build Our Group",CalenderStart=DateTime.Parse("2017-08-25 12:00:00"),CalenderEnd=DateTime.Parse("2017-08-25 16:00:00")},
                new Calender{DepartmentID=7,CalTitle="Scrum Meeting",CalContent="SCRUM..",CalenderStart=DateTime.Parse("2017-08-18 15:00:00"),CalenderEnd=DateTime.Parse("2017-08-18 16:00:00")},

                new Calender{DepartmentID=1,CalTitle="Prsentation",CalContent="We Present Project",CalenderStart=DateTime.Parse("2017-08-14 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-14 16:00:00")},
                new Calender{DepartmentID=2,CalTitle="Prsentation",CalContent="We Present Project",CalenderStart=DateTime.Parse("2017-08-14 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-14 16:00:00")},
                new Calender{DepartmentID=3,CalTitle="Prsentation",CalContent="We Present Project",CalenderStart=DateTime.Parse("2017-08-14 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-14 16:00:00")},
                new Calender{DepartmentID=4,CalTitle="Prsentation",CalContent="We Present Project",CalenderStart=DateTime.Parse("2017-08-14 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-14 16:00:00")},
                new Calender{DepartmentID=5,CalTitle="Prsentation",CalContent="We Present Project",CalenderStart=DateTime.Parse("2017-08-14 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-14 16:00:00")},
                new Calender{DepartmentID=6,CalTitle="Prsentation",CalContent="We Present Project",CalenderStart=DateTime.Parse("2017-08-14 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-14 16:00:00")},
                new Calender{DepartmentID=7,CalTitle="Prsentation",CalContent="We Present Project",CalenderStart=DateTime.Parse("2017-08-14 13:00:00"),CalenderEnd=DateTime.Parse("2017-08-14 16:00:00")}
            };
            foreach (Calender c in calenders){
                context.CalenderTask.Add(c);
            };
            context.SaveChanges();
#endregion
        }
    }
}
