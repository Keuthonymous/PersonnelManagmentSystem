using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class MessageViewModel
    {
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

        public int JobOpeningID { get; set; }
        public string JobOpeningName { get; set; }
        public string SenderID { get; set; }
        public string RecipientID { get; set; }
        public string RecipientName { get; set; }
        public int PreviousMessageID { get; set; }
        public int FirstMessageinThreadID { get; set; }
        public string SenderName { get; set; }
        public IEnumerable<MessageViewModel> MessagesInThread { get; set; }

        public MessageViewModel()
        {
            MessagesInThread = new MessageViewModel[] { };
        }

        
    }
}