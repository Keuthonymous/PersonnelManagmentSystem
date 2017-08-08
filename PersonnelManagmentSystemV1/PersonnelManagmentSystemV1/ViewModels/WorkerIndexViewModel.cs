using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class WorkerIndexViewModel
    {
        public IEnumerable<CalenderViewModel> Events { get; set; }
        public IEnumerable<InformationViewModel> Information { get; set; }
    }
}