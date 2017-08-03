using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.Repositories;

namespace PersonnelManagmentSystemV1.Controllers
{
    [Authorize(Roles="Boss, Worker")]
    public class InformationController : Controller
    {
        private InformationRepository db = new InformationRepository();

        private InformationViewModel MapInformationToInformationViewModel(Information info)
        {
            return new InformationViewModel()
            {
                ID = info.ID,
                DepartmentID = info.Department.ID,
                Title = info.Title,
                Content = info.Contents,
                IsPublic = info.IsPublic,
                UploadTime = info.UploadTime
            };
        }

        // GET: Information
        public ActionResult Index()
        {
            return View(db.Informations().ToList());
        }

        [AllowAnonymous]
        public ActionResult PublicNews()
        {
            return PartialView(db.Informations(true));
        }
        // GET: Information/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Information information = db.Information(id.Value);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        [Authorize(Roles="Boss")]
        // GET: Information/Create
        public ActionResult Create()
        {
            ViewBag.Departments = db.Departments().Select(d => new SelectListItem() { Text = d.Name, Value = d.ID.ToString() });
            return View();
        }

        // POST: Information/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Boss")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,IsPublic,DepartmentID")] InformationViewModel information)
        {
            
            
            if (ModelState.IsValid)
            {
                Information info = new Information() { Title = information.Title, ID = information.ID, Contents = information.Content, IsPublic = information.IsPublic, Department = db.Department(information.DepartmentID) };
                //info.UploadTime = DateTime.Now;
                db.AddInformation(info);
                return RedirectToAction("Index");
            }

            return View(information);
        }


        // GET: Information/Edit/5
        [Authorize(Roles = "Boss")]
        public ActionResult Edit(int? id)
        {
            ViewBag.Departments = db.Departments().Select(d => new SelectListItem() { Text = d.Name, Value = d.ID.ToString() });
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationViewModel informationViewModel = MapInformationToInformationViewModel(db.Information(id.Value));
            if (informationViewModel == null)
            {
                return HttpNotFound();
            }
            return View(informationViewModel);
        }

        // POST: Information/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Boss")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Contents,IsPublic,DepartmentID")] InformationViewModel information)
        {
            if (ModelState.IsValid)
            {
                Information info = new Information() { Title = information.Title, ID = information.ID, Contents = information.Content, IsPublic = information.IsPublic, Department = db.Department(information.DepartmentID) };
                db.EditInformation(info);
                return RedirectToAction("Index");
            }
            return View(information);
        }

        // GET: Information/Delete/5
        [Authorize(Roles = "Boss")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Information information = db.Information(id.Value);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        // POST: Information/Delete/5
        [Authorize(Roles = "Boss")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteInformation(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
