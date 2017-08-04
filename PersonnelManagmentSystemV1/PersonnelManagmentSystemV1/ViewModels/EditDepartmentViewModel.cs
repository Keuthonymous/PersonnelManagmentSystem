using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PersonnelManagmentSystemV1.Models;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class EditDepartmentViewModel
    {
        public ApplicationUser Manager { get; set; }

        public string ManagerUserName { get; set; }

        [Display(Name = "Department Name")]
        public string Name { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public IEnumerable<string> UserNames { get; set; }

        public int CurrentDepartmentID { get; set; }
    }
}