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
            AdminIndexListViewModel indexList = new AdminIndexListViewModel();
            indexList.IndexUsers = db.GetIndexList().ToList();
            indexList.IndexDepartments = db.Departments().ToList();

            return View(indexList);
        }

        // GET: Admin/DetailsUser/5
        public ActionResult DetailsUser(string id)
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

        // GET: Admin/AddUserDepartment/5
        public ActionResult AddUserDepartment(string id)
        {
            AddUserToDepartmentViewModel addUser = new AddUserToDepartmentViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.FindUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            addUser.UserToAdd = user;
            addUser.DepartmentList = db.Departments().ToList();
            addUser.UserToAddID = user.Id;
            return View(addUser);
        }

        // POST: Admin/AddUserDepartment/5
        [HttpPost]
        public ActionResult AddUserDepartment(AddUserToDepartmentViewModel addUser, string userID)
        {
            addUser.DepartmentList = db.Departments().ToList();
            addUser.UserToAdd = db.FindUser(addUser.UserToAddID);
            Department department = db.FindDepartment(addUser.SelectedDepartment);

            if (ModelState.IsValid)
            {
                db.AddUserToDepartment(department, addUser.UserToAdd);
                return RedirectToAction("Index");
            }
            return View(addUser);
        }

        // GET: Admin/DepartmentUserAdd/5
        public ActionResult DepartmentUserAdd(int? id)
        {
            AddDepartmentToUserViewModel addDepartment = new AddDepartmentToUserViewModel();
            addDepartment.UsersList = db.GetAllUsers().ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.FindDepartment(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            addDepartment.SelectDepartment = department;
            return View(addDepartment);
        }

        // POST: Admin/DepartmentUserAdd/5
        [HttpPost]
        public ActionResult DepartmentUserAdd(AddDepartmentToUserViewModel addDepartment, string[] SelectedUsers)
        {
            addDepartment.UsersList = db.GetAllUsers().ToList();
            addDepartment.UsersToAdd = new List<ApplicationUser>();
            addDepartment.SelectDepartment = db.FindDepartment(addDepartment.SelectDepartment.ID);

            foreach (var i in SelectedUsers)
            {
                addDepartment.UsersToAdd.Add(db.FindUser(i));
            }
            if (ModelState.IsValid)
            {
                foreach (var i in addDepartment.UsersToAdd)
                {
                    db.AddUserToDepartment(addDepartment.SelectDepartment, i);
                }
            }
            return View(addDepartment);
        }

        // GET: Admin/DepartmentDetails/5
        public ActionResult DetailsDepartment(int? id)
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

        // GET: Admin/CreateUser
        public ActionResult CreateUser()
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
        public ActionResult CreateUser(RegisterViewModel registerUser, string userRole)
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
            return View("CreateUser", registerUser);
        }

        // GET: Admin/CreateDepartment
        public ActionResult CreateDepartment()
        {
            List<ApplicationUser> users = db.GetAllUsers().ToList();
            CreateDepartmentViewModel createDepartment = new CreateDepartmentViewModel { Users = users };
            createDepartment.UserNames = db.GetAllUserNames().ToList();
            return View(createDepartment);
        }

        // POST: Admin/CreateDepartment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDepartment(CreateDepartmentViewModel createDepartment)
        {
            List<ApplicationUser> users = db.GetAllUsers().ToList();
            createDepartment.Users = users;
            createDepartment.UserNames = db.GetAllUserNames().ToList();

            createDepartment.Manager = db.GetAllUsers().Where(u => u.UserName == createDepartment.ManagerUserName).First();
            if (ModelState.IsValid)
            {
                Department department = new Department { Manager = createDepartment.Manager, Name = createDepartment.Name };
                db.AddDepartment(department);

                return RedirectToAction("Index");
            }

            return View(createDepartment);
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
            EditDepartmentViewModel editDepartment = new EditDepartmentViewModel();
            editDepartment.Users = db.GetAllUsers();
            editDepartment.UserNames = db.GetAllUserNames();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.FindDepartment(id);
            editDepartment.Manager = department.Manager;
            editDepartment.CurrentDepartmentID = department.ID;
            editDepartment.Name = department.Name;
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(editDepartment);
        }

        // POST : Admin/EditDepartment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(EditDepartmentViewModel editDepartment)
        {
            List<ApplicationUser> users = db.GetAllUsers().ToList();
            editDepartment.Users = users;
            editDepartment.UserNames = db.GetAllUserNames().ToList();
            Department department = db.FindDepartment(editDepartment.CurrentDepartmentID);

            if (ModelState.IsValid)
            {
                department.Manager = db.GetAllUsers().Where(u => u.UserName == editDepartment.ManagerUserName).First();
                department.Name = editDepartment.Name;

                db.EditDepartment(department);
                return RedirectToAction("Index");
            }
            return View(editDepartment);
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
            return RedirectToAction("Index");
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
