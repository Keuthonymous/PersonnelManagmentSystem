using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PersonnelManagmentSystemV1.Models;
using System.Data.Entity;

namespace PersonnelManagmentSystemV1.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<PersonnelManagmentSystemV1.Models.JobOpening> Jobs { get; set; }

        public System.Data.Entity.DbSet<PersonnelManagmentSystemV1.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<PersonnelManagmentSystemV1.Models.Information> Information { get; set; }

        public System.Data.Entity.DbSet<PersonnelManagmentSystemV1.Models.Calender> CalenderTask { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Calender>()
            .Property(f => f.CalenderStart)
            .HasColumnType("datetime2");

             modelBuilder.Entity<Calender>()
            .Property(f => f.CalenderEnd)
            .HasColumnType("datetime2");

             base.OnModelCreating(modelBuilder);
        }

    }
}