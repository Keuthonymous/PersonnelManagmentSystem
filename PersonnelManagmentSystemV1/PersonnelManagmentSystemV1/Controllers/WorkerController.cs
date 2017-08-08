using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.ViewModels;
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
            WorkerIndexViewModel workerIndex = new WorkerIndexViewModel()
            {
                Events = db.GetEvents(User.Identity.GetUserId()).Select(e => CalenderViewModel.MapCalenderToCalenderViewModel(e)),
                Information = db.GetInformation(User.Identity.GetUserId()).Select(i => InformationViewModel.MapInformationToInformationViewModel(i))
            };            
            return View(workerIndex);
        }
    }
}