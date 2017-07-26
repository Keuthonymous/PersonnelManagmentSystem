using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class AdminRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return db.Users;
        }

        public ApplicationUser FindUser(string id)
        {
            return db.Users.Where(u => u.Id == id).First();
        }

        public IEnumerable<Department> Departments()
        {
            return db.Departments;
        }

        public Department FindDepartment(int? id)
        {
            return db.Departments.Where(d => d.ID == id).First();
        }

        public void AddUser(ApplicationUser applicationUser)
        {
            db.Users.Add(applicationUser);
            db.SaveChanges();
        }

        public void AddDepartment(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }

        public void EditUser(ApplicationUser applicationUser)
        {
            db.Entry(applicationUser).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void EditDepartment(Department department)
        {
            db.Entry(department).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveUser(ApplicationUser user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public void RemoveDepartment(Department department)
        {
            db.Departments.Remove(department);
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