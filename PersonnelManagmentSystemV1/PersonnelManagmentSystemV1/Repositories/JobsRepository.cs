using PersonnelManagmentSystemV1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.DataAccess;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class JobsRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<JobOpening> Jobs()
        {
            return db.Jobs.Include(j => j.Department);
        }

        public IEnumerable<Department> Departments() //!!!! DEPARTMENT !!!!
        {
            return db.Departments
                .Include(d => d.Manager); //Include manager, because it is required in views and lazy loading does not work because only one reader action can be used simultaneously.
        }

        public Department Department(int id) //!!!! DEPARTMENT !!!!
        {
            return db.Departments.SingleOrDefault(d => d.ID == id);
        }

        public IEnumerable<Department> GetManagedDepartmentsByUserName(string userName) //!!!!! DEPARTMENT !!!!
        {
            return db.Users.SingleOrDefault(u => u.UserName == userName).ManagedDepartments;
        }

        public JobOpening Job(int id)
        {
            return Jobs().SingleOrDefault(j => j.ID == id);
        }

        public void Add(JobOpening job)
        {
            db.Jobs.Add(job);
            db.SaveChanges();
        }

        public void Edit(JobOpening job)
        {
            db.Entry(job).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove(JobOpening job)
        {
            db.Jobs.Remove(job);
            db.SaveChanges();
        }
    }
}