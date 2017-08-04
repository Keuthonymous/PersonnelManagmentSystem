using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PersonnelManagmentSystemV1.Models;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class AddUserToDepartmentViewModel
    {
        public ApplicationUser UserToAdd { get; set; }
        public List<Department> DepartmentList { get; set; }
        public string UserToAddID { get; set; }

        public int? SelectedDepartment { get; set; }
    }
}