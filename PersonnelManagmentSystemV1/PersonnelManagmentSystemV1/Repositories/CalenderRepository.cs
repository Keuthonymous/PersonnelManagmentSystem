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

        public List<Calender> GetAllCalenderTasks(IEnumerable<Department> departments)
        {
            List<Calender> result = db.CalenderTask.ToList();
            for (var i = result.Count - 1; i > -1; i--)
            {
                if (!departments.Any(d => d.ID == result[i].Department.ID))
                {
                    result.RemoveAt(i);
                }
            }
            return result;
        }

        public List<Calender> GetAll()
        {
            return db.CalenderTask.ToList();
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
            return db.Users.Include("Department").SingleOrDefault(u => u.UserName == userName);
        }
    }
}