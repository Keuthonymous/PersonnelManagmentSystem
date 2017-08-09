using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.Models;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class InformationViewModel
    {
        
        public int ID { get; set; }
        [Required]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Public Information")]
        public bool IsPublic { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Posted Time")]
        public DateTime UploadTime { get; set; }

        public bool IsEditable { get; set; }

        public static InformationViewModel MapInformationToInformationViewModel(Information info)
        {
            if (info == null)
                return null;

            return new InformationViewModel()
            {
                ID = info.ID,
                DepartmentID = info.Department.ID,
                DepartmentName = info.Department.Name,
                Title = info.Title,
                Content = info.Contents,
                IsPublic = info.IsPublic,
                UploadTime = info.UploadTime
            };
        }
    }
}