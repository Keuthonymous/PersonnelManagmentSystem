using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [ForeignKey("Manager")]
        public string ManagerID { get; set; }
        [InverseProperty("ManagedDepartments")]
        public virtual ApplicationUser Manager { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<ApplicationUser> Employees { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<JobOpening> JobOpenings { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Information> Informations { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<UserFile> Files { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Calender> Calenders { get; set; }
    }


}