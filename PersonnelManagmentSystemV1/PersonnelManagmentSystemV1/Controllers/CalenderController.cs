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

namespace PersonnelManagmentSystemV1.Controllers
{
    public class CalenderController : Controller
    {
        private CalenderRepository calrepo = new CalenderRepository();

        #region Index Get
        // GET: Calender
        [Authorize(Roles="Boss")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
           // DepartmentRepository depRepo = new DepartmentRepository();


            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = await userManager.FindByNameAsync(User.Identity.Name);

            List<SelectListItem> departments = new List<SelectListItem>();
            //if (currentUser.Department != null)
            //{
            //    departments.Add(new SelectListItem() { Text = currentUser.Department.Name, Value = currentUser.Department.ID.ToString() });
            //}
            departments.AddRange(currentUser.ManagedDepartments.Select(dep => new SelectListItem() { Text = dep.Name, Value = dep.ID.ToString() }));

            ViewBag.DepartmentList = departments;

            var calenderVM = new CalenderVM
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

        #region Edit

        // GET: Garage/Edit/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            calrepo.DeleteMessage(id);
            return RedirectToAction("Index");
        }
#endregion


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}