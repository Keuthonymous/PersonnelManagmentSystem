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
        [Display(Name = "Title")]
        public string CalTitle { get; set; }

        [StringLength(200)]
        [Display(Name = "Content")]
        public string CalContent { get; set; }

        [Display(Name = "Event Start")]
        public DateTime CalenderStart { get; set; }

        [Display(Name = "Event End")]
        public DateTime CalenderEnd { get; set; }


    }
}