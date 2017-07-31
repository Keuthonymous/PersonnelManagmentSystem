using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class AdminIndexListViewModel
    {
        public List<AdminIndexUserViewModel> IndexUsers { get; set; }
        public List<Department> IndexDepartments { get; set; }
    }
}