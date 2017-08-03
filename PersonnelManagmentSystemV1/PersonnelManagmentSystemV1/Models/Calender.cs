using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PersonnelManagmentSystemV1.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonnelManagmentSystemV1.Models
{
    public class Calender
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [StringLength(25)]
        public string CalTitle { get; set; }

        [StringLength(200)]
        public string CalContent { get; set; }

        public DateTime CalenderStart { get; set; }

        public DateTime CalenderEnd { get; set; }


    }
}