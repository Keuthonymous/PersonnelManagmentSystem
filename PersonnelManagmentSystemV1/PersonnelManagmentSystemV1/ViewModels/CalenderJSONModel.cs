using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class CalenderJSONModel
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CalenderStart { get; set; }
        public DateTime CalenderEnd { get; set; }
        public string CalTitle { get; set; }
        public string CalContent { get; set; }
    }
}