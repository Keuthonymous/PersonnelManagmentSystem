using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        
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
    
        [ForeignKey("Sender")]
        public string SenderID { get; set; }
        public virtual ApplicationUser Sender { get; set; }

        public virtual ICollection<ApplicationUser> Recipiants { get; set; }
    }
}