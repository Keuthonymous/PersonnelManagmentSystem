using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonnelManagmentSystemV1.Repositories;

namespace PersonnelManagmentSystemV1.Controllers
{
    public class HomeController : Controller
    {
        private InformationRepository repo = new InformationRepository();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(repo.Informations(true));
        }
    }
}