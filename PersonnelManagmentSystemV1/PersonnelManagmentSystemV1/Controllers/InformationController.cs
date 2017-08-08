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
using PersonnelManagmentSystemV1.ViewModels;

namespace PersonnelManagmentSystemV1.Controllers
{
    [Authorize(Roles="Boss, Worker")]
    public class InformationController : Controller
    {
        private InformationRepository db = new InformationRepository();


        // GET: Information
        public ActionResult Index()
        {
            List<InformationViewModel> informationToShow = new List<InformationViewModel>();
            InformationViewModel infoToAdd;
            
            foreach (Information info in db.InformationsForUsersManagedDepartments(User.Identity.Name))
            { //info for users managed departments (that he does manage)
                infoToAdd = InformationViewModel.MapInformationToInformationViewModel(info);
                infoToAdd.IsEditable = true;
                informationToShow.Add(infoToAdd);
            }
            foreach (Information info in db.InformationsForUser(User.Identity.Name))
            { //info for users own department (that he does not manage)
                if (!informationToShow.Any(i => i.ID == info.ID))
                {
                    infoToAdd = InformationViewModel.MapInformationToInformationViewModel(info);
                    infoToAdd.IsEditable = false;
                    informationToShow.Add(infoToAdd);
                }
            }
            return View(informationToShow);
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
            Information info = db.Information(id.Value);
            InformationViewModel informationViewModel = InformationViewModel.MapInformationToInformationViewModel(info);
            if (informationViewModel == null)
            {
                return HttpNotFound();
            }
            informationViewModel.IsEditable = db.GetManagedDepartmentsByUserName(User.Identity.Name).Contains(info.Department);
            return View(informationViewModel);
        }

        [Authorize(Roles="Boss")]
        // GET: Information/Create
        public ActionResult Create()
        {
            ViewBag.Departments = db.GetManagedDepartmentsByUserName(User.Identity.Name).Select(d => new SelectListItem() { Text = d.Name, Value = d.ID.ToString() });
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
            ViewBag.Departments = db.GetManagedDepartmentsByUserName(User.Identity.Name).Select(d => new SelectListItem() { Text = d.Name, Value = d.ID.ToString() });
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationViewModel informationViewModel = InformationViewModel.MapInformationToInformationViewModel(db.Information(id.Value));
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
        public ActionResult Edit([Bind(Include = "ID,Title,Content,IsPublic,DepartmentID")] InformationViewModel informationViewModel)
        {
            if (ModelState.IsValid)
            {
                Information information = db.Information(informationViewModel.ID);
                information.Title = informationViewModel.Title;
                information.Contents = informationViewModel.Content;
                information.IsPublic = informationViewModel.IsPublic;
                information.Department = db.Department(informationViewModel.DepartmentID);
                db.EditInformation(information);
                return RedirectToAction("Index");
            }
            return View(informationViewModel);
        }

        // GET: Information/Delete/5
        [Authorize(Roles = "Boss")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationViewModel informationViewModel = InformationViewModel.MapInformationToInformationViewModel(db.Information(id.Value));
            if (informationViewModel == null)
            {
                return HttpNotFound();
            }
            return View(informationViewModel);
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
    }
}
