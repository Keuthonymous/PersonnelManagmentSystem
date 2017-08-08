using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.ViewModels;
using PersonnelManagmentSystemV1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PersonnelManagmentSystemV1.DataAccess;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PersonnelManagmentSystemV1.Controllers
{
    public class CalenderController : Controller
    {
        private CalenderRepository calrepo = new CalenderRepository();

        #region Index Get

        // GET: Calender
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Index()
        {


            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = await userManager.FindByNameAsync(User.Identity.Name);

            List<SelectListItem> departments = new List<SelectListItem>();

            departments.AddRange(currentUser.ManagedDepartments.Select(dep => new SelectListItem() { Text = dep.Name, Value = dep.ID.ToString() }));

            ViewBag.DepartmentList = departments;

            var calenderVM = new CalenderViewModel
            {
                StartDate = DateTime.Today,
                StartTime = DateTime.Now,
                EndDate = DateTime.Today,
                EndTime = DateTime.Now
            };

            return View(calenderVM);
        }
        #endregion

        #region Index Post
        [Authorize(Roles = "Boss")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,StartDate,StartTime,EndDate,EndTime,DepartmentID,CalTitle,CalContent,")] CalenderViewModel calevntVM)
        {
            DateTime calenderstart = calrepo.CalStart(calevntVM.StartDate, calevntVM.StartTime);
            DateTime calenderend = calrepo.CalEnd(calevntVM.EndDate, calevntVM.EndTime);

            if (ModelState.IsValid)
            {
                Calender calevnt = new Calender() { DepartmentID = calevntVM.DepartmentID, CalenderStart = calenderstart, CalenderEnd = calenderend, CalTitle = calevntVM.CalTitle, CalContent = calevntVM.CalContent };

                calrepo.AddCalender(calevnt);
                return RedirectToAction("Events");
            }
            return View(calevntVM);
        }
        #endregion

        #region Events
        [Authorize]
        public async Task<ActionResult> Events()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = await userManager.FindByNameAsync(User.Identity.Name);

            List<Department> departments = new List<Department>();
            if (currentUser.Department != null)
            {
                departments.Add(currentUser.Department);
            }
            departments.AddRange(currentUser.ManagedDepartments);
            return View(calrepo.GetAllCalenderTasks(departments));
        }
        #endregion

        #region EventsJSON
        [Authorize(Roles = "Worker, Boss")]
        public string EventsJSON()
        {

            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = calrepo.GetUserByName(User.Identity.Name);

            List<Department> departments = new List<Department>();
            if (currentUser.Department != null)
            {
                departments.Add(currentUser.Department);
            }
            departments.AddRange(currentUser.ManagedDepartments);

            return JsonConvert.SerializeObject(calrepo.GetAllCalenderTasks(departments),
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, DateFormatString = "yyyy-MM-dd HH:mm" });

        }

        #endregion

        #region Edit

        // GET: Garage/Edit/5
        [Authorize(Roles = "Boss")]
        public async Task<ActionResult> Edit(int id)
        {

            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = await userManager.FindByNameAsync(User.Identity.Name);

            List<SelectListItem> departments = new List<SelectListItem>();

            departments.AddRange(currentUser.ManagedDepartments.Select(dep => new SelectListItem() { Text = dep.Name, Value = dep.ID.ToString() }));

            ViewBag.DepartmentList = departments;

            return View(calrepo.GetEventById(id));
        }

        // POST: Garage/Edit/5
        [HttpPost]
        [Authorize(Roles = "Boss")]
        public ActionResult Edit(Calender calender)
        {
            try
            {
                calrepo.Edit(calender);
                return RedirectToAction("Events");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Delete
        [Authorize(Roles = "Boss")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calender calender = calrepo.GetEventById(id.Value);
            if (calender == null)
            {
                return HttpNotFound();
            }
            return View(calender);
        }

        // POST: Message/Delete/5
        [Authorize(Roles = "Boss")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            calrepo.DeleteMessage(id);
            return RedirectToAction("Events");
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}