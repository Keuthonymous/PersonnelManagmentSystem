using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.Models;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class AdminIndexListViewModel
    {
        public IEnumerable<UserViewModel> IndexUsers { get; set; }
        public IEnumerable<Department> IndexDepartments { get; set; }
    }
}