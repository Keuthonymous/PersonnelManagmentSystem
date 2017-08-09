using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.IO;

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
        public string Description { get; set; }
        [Required]
        public string MimeType { get; set; }
        [Required]
        public byte[] Content { get; set; }
        [Required]
        public virtual ApplicationUser Uploader { get; set; }
        [Required]
        public DateTime UploadTime { get; set; }
        public string FileName{ get; set; }
        public string GetFileName()
        {
            return Path.GetFileName(FileName);
        }
    }
}