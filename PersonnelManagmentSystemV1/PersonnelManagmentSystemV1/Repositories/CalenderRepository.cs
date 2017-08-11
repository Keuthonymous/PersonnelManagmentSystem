using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PersonnelManagmentSystemV1.Controllers;
using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.DataAccess;
using System.Data.Entity;
using System.Web.Mvc;



namespace PersonnelManagmentSystemV1.Repositories
{
    public class CalenderRepository
    {
        DateTime TodaysDate = DateTime.Now.Date;

        private ApplicationDbContext db = new ApplicationDbContext();

        public void AddCalender(Calender calevnt)
        {
                db.CalenderTask.Add(calevnt);
                db.SaveChanges();
        }

        public DateTime CalStart(DateTime StartDate, DateTime StartTime)
        {
            return StartDate + new TimeSpan(StartTime.Hour, StartTime.Minute, 0);
        }

        public DateTime CalEnd(DateTime EndDate, DateTime EndTime)
        {
            return EndDate + new TimeSpan(EndTime.Hour, EndTime.Minute, 0);

        }

        private IEnumerable<Calender> GetAllCalenderTasks(IEnumerable<Department> departments)
        {
            return GetAll().Where(cal => departments.Contains(cal.Department));
        }

        public IEnumerable<Calender> GetAll()
        {
            return db.CalenderTask
                .Include(c => c.Department)
                .Include(c => c.Department.Manager);
        }

        public Calender GetEventById(int id)
        {
            return GetAll().SingleOrDefault(m => m.ID == id);
        }

        public void DeleteMessage(int id)
        {
            Calender calender = GetEventById(id);
            db.CalenderTask.Remove(calender);
            db.SaveChanges();
        }

        public void Edit(Calender calender)
        {
            db.Entry(calender).State = EntityState.Modified;
            db.SaveChanges();
        }
        public ApplicationUser GetUserByName(string userName) //!!!! USER !!!!
        {
            return db.Users.Include(c => c.Department).SingleOrDefault(u => u.UserName == userName);
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

        public IEnumerable<Calender>  GetAllCalenderTasksForUser(string userName)
        {
            List<Department> departments = new List<Department>();
            ApplicationUser user = GetUserByName(userName);
            if (user.Department != null)
            {
                departments.Add(user.Department);
            }
            departments.AddRange(user.ManagedDepartments);
            if (departments.Count() > 0)
            {
                return GetAllCalenderTasks(departments);
            }
            else
            {
                return new Calender[] { };
            }
            
        }
    }
}