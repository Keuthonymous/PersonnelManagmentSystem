using PersonnelManagmentSystemV1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class CVVM
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        public HttpPostedFileBase Contents { get; set; }

        //[Required]
        //public virtual ApplicationUser Uploader { get; set; }
    }
}