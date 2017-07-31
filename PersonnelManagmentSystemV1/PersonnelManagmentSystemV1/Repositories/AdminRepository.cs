using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
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
            return db.Users;
        }

        public IEnumerable<AdminIndexUserViewModel> GetIndexList()
        {
            List<ApplicationUser> users = db.Users.ToList();
            List<AdminIndexUserViewModel> indexList = new List<AdminIndexUserViewModel>();
            List<IdentityRole> roles = db.Roles.ToList();
  
            foreach (var i in users)
            {
                indexList.Add(new AdminIndexUserViewModel { Email = i.Email, UserName = i.UserName });
            }
            foreach (var i in indexList)
            {
                i.RoleName = (from t in roles
                              where t.Id == i.Role
                              select t.Name).First();
            }
            return indexList;
        }

        public IEnumerable<string> GetRoles()
        {
            List<IdentityRole> roles = db.Roles.ToList();
            List<string> roleNames = new List<string>();

            foreach (var i in roles)
            {
                roleNames.Add(i.Name);
            }

            return roleNames;
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

        public ApplicationUser FindUser(string id)
        {
            return db.Users.Where(u => u.Id == id).First();
        }

        public IEnumerable<Department> Departments()
        {
            return db.Departments;
        }

        public Department FindDepartment(int? id)
        {
            return db.Departments.Where(d => d.ID == id).First();
        }

        public void AddUser(ApplicationUser applicationUser)
        {
            db.Users.Add(applicationUser);
            db.SaveChanges();
        }

        public void AddDepartment(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }

        public void EditUser(ApplicationUser applicationUser, EditUserViewModel editUser)
        {
            if (editUser.Email != null)
            {
                applicationUser.Email = editUser.Email;
            }

            if (editUser.Password != null)
            {
                userManager.RemovePassword(applicationUser.Id);
                userManager.AddPassword(applicationUser.Id, editUser.Password);
            }
            db.SaveChanges();
        }

        public void EditDepartment(Department department)
        {
            db.Entry(department).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveUser(ApplicationUser user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public void RemoveDepartment(Department department)
        {
            db.Departments.Remove(department);
            db.SaveChanges();
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