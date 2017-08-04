﻿using Microsoft.AspNet.Identity;
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
            return db.Users;
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

        public void EditUser(ApplicationUser applicationUser)
        {
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
            //department.Employees.Add(user);
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