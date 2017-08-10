using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class DepartmentRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Department> Departments()
        {
            return db.Departments
                .Include(d => d.Employees) //Include Employees, because it is required in views and lazy loading does not work because only one reader action can be used simultaneously.
                .Include(d => d.Manager); //Include manager, because it is required in views and lazy loading does not work because only one reader action can be used simultaneously.
        }

        public Department Department(int id)
        {
            return db.Departments.SingleOrDefault(d => d.ID == id);
        }

        public IEnumerable<Department> GetDepartmentsForManagerID(string managerID)
        {
            return Departments().Where(d => d.ManagerID == managerID);
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