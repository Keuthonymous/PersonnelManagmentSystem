using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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



        public List<Calender> GetAllCalenderTasks()
        {
            return db.CalenderTask.ToList();
        }

    }


}