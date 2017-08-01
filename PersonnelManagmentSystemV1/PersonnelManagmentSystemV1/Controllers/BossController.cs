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
    [Authorize(Roles="Boss")]
    public class BossController : Controller
    {
        private DepartmentRepository db = new DepartmentRepository();

        // GET: Boss
        public ActionResult Index()
        {
             return View(db.Departments());
        }

        // GET: Boss/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
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
