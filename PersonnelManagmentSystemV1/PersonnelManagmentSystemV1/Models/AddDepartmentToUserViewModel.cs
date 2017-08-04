using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class AddDepartmentToUserViewModel
    {
        public Department SelectDepartment { get; set; }
        public List<ApplicationUser> UsersList { get; set; }
        public List<ApplicationUser> UsersToAdd { get; set; }
    }
}