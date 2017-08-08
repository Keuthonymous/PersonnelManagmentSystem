using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.IO;

namespace PersonnelManagmentSystemV1.Models
{
    public class UserFile
    {
        private string originalFilename;

        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        public string MimeType { get; set; }

        public string OriginalFilename 
        {
            get { return originalFilename;}
            set 
            { //Set only the filename, not the path/directoryname
                originalFilename = Path.GetFileName(value);  
            }
        }
        [Required]
        public byte[] Content { get; set; }
        [Required]
        public virtual Department Department { get; set; }

        public string GetFileName()
        {
            if (OriginalFilename != "")
            {
                return OriginalFilename;
            }
            else
            {
                return ID.ToString() + "-" + Title;
            }
        }
    }
}