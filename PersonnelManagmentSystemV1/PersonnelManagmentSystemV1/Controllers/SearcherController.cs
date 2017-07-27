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
    [Authorize(Roles="Searcher")]
    public class SearcherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Searcher
        public ActionResult Index()
        {
            return View(db.Jobs.ToList());
        }

        // GET: Searcher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOpening jobOpening = db.Jobs.Find(id);
            if (jobOpening == null)
            {
                return HttpNotFound();
            }
            return View(jobOpening);
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
