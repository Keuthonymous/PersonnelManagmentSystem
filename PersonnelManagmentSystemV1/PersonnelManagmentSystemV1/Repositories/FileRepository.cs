using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
using System.Data.Entity;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class FileRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<UserFile> GetAllFiles()
        {
            return context.Files.Include(f => f.Department);
        }

        public UserFile GetFileById(int id)
        {
            return GetAllFiles().SingleOrDefault(f => f.ID == id);
        }

        public void AddFile(UserFile file)
        {
            context.Files.Add(file);
            context.SaveChanges();
        }

        public void ChangeFile(UserFile file)
        {
            context.Entry(file).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteFile(int id)
        {
            context.Files.Remove(GetFileById(id));
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

        private IEnumerable<UserFile> GetFilesForDepartments(IEnumerable<Department> departments)
        {
            return GetAllFiles().Where(info => departments.Contains(info.Department));
        }

        public IEnumerable<UserFile> GetFilesForUser(string userName)
        {
            return GetFilesForDepartments(new List<Department>(){ context.Users.SingleOrDefault(u => u.UserName == userName).Department });
        }

        public IEnumerable<UserFile> GetFilesForUsersManagedDepartments(string userName)
        {
            return GetFilesForDepartments(context.Users.SingleOrDefault(u => u.UserName == userName).ManagedDepartments);
        }
    }
}