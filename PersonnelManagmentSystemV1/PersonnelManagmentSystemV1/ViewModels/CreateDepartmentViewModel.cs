using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.Models;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class CreateDepartmentViewModel
    {
        public ApplicationUser Manager { get; set; }

        public string ManagerUserName { get; set; }

        [Display(Name = "Department Name")]
        public string Name { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public IEnumerable<string> UserNames { get; set; }
    }
}