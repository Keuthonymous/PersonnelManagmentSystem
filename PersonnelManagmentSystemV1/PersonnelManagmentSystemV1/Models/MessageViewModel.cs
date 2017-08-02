using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class MessageViewModel
    {
        public int ID { get; set; }

        public Message FirstMessageInThread { get; set; }

        [Required]
        [Display(Name = "Subject")]
        [StringLength(200, ErrorMessage = "The subject cannot be longer than 200 characters")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Body")]
        public string BodyContent { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Time Sent")]
        public DateTime SendTime { get; set; }

        public int JobOpeningID { get; set; }
        public string SenderID { get; set; }
        public string RecipientID { get; set; }
    }
}