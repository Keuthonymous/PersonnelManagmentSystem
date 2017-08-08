using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class WorkerRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Calender> GetEvents(string id)
        { 
            var user = db.Users.Find(id);
            if (user.Department == null)
            {
                return null;
            }
            List<Calender> events = db.CalenderTask.Where(c => c.DepartmentID == user.Department.ID).ToList();
            if (events != null)
            {
                return events;
            }
            return null;
        }

        public IEnumerable<Information> GetInformation(string id)
        {
            var user = db.Users.Find(id);
            if (user.Department == null)
            {
                return null;
            }
            List<Information> events = db.Information.Where(c => c.Department.ID == user.Department.ID).ToList();
            if (events != null)
            {
                return events;
            }
            return null;
        }
    }
}