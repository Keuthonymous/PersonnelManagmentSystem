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

        public DbSet<JobOpening> Jobs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Calender> CalenderTask { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserFile> Files { get; set; }
        public DbSet<CV> CVs { get; set; }

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