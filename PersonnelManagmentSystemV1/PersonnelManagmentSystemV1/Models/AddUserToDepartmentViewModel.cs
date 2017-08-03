using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class AddUserToDepartmentViewModel
    {
        public ApplicationUser UserToAdd { get; set; }
        public List<Department> DepartmentList { get; set; }
        public string UserToAddID { get; set; }

        public int? SelectedDepartment { get; set; }
    }
}