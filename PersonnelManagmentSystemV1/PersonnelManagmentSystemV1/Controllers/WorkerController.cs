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
            workerIndex.CurrentUser = db.FindUser(workerIndex.CurrentUserID);
            workerIndex.CurrentUserDepartment = workerIndex.CurrentUser.Department;
           
            workerIndex.Events = new List<Calender>();
            if (db.GetEvents(workerIndex.CurrentUserID).Count() != 0)
            {
                workerIndex.Events = db.GetEvents(workerIndex.CurrentUserID).ToList();
            }
            else if (db.GetEvents(workerIndex.CurrentUserID).Count() == 0)
            {
                workerIndex.Events = null;
            }
            
            workerIndex.Information = new List<Information>();
            if (db.GetInformation(workerIndex.CurrentUserID).Count() != 0)
            {
                workerIndex.Information = db.GetInformation(workerIndex.CurrentUserID).ToList();
            }
            else if (db.GetInformation(workerIndex.CurrentUserID).Count() == 0)
            {
                workerIndex.Information = null;
            }
            
            return View(workerIndex);
        }
    }
}