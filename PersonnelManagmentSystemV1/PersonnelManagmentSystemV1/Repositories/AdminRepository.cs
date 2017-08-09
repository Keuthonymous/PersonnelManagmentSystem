using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
//using PersonnelManagmentSystemV1.ViewModels;
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
        private UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return db.Users
                .Include(u => u.Department);
        }

        public IEnumerable<string> GetRoleNames()
        {
            List<IdentityRole> roles = db.Roles.ToList();
            List<string> roleNames = new List<string>();

            foreach (var i in roles)
            {
                roleNames.Add(i.Name);
            }

            return roleNames;
        }

        public string GetRoleName(string roleId)
        {
            return (from t in db.Roles
             where t.Id == roleId
             select t.Name).FirstOrDefault();
        }



        public void RemoveUserFromRole(ApplicationUser user, string currentRole)
        {
            userManager.RemoveFromRole(user.Id, currentRole);
            db.SaveChanges();
        }

        public void AddUserToRole(ApplicationUser user, string userRole)
        {
            userManager.AddToRole(user.Id, userRole);
            db.SaveChanges();
        }

        public IEnumerable<string> GetUserIds()
        {
            List<string> userIds = new List<string>();
            List<ApplicationUser> users = new List<ApplicationUser>();

            foreach (var i in users)
            {
                userIds.Add(i.Id);
            }

            return userIds;
        }

        public IEnumerable<string> GetAllUserNames()
        {
            List<ApplicationUser> users = GetAllUsers().ToList();
            List<string> userNames = new List<string>();

            foreach (var u in users)
            {
                userNames.Add(u.UserName);
            }

            return userNames;
        }

        public ApplicationUser GetUserByID(string id)
        {
            return GetAllUsers().SingleOrDefault(u => u.Id == id);
        }

        public IEnumerable<Department> Departments() //!!!! DEPARTMENTS !!!!
        {
            return db.Departments;
        }

        public void AddDepartment(Department department) //!!!! DEPARTMENTS !!!!
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }


        public void EditDepartment(Department department)
        {
            db.Entry(department).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddUserToDepartment(Department department, ApplicationUser user)
        {
            user.Department = department;
            db.SaveChanges();
        }

        public void RemoveUser(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public void RemoveDepartment(int departmentId)
        {
            Department department = db.Departments.Find(departmentId);
            db.Departments.Remove(department);
            db.SaveChanges();
        }

        public Department GetDepartmentByID(int id)//!!!! DEPARTMENTS !!!!
        {
            return db.Departments.SingleOrDefault(d => d.ID == id);
        }

        public void SaveUser(ApplicationUser user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public string GetPrimaryRoleName(string userId)
        {
            List<IdentityUserRole> roles = GetUserByID(userId).Roles.ToList();

            var primaryRole = roles.FirstOrDefault();
            if (primaryRole == null) return "";
            return GetRoleName(primaryRole.RoleId);
        }
    }
}