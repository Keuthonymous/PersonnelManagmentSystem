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
        public ActionResult Index()
        {
            //It works...
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            if (User.IsInRole("Worker"))
            {
                return RedirectToAction("Index", "Worker");
            }
            if (User.IsInRole("Boss"))
            {
                return RedirectToAction("Index", "Boss");
            }
            if (User.IsInRole("Searcher"))
            {
                return RedirectToAction("Index", "Searcher");
            }

            return View();
        }

        public ActionResult Start()
        {
            return View();
        }
    }
}