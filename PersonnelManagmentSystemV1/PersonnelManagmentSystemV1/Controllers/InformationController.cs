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

        // GET: Information
        public ActionResult Index()
        {
            return View(db.Informations());
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
            return View();
        }

        // POST: Information/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Boss")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Contents,IsPublic")] Information information)
        {
            information.UploadTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.AddInformation(information);
                return RedirectToAction("Index");
            }

            return View(information);
        }


        // GET: Information/Edit/5
        [Authorize(Roles = "Boss")]
        public ActionResult Edit(int? id)
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

        // POST: Information/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Boss")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Contents,IsPublic,UploadTime")] Information information)
        {
            if (ModelState.IsValid)
            {
                db.EditInformation(information);
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
