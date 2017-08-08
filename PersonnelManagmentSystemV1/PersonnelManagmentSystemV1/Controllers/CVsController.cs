using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.Repositories;
using System.IO;
using PersonnelManagmentSystemV1.ViewModels;
using Microsoft.AspNet.Identity;
using PersonnelManagmentSystemV1.DataAccess;

namespace PersonnelManagmentSystemV1.Controllers
{
    public class CVsController : Controller
    {
        private CvRepository repo = new CvRepository();

        // GET: CVs
        public ActionResult Index()
        {
            return View(repo.GetAllCVs());
        }

        [Authorize(Roles = "Searcher")]
        [HttpGet]
        public ActionResult UploadCv()
        {
            return View();
        }

        [Authorize(Roles = "Searcher")]
        [HttpPost]
        public ActionResult UploadCv([Bind(Include = "ID,Title,Description,Contents")] CVVM cvVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CV cv = new CV()
                    {
                        Title = cvVM.Title,
                        Description = cvVM.Description,
                        MimeType = cvVM.Contents.ContentType,
                        Uploader = repo.GetUserByName(User.Identity.Name),
                        Content = null
                    };
          
                    BinaryReader binaryReader = new BinaryReader(cvVM.Contents.InputStream);
                    cv.Content = binaryReader.ReadBytes(cvVM.Contents.ContentLength);

                    repo.AddCv(cv);

                    return RedirectToAction("Index");
                }

                
                return View(cvVM);
                //if (cv.ContentLength > 0)
                //{
                //    string _FileName = Path.GetFileName(cv.FileName);
                //    string _path = Path.Combine(Server.MapPath("~/UploadedFiles/CV"), _FileName);
                //    cv.SaveAs(_path);
                //}
                //ViewBag.Message = "CV Uploaded Successfully!";
                //return View();
            }
            catch
            {
                ViewBag.Message = "CV upload failed!";
                return View(cvVM);
            }
        }
        

        // GET: CVs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cv = repo.GetCvById(id.Value);
            if (cv == null)
            {
                return HttpNotFound();
            }
            return View(cv);
        }

        //// GET: CVs/Create
        //public ActionResult Create()
        //{
        //    //ViewBag.UploaderID = new SelectList(repo.Users, "Id", "Email");
        //    return View();
        //}

        //// POST: CVs/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Title,Description,MimeType,Content")] CV cV)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repo.AddCv(cV);
        //        //repo.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    //ViewBag.UploaderID = new SelectList(repo.Users, "Id", "Email", cV.Uploader.Id);
        //    return View(cV);
        //}

        // GET: CVs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = repo.GetCvById(id.Value);
            if (cV == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UploaderID = new SelectList(repo.Users, "Id", "Email", cV.Uploader.Id);
            return View(cV);
        }

        // POST: CVs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Content")] CVVM cvVM)
        {
            //if (ModelState.IsValid)
            //{
            //    repo.ChangeCv(cV);

            //    return RedirectToAction("Index");
            //}
            if (ModelState.IsValid)
            {
                CV cv = new CV()
                {
                    Title = cvVM.Title,
                    Description = cvVM.Description,
                    MimeType = cvVM.Contents.ContentType,
                    Uploader = repo.GetUserByName(User.Identity.Name),
                    Content = null
                };

                BinaryReader binaryReader = new BinaryReader(cvVM.Contents.InputStream);
                cv.Content = binaryReader.ReadBytes(cvVM.Contents.ContentLength);

                repo.ChangeCv(cv);

                return RedirectToAction("Index");
            }

            return View(cvVM);
        }

        // GET: CVs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = repo.GetCvById(id.Value);
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
            repo.DeleteCv(id);

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        repo.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
