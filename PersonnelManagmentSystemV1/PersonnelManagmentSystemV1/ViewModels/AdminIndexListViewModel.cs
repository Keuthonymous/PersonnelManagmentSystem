using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.Models;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class AdminIndexListViewModel
    {
        public List<AdminIndexUserViewModel> IndexUsers { get; set; }
        public List<Department> IndexDepartments { get; set; }
    }
}