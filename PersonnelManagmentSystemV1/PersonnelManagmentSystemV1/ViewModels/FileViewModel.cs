using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class FileViewModel: IValidatableObject
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string FileName { get; set; }

        public HttpPostedFileBase Contents { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        public bool AllowEdit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ID == 0 && Contents == null)
            {
                yield return new ValidationResult("File to upload is mandatory when uploading a new file", new[] { "Contents" });
            }
        }
    }


}