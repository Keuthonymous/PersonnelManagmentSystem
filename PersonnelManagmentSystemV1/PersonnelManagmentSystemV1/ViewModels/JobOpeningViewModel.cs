using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class JobOpeningViewModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentID { get; set; }
        
        [Required]
        [Display(Name="Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name="Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name="Job Type")]
        public string JobType { get; set; }

    }
}