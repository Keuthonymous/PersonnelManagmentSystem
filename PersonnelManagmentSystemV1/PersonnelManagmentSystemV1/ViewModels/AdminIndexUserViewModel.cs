using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.ViewModels
{
    public class AdminIndexUserViewModel
    {
        public string ID { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Identity User Role")]
        public IdentityUserRole Role { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}