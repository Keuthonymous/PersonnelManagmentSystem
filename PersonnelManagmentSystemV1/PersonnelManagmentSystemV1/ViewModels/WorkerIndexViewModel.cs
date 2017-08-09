using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.Models;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class WorkerIndexViewModel
    {
       /* public List<Calender> Events { get; set; }
        public List<Information> Information { get; set; }
        public string CurrentUserID { get; set; }
        public ApplicationUser CurrentUser { get; set; }*/
        public string DepartmentName { get; set; }
        public IEnumerable<CalenderViewModel> Events { get; set; }
        public IEnumerable<InformationViewModel> Information { get; set; }
    }
}