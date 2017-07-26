using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class AdminRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return db.Users;
        }

        public ApplicationUser Find(string id)
        {
            return db.Users.Where(u => u.Id == id).First();
        }
    }
}