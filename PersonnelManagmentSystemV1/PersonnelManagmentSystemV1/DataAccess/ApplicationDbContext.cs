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

        protected virtual void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasRequired<ApplicationUser>(d => d.Manager).WithMany(m => m.ManagedDepartments);
            modelBuilder.Entity<Department>().HasMany(d => d.Employees).WithOptional(e => e.Department);
           // modelBuilder.Entity<ApplicationUser>().HasOptional(u => u.Department).WithMany(d => d.Employees);
            modelBuilder.Entity<CV>().HasRequired(c => c.Uploader).WithMany(u => u.CVs);
            modelBuilder.Entity<Information>().HasRequired(i => i.Department).WithMany(d => d.Informations);
            modelBuilder.Entity<JobOpening>().HasRequired(j => j.Department).WithMany(d => d.JobOpenings);
            modelBuilder.Entity<Message>().HasRequired(m => m.JobOpening).WithMany(j => j.Messages);
            modelBuilder.Entity<Message>().HasOptional(m => m.FirstMessageInThread).WithRequired(m => m);
            modelBuilder.Entity<Message>().HasRequired(m => m.Sender).WithMany(u => u.SentMessages);
            modelBuilder.Entity<Message>().HasRequired(m => m.Recipient).WithMany(u => u.ReceivedMessages);
        }

        public System.Data.Entity.DbSet<PersonnelManagmentSystemV1.Models.JobOpening> Jobs { get; set; }

        public System.Data.Entity.DbSet<PersonnelManagmentSystemV1.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<PersonnelManagmentSystemV1.Models.Information> Information { get; set; }
    }
}