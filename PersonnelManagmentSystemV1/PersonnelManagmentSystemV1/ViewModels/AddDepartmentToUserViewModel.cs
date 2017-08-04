using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.Models;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class AddDepartmentToUserViewModel
    {
        public Department SelectDepartment { get; set; }
        public List<ApplicationUser> UsersList { get; set; }
        public List<ApplicationUser> UsersToAdd { get; set; }
    }
}