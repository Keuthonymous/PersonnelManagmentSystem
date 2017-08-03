using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class JobOpening
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Job title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Job description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Job Type")]
        public string JobType { get; set; }

        public JobOpening()
        {
            Messages = new List<Message>();
            Department = new Department();

        }
        public virtual Department Department { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public IEnumerable<ApplicationUser> GetAllApplicants()
        {
            return  Messages
                .Where(m => m.IsFirstMessage)
                .Select(m => m.Sender)
                .Distinct();
        }
    }
}