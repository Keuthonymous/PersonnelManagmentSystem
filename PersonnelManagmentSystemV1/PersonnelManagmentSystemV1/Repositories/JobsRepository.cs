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

        public IEnumerable<Department> Departments()
        {
            return db.Departments
                .Include(d => d.Manager); //Include manager, because it is required in views and lazy loading does not work because only one reader action can be used simultaneously.
        }

        public Department Department(int id)
        {
            return db.Departments.SingleOrDefault(d => d.ID == id);
        }

        public JobOpening Job(int? id)
        {
            return db.Jobs.Where(j => j.ID == id).First();
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

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        // This code added to correctly implement the disposable pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}