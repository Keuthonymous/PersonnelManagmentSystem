using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace PersonnelManagmentSystemV1.Repositories
{
    public class WorkerRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Calender> GetEvents(string userId)
        { 
            var user = db.Users.Find(userId);
            if (user.Department == null)
            {
                return null;
            }
            return db.CalenderTask.Where(c => c.DepartmentID == user.Department.ID);
        }

        public IEnumerable<Information> GetInformation(string id)
        {
            var user = db.Users.Find(id);
            if (user.Department == null)
            {
                return null;
            }
            List<Information> information = db.Information.Where(c => c.Department.ID == user.Department.ID).ToList();
            if (information != null)
            {
                return information;
            }
            return null;
        }

        public ApplicationUser FindUser(string id)
        {
            return db.Users.Include(u => u.Department).SingleOrDefault(u => u.Id == id);
        }

        public Department FindDepartment(int id)
        {
            return db.Departments.Where(d => d.ID == id).First();
        }
    }
}