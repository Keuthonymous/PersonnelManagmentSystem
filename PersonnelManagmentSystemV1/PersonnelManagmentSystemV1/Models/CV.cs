using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class CV
    {
        private string originalCV;

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

        public string OriginalCV
        {
            get { return originalCV; }
            set
            { //Set only the filename, not the path/directoryname
                originalCV = Path.GetFileName(value);
            }
        }

        public string GetCV()
        {
            if (OriginalCV != "")
            {
                return OriginalCV;
            }
            else
            {
                return ID.ToString() + "-" + Title;
            }
        }
    }
}