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

        public bool IsFirstMessage { get { return FirstMessageInThread == this; } }

        public virtual JobOpening JobOpening { get; set; }
        [InverseProperty("ReceivedMessages")]
        public virtual ApplicationUser Sender { get; set; }
        [InverseProperty("SentMessages")]
        public virtual ApplicationUser Recipient { get; set; }
    }
}