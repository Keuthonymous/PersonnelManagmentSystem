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
    [Authorize(Roles="Searcher")]
    public class SearcherController : Controller
    {
        private JobsRepository db = new JobsRepository();

        // GET: Searcher
        public ActionResult Index()
        {
            return View(db.Jobs().ToList());
        }

        // GET: Searcher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobOpening jobOpening = db.Job(id.Value);
            if (jobOpening == null)
            {
                return HttpNotFound();
            }
            return View(jobOpening);
        }
    }
}
