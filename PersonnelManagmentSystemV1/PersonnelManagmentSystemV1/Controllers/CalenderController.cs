using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using PersonnelManagmentSystemV1.DataAccess;

namespace PersonnelManagmentSystemV1.Controllers
{
    public class CalenderController : Controller
    {
        private CalenderRepository calrepo = new CalenderRepository();


        // GET: Calender
        [HttpGet]
        public ActionResult Index()
        {
            DepartmentRepository depRepo = new DepartmentRepository();

            ViewBag.DepartmentList = depRepo.Departments().Select(dep => new SelectListItem() { Text = dep.ID.ToString() + " | " + dep.Name, Value = dep.ID.ToString() });

            var calenderVM = new CalenderVM
            {
                StartDate = DateTime.Today,
                StartTime = DateTime.Now,
                EndDate = DateTime.Today,
                EndTime = DateTime.Now
            };

            return View(calenderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,StartDate,StartTime,EndDate,EndTime,DepartmentID,CalTitle,CalContent,")] CalenderVM calevntVM)
        {
            DateTime calenderstart = calrepo.CalStart(calevntVM.StartDate, calevntVM.StartTime);
            DateTime calenderend = calrepo.CalEnd(calevntVM.EndDate, calevntVM.EndTime);

            if (ModelState.IsValid)
            {
                Calender calevnt = new Calender() { DepartmentID = calevntVM.DepartmentID, CalenderStart = calenderstart, CalenderEnd = calenderend, CalTitle = calevntVM.CalTitle, CalContent = calevntVM.CalContent };

                calrepo.AddCalender(calevnt);
                return RedirectToAction("Index");
            }
            return View(calevntVM);
        }


    }
}