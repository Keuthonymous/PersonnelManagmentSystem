using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class CvRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<CV> GetAllCVs()
        {
            return context.CVs.Include(c => c.Uploader);
        }

        public CV GetCvById(int id)
        {
            return GetAllCVs().SingleOrDefault(c => c.ID == id);
        }

        public void AddCv(CV cv)
        {
            context.CVs.Add(cv);
            context.SaveChanges();
        }

        public void ChangeCv(CV cv)
        {
            context.Entry(cv).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteCv(int id)
        {
            context.CVs.Remove(GetCvById(id));
            context.SaveChanges();
        }

        public IEnumerable<Department> GetManagedDepartmentsByUserName(string userName) //!!!!! DEPARTMENT !!!!
        {
            return context.Users.SingleOrDefault(u => u.UserName == userName).ManagedDepartments;
        }
        public Department Department(int id) //!!!! DEPARTMENT !!!!
        {
            return context.Departments.SingleOrDefault(d => d.ID == id);
        }

        public ApplicationUser GetUserByName(string userName) //!!!! USER !!!!
        {
            return context.Users.SingleOrDefault(u => u.UserName == userName);
        }
    }
}