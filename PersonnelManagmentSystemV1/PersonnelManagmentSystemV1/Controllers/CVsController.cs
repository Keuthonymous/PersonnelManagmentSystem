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

namespace PersonnelManagmentSystemV1.Controllers
{
    public class CVsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CVs
        public ActionResult Index()
        {
            var cVs = db.CVs.Include(c => c.Uploader);
            return View(cVs.ToList());
        }

        [HttpPost]
        public ActionResult Index(CV cv)
        {
            if (!ModelState.IsValid)
            {
                return View(cv);
            }

            byte[] uploadedFile = new byte[cv.File.InputStream.Length];
            cv.File.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            return Content("File has been uploaded");
        }

        //[HttpPost]
        //public ActionResult CV(CV model)
        //{
        //    var file = model.File;

        //    return View();
        //}

        // GET: CVs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = db.CVs.Find(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            return View(cV);
        }

        // GET: CVs/Create
        public ActionResult Create()
        {
            ViewBag.UploaderID = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: CVs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Content,UploaderID")] CV cV)
        {
            var file = cV.File;

            if (ModelState.IsValid)
            {
                db.CVs.Add(cV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UploaderID = new SelectList(db.ApplicationUsers, "Id", "Email", cV.UploaderID);
            return View(cV);
        }

        // GET: CVs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = db.CVs.Find(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            ViewBag.UploaderID = new SelectList(db.ApplicationUsers, "Id", "Email", cV.UploaderID);
            return View(cV);
        }

        // POST: CVs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Content,UploaderID")] CV cV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UploaderID = new SelectList(db.ApplicationUsers, "Id", "Email", cV.UploaderID);
            return View(cV);
        }

        // GET: CVs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = db.CVs.Find(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            return View(cV);
        }

        // POST: CVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CV cV = db.CVs.Find(id);
            db.CVs.Remove(cV);
            db.SaveChanges();
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
