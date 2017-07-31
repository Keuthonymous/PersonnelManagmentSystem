using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class CV
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(100, ErrorMessage = "The description cannot be more than 100 characters")]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

     //   [Required]
      //  public HttpPostedFileBase File { get; set; }

        [ForeignKey("Uploader")]
        public string UploaderID { get; set; }
        public virtual ApplicationUser Uploader { get; set; }
    }
}