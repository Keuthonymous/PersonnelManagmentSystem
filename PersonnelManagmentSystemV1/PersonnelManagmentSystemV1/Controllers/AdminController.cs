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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace PersonnelManagmentSystemV1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private AdminRepository db = new AdminRepository();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            List<ApplicationUser> users = db.GetAllUsers().ToList();
            List<AdminIndexUserViewModel> usersIndex = new List<AdminIndexUserViewModel>();
            List<string> roles = db.GetRoles().ToList();
            foreach (var i in users)
            {
                usersIndex.Add(new AdminIndexUserViewModel { Email = i.Email, UserName = i.UserName });
            }

            return View(db.GetAllUsers().ToList());
        }

        public ActionResult Departments()
        {
            return View(db.Departments());
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.FindUser(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: Admin/DepartmentDetails/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.FindDepartment(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            IEnumerable<string> roleName = db.GetRoles();
            ViewBag.userRole = new SelectList(roleName);
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel registerUser, string userRole)
        {
            IEnumerable<string> roleName = db.GetRoles();
            ViewBag.userRole = new SelectList(roleName);

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = registerUser.Email, Email = registerUser.Email };
                var result = UserManager.Create(user, registerUser.Password);

                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, userRole);
                    return RedirectToAction("Index");
                }
            }
            return View(registerUser);
        }

        // GET: Admin/CreateDepartment
        public ActionResult CreateDepartment()
        {
            return View();
        }

        // POST: Admin/CreateDepartment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDepartment([Bind(Include = "Name,Manager")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.AddDepartment(department);
                return RedirectToAction("Departments");
            }

            return View(department);
        }

        // GET: Admin/EditUser/5
        public ActionResult EditUser(string id)
        {
            IEnumerable<string> roleName = db.GetRoles();
            ViewBag.userRole = new SelectList(roleName);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.FindUser(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            EditUserViewModel editUser = new EditUserViewModel { ID = applicationUser.Id, Email = null, Password = null };
            return View(editUser);
        }

        //POST: Admin/EditUser/5
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(EditUserViewModel editUser, string userRole)
        {
            IEnumerable<string> roleName = db.GetRoles();
            ViewBag.userRole = new SelectList(roleName);
            if (ModelState.IsValid)
            {
                ApplicationUser user = db.FindUser(editUser.ID);
                
                db.EditUser(user, editUser);
                if (userRole != null)
                {
                    string currentRole = user.Roles.ToString();
                    db.RemoveUserFromRole(user, currentRole);
                    db.AddUserToRole(user, userRole);
                }
                return RedirectToAction("Index");
            }
            return View(editUser);
        }

        // GET: Admin/EditDepartment/5
        public ActionResult EditDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.FindDepartment(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST : Admin/EditDepartment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment([Bind(Include = "Name,Manager")]Department department)
        {
            if (ModelState.IsValid)
            {
                db.EditDepartment(department);
                return RedirectToAction("Departments");
            }
            return View(department);
        }

        // GET: Admin/DeleteUser/5
        public ActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.FindUser(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Admin/DeleteUser/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(string id)
        {
            ApplicationUser applicationUser = db.FindUser(id);
            db.RemoveUser(applicationUser);
            return RedirectToAction("Index");
        }

        // GET: Admin/DeleteDepartment/5
        public ActionResult DeleteDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.FindDepartment(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Admin/DeleteDepartment/5
        [HttpPost, ActionName("DeleteDepartment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDepartmentConfirmed(int? id)
        {
            Department department = db.FindDepartment(id);
            db.RemoveDepartment(department);
            return RedirectToAction("Departments");
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
