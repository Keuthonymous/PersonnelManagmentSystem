using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.DataAccess;
using System.Data.Entity;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class InformationRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Information> Informations(bool publicOnly = false)
        {
            if (publicOnly)
            {
                return db.Information
                        .Include(i => i.Department)
                        .Where(i => i.IsPublic);
            }
            return db.Information
                .Include(i => i.Department);
        }

        public IEnumerable<Information> Information()
        {
            return db.Information.Include(i => i.Department);
        }

        public Department Department(int id) //!!!! DEPARTMENT !!!!
        {
            return db.Departments.SingleOrDefault(d => d.ID == id);
        }

        public IEnumerable<Department> Departments() //!!!! DEPARTMENT !!!!
        {
            return db.Departments
                .Include(d => d.Manager); //Include manager, because it is required in views and lazy loading does not work because only one reader action can be used simultaneously.
        }

        public Information Information(int id)
        {
            return db.Information.SingleOrDefault(d => d.ID == id);
        }

        public void AddInformation(Information info)
        {
            info.UploadTime = DateTime.Now;
            db.Information.Add(info);
            db.SaveChanges();
        }

        public void EditInformation(Information info)
        {
            db.Entry(info).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteInformation(int id)
        {
            Information information = Information(id);
            db.Information.Remove(information);
            db.SaveChanges();
        }

        public IEnumerable<Department> GetManagedDepartmentsByUserName(string userName) //!!!!! DEPARTMENT !!!!
        {
            return db.Users.SingleOrDefault(u => u.UserName == userName).ManagedDepartments;
        }

        public IEnumerable<Information> InformationsForUser(string userName)
        {
            List<Department> departments = new List<Department>(){db.Users.SingleOrDefault(u => u.UserName == userName).Department};

            return Informations().Where(info => departments.Contains(info.Department));
        }

        public IEnumerable<Information> InformationsForUsersManagedDepartments(string userName)
        {
            List<Department> departments = db.Users.SingleOrDefault(u => u.UserName == userName).ManagedDepartments.ToList();

            return Informations().Where(info => departments.Contains(info.Department));
        }
    }
}