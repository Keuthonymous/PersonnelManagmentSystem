using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class Information
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Contents { get; set; }

        [Required]
        [Display(Name = "Public Information")]
        public bool IsPublic { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Posted Time")]
        public DateTime UploadTime { get; set; }

        public virtual Department Department { get; set; }
    }
}