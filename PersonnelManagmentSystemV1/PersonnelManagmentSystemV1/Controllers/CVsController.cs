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


        private CVVM MapCVToCVVM(CV cv)
        {
            if (cv == null)
                return null;

            return new CVVM()
            {
                ID = cv.ID,
                Title = cv.Title,
                Description = cv.Description,
                UploadTime = cv.UploadTime,
                FileName = cv.FileName,
                AllowEdit = cv.Uploader.UserName == User.Identity.Name
            };
        }

        // GET: CVs
        public ActionResult Index()
        {
            return View(repo.GetCVsForUser(User.Identity.GetUserId()).OrderByDescending(c => c.UploadTime).Select(cv => MapCVToCVVM(cv)));
        }


        [Authorize(Roles = "Searcher")]
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Contents")] CVVM cvVM)
            {
                if (ModelState.IsValid)
                {
                    CV cv = new CV()
                    {
                        Title = cvVM.Title,
                        Description = cvVM.Description,
                        MimeType = cvVM.Contents.ContentType,
                        Uploader = repo.GetUserByName(User.Identity.Name),
                        FileName = cvVM.Contents.FileName,
                        Content = null
                    };
          
                    BinaryReader binaryReader = new BinaryReader(cvVM.Contents.InputStream);
                    cv.Content = binaryReader.ReadBytes(cvVM.Contents.ContentLength);

                    repo.AddCv(cv);

                    return RedirectToAction("Index");
                }

                return View(cvVM);
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

        // GET: CVs/Create
        [Authorize(Roles = "Searcher")]
        public ActionResult Create()
        {
            return View();
        }

        // GET: CVs/Edit/5
        [Authorize(Roles = "Searcher")]
        public ActionResult Edit(int? id)
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
            return View(MapCVToCVVM(cv));
        }

        // POST: CVs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Searcher")]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Contents")] CVVM cvVM)
        {

            if (ModelState.IsValid)
            {
                CV cv = repo.GetCvById(cvVM.ID);
                if (cv == null)
                {
                    return HttpNotFound();
                }

                cv.Title = cvVM.Title;
                cv.Description = cvVM.Description;
                if (cvVM.Contents != null)
                {
                    cv.FileName = cvVM.Contents.FileName;
                    cv.MimeType = cvVM.Contents.ContentType;
                    BinaryReader binaryReader = new BinaryReader(cvVM.Contents.InputStream);
                    cv.Content = binaryReader.ReadBytes(cvVM.Contents.ContentLength);
                }

                repo.ChangeCv(cv);

                return RedirectToAction("Index");
            
            }

            return View(cvVM);
        }

        // GET: CVs/Delete/5
        [Authorize(Roles = "Searcher")]
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
            if (cV.Uploader.UserName != User.Identity.Name)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(cV);
        }

        // POST: CVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CV cV = repo.GetCvById(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            if (cV.Uploader.UserName != User.Identity.Name)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            repo.DeleteCv(id);

            return RedirectToAction("Index");
        }
    }
}
