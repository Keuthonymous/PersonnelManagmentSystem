using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.Repositories;
using PersonnelManagmentSystemV1.ViewModels;

namespace PersonnelManagmentSystemV1.Controllers
{
    [Authorize(Roles = "Boss")]
    public class JobOpeningsController : Controller
    {
        private JobsRepository db = new JobsRepository();

        private JobOpeningViewModel MapJobOpening(JobOpening jobOpening)
        {
            if (jobOpening == null)
                return null;
            return new JobOpeningViewModel()
            {
                ID = jobOpening.ID,
                DepartmentID = jobOpening.Department.ID,
                DepartmentName = jobOpening.Department.Name,
                Title = jobOpening.Title,
                Description = jobOpening.Description,
                JobType = jobOpening.JobType,
                AllowEdit = jobOpening.Department.ManagerID == User.Identity.GetUserId()
            };
        }

        // GET: Jobs
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult _Index()
        {
            return PartialView(db.Jobs().Select(j => MapJobOpening(j)));
        }
        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOpening job = db.Job(id.Value);
            if (job == null)
            {
                return HttpNotFound();
            }
            JobOpeningViewModel jobVM = MapJobOpening(job);
            //Moved here in order to prevent multiple concurrent read actions
             jobVM.Applicants = job.GetAllApplicants().Select(a => new ApplicantViewModel() 
                    { 
                        Email = a.Email, 
                        DepartmentName = a.GetDepartmentName(), 
                        CVID = a.GetMostRecentCVID() 
                    });
             jobVM.Messages = job.Messages
                    .Where(m => m.IsFirstMessage)
                    .Select(m => new MessageViewModel() { SenderName = m.Sender.UserName, RecipientName = m.Recipient.UserName, SendTime = m.SendTime, BodyContent = m.BodyContent });
            return View(jobVM);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            //When creating, select only departments that user manages
            ViewBag.Departments = db.GetManagedDepartmentsByUserName(User.Identity.Name).Select(d => new SelectListItem() { Text = d.Name, Value = d.ID.ToString() });
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,JobType,DepartmentID")] JobOpeningViewModel jobVM)
        {
            if (ModelState.IsValid)
            {
                JobOpening job = new JobOpening() { Title = jobVM.Title, ID = jobVM.ID, Description = jobVM.Description, JobType = jobVM.JobType, Department = db.Department(jobVM.DepartmentID) };
                db.Add(job);
                return RedirectToAction("Index");
            }

            return View(jobVM);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOpening job = db.Job(id.Value);
            if (job == null)
            {
                return HttpNotFound();
            }
            //When editing, select only departments that user manages or the current department
            List<Department> departments = new List<Department>();
            departments.AddRange(db.GetManagedDepartmentsByUserName(User.Identity.Name));
            if (!departments.Contains(job.Department))
            {
                departments.Add(job.Department);
            }
            //Convert list of departents to a list of selectitem
            ViewBag.Departments = departments.Select(d => new SelectListItem() { Text = d.Name, Value = d.ID.ToString() });
            JobOpeningViewModel jobVM = MapJobOpening(job);

            return View(jobVM);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,JobType,DepartmentID")] JobOpeningViewModel jobVM)
        {
            if (ModelState.IsValid) //Refers to the model that has been bound by the modelbinder; in this case it is jobVM
            {
                JobOpening job = db.Job(jobVM.ID);
                job.JobType = jobVM.JobType;
                job.Title = jobVM.Title;
                job.Description = jobVM.Description;
                job.Department = db.Department(jobVM.DepartmentID);
                db.Edit(job);
                return RedirectToAction("Index");
            }
            return View(jobVM);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOpening job = db.Job(id.Value);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobOpening job = db.Job(id);
            db.Remove(job);
            return RedirectToAction("Index");
        }


        public ActionResult DownloadCV(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cv = db.GetCvById(id.Value);
            if (cv == null)
            {
                return HttpNotFound();
            }
            return File(cv.Content, cv.MimeType, cv.GetFileName());
        

        }
    }
}
