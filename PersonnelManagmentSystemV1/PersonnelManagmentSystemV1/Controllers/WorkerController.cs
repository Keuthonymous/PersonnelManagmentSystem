using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace PersonnelManagmentSystemV1.Controllers
{
    [Authorize(Roles = "Worker")]
    public class WorkerController : Controller
    {
        private WorkerRepository db = new WorkerRepository();

        // GET: Worker
        public ActionResult Index()
        {
            WorkerIndexViewModel workerIndex = new WorkerIndexViewModel();
            workerIndex.CurrentUserID = User.Identity.GetUserId();
           
            workerIndex.Events = new List<Calender>();
            if (db.GetEvents(workerIndex.CurrentUserID) != null)
            {
                workerIndex.Events = db.GetEvents(workerIndex.CurrentUserID).ToList();
            }
            workerIndex.Events = null;
            
            workerIndex.Information = new List<Information>();
            if (db.GetInformation(workerIndex.CurrentUserID) != null)
            {
                workerIndex.Information = db.GetInformation(workerIndex.CurrentUserID).ToList();
            }
            workerIndex.Information = null;
            
            return View(workerIndex);
        }
    }
}