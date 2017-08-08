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
    [Authorize]
    public class FilesController : Controller
    {
        private FileRepository repo = new FileRepository();

        private FileViewModel MapFileToFileViewModel(UserFile file)
        {
            if (file == null)
                return null;

            return new FileViewModel()
            {
                ID = file.ID,
                Title = file.Title,
                Description = file.Description,
                DepartmentID = file.Department.ID,
                DepartmentName = file.Department.Name,
                AllowEdit = User.IsInRole("Boss")//repo.GetManagedDepartmentsByUserName(User.Identity.Name).Contains(file.Department)
            };

        }

        // GET: Files
        [Authorize(Roles = "Boss, Worker")]
        public ActionResult Index()
        {
            FileViewModel fileToAdd = null;
            List<FileViewModel> files = new List<FileViewModel>();
            foreach (UserFile file in repo.GetFilesForUsersManagedDepartments(User.Identity.Name))
            { //info for users managed departments (that he does manage)
                fileToAdd = MapFileToFileViewModel(file);
                fileToAdd.AllowEdit = true;
                files.Add(fileToAdd);
            }
            foreach (UserFile file in repo.GetFilesForUser(User.Identity.Name))
            { //info for users own department (that he does not manage)
                if (!files.Any(i => i.ID == file.ID))
                {
                    fileToAdd = MapFileToFileViewModel(file);
                    fileToAdd.AllowEdit = false;
                    files.Add(fileToAdd);
                }
            }
            return View(files);
        }

        // GET: Files/Details/5
        [Authorize(Roles = "Boss, Worker")]
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

            return View(MapFileToFileViewModel(file));
        }
        // GET download
        [Authorize(Roles = "Boss, Worker")]
        public ActionResult Download(int? id)
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
            return File(file.Content, file.MimeType, file.GetFileName());
        }

        // GET: Files/Create
        [Authorize(Roles = "Boss")]
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
        [Authorize(Roles = "Boss")]
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
                    OriginalFilename = fileVM.Contents.FileName,
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
        [Authorize(Roles = "Boss")]
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
            ViewBag.Departments = repo.GetManagedDepartmentsByUserName(User.Identity.Name).Select(d => new SelectListItem() { Text = d.Name, Value = d.ID.ToString() });
            return View(MapFileToFileViewModel(file));
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Boss")]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,DepartmentID")] FileViewModel fileViewModel)
        {
            if (ModelState.IsValid)
            {
                UserFile file = repo.GetFileById(fileViewModel.ID);
                file.Description = fileViewModel.Description;
                file.Title = fileViewModel.Title;
                file.Department = repo.Department(fileViewModel.DepartmentID);
                repo.ChangeFile(file);
                
                return RedirectToAction("Index");
            }
            ViewBag.Departments = repo.GetManagedDepartmentsByUserName(User.Identity.Name).Select(d => new SelectListItem() { Text = d.Name, Value = d.ID.ToString() });
            return View(fileViewModel);
        }

        // GET: Files/Delete/5
        [Authorize(Roles = "Boss")]
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
            return View(MapFileToFileViewModel(file));
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Boss")]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteFile(id);
            
            return RedirectToAction("Index");
        }
    }
}
