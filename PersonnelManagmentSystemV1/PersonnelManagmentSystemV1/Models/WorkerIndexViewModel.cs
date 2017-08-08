using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class WorkerIndexViewModel
    {
        public List<Calender> Events { get; set; }
        public List<Information> Information { get; set; }
        public string CurrentUserID { get; set; }
    }
}