using PersonnelManagmentSystemV1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonnelManagmentSystemV1.Controllers
{
    [Authorize(Roles = "Worker")]
    public class WorkerController : Controller
    {
        private WorkerRepository db = new WorkerRepository();
        // GET: Worker
        public ActionResult Index()
        {
            return View();
        }
    }
}