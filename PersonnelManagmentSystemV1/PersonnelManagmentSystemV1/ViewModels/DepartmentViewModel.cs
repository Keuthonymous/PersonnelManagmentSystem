using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class DepartmentViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public IEnumerable<UserViewModel> Employees { get; set; }

        public static DepartmentViewModel MapDepartment(PersonnelManagmentSystemV1.Models.Department dep)
        {
            return new DepartmentViewModel()
            {
                ID = dep.ID,
                Name = dep.Name,
                ManagerName = dep.Manager.UserName,
                Employees = dep.Employees.Select(u => UserViewModel.MapUser(u))
            };
        }
    }
}