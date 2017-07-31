using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelManagmentSystemV1.Models
{
    public class AdminIndexUserViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
    }
}