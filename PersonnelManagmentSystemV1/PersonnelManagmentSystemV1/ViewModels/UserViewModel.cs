using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class UserViewModel
    {
        public string ID { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int DepartmentID { get; set; }
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        public IEnumerable<PersonnelManagmentSystemV1.Models.Department> Departments { get; set; }
        public IEnumerable<SelectListItem> GetSelectItemsDepartment()
        {
            return Departments.Select(dep => new SelectListItem() { Text = dep.Name, Value = dep.ID.ToString() });
        }


        
    }
}