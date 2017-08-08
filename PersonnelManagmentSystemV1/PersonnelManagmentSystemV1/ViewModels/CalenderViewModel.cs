using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class CalenderViewModel
    {
        [Required]
        public int DepartmentID { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(25, ErrorMessage = "The event can not be longer then 25 carracters")]
        public string CalTitle { get; set; }

        [Required]
        [Display(Name = "Content")]
        [StringLength(200, ErrorMessage = "The event can not be longer then 200 charhacters")]
        public string CalContent { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        public static CalenderViewModel MapCalenderToCalenderViewModel(Models.Calender cal)
        {
            if (cal == null)
                return null;

            return new CalenderViewModel()
            {
                DepartmentID = cal.DepartmentID,
                CalTitle = cal.CalTitle,
                CalContent = cal.CalContent,
                StartDate = cal.CalenderStart.Date,
                StartTime = cal.CalenderStart,
                EndDate = cal.CalenderEnd.Date,
                EndTime = cal.CalenderEnd
            };
        }
    }
}