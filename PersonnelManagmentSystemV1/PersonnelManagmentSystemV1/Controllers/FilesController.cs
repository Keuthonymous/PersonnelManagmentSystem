using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonnelManagmentSystemV1.Repositories;
using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.ViewModels;
using System.IO;

namespace PersonnelManagmentSystemV1.Controllers
{
    public class FilesController : Controller
    {
        private FileRepository repo = new FileRepository();

        // GET: Files
        public ActionResult Index()
        {
            return View(repo.GetAllFiles());
        }

        // GET: Files/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFile  file = repo.GetFileById(id.Value);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            ViewBag.Departments = repo.GetManagedDepartmentsByUserName(User.Identity.Name).Select(d => new SelectListItem() { Text = d.Name, Value = d.ID.ToString() });
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,DepartmentID,Description,Contents")] FileViewModel fileVM)
        {
            if (ModelState.IsValid)
            {
                UserFile file = new UserFile()
                {
                    Title = fileVM.Title,
                    Description = fileVM.Description,
                    MimeType = fileVM.Contents.ContentType,
                    Department = repo.Department(fileVM.DepartmentID),
                    Content = null
                };

		        BinaryReader binaryReader = new BinaryReader(fileVM.Contents.InputStream);
                file.Content = binaryReader.ReadBytes(fileVM.Contents.ContentLength);
                repo.AddFile(file);
                
                return RedirectToAction("Index");
            }

            ViewBag.Departments = repo.GetManagedDepartmentsByUserName(User.Identity.Name).Select(d => new SelectListItem() { Text = d.Name, Value = d.ID.ToString() });

            return View(fileVM);
        }

        // GET: Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFile file = repo.GetFileById(id.Value);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,MimeType,Content")] UserFile file)
        {
            if (ModelState.IsValid)
            {
                repo.ChangeFile(file);
                
                return RedirectToAction("Index");
            }
            return View(file);
        }

        // GET: Files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFile file = repo.GetFileById(id.Value);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteFile(id);
            
            return RedirectToAction("Index");
        }
    }
}
