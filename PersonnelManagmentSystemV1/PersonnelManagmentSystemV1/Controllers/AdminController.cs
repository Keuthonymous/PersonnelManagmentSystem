using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.ViewModels;
using PersonnelManagmentSystemV1.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PersonnelManagmentSystemV1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private AdminRepository db = new AdminRepository();

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private UserViewModel MapUserToUserViewModel(ApplicationUser user)
        {
            if (user == null)
                return null;
            UserViewModel result = MapUserToUserViewModel(user);

            if (user.Department != null)
            {
                result.DepartmentID = user.Department.ID;
                result.DepartmentName = user.Department.Name;
            }

            return result;
        }

        // GET: Admin
        public ActionResult Index()
        {
            AdminIndexListViewModel indexVM = new AdminIndexListViewModel();
            //Get a list of users first, so that the readaction on the dbcontext finishes
            List<ApplicationUser> users = db.GetAllUsers().ToList();
            //Now convert the list using a lookup function for the role name
            indexVM.IndexUsers = users.Select(user => 
                MapUserToUserViewModel(user));
            indexVM.IndexDepartments = db.Departments().ToList();

            return View(indexVM);
        }

        // GET: Admin/DetailsUser/5
        public ActionResult DetailsUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.GetUserByID(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            UserViewModel userVM = MapUserToUserViewModel(user);
            return View(userVM);
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
            Department department = db.GetDepartmentByID(id.Value);
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
            addDepartment.SelectDepartment = db.GetDepartmentByID(addDepartment.SelectDepartment.ID);
            if (SelectedUsers != null)
            { //only if at least one user was selected

                foreach (string userID in SelectedUsers)
                {
                    addDepartment.UsersToAdd.Add(db.GetUserByID(userID));
                }
                //if (ModelState.IsValid) 
                //ModelState.IsValid is determined by the modelbinder and on the model bound, in this case addDepartment and string[] SelectedUsers
                // In this case the check is meaningless and will always fail, because addDepartment.SelectDepartment is recreated from scratch in the modelbinder and 
                // does not have all properties needed at the end of modelbinding.
                {
                    foreach (ApplicationUser user in addDepartment.UsersToAdd)
                    {
                        db.AddUserToDepartment(addDepartment.SelectDepartment, user);
                    }
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
            Department department = db.GetDepartmentByID(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Admin/CreateUser
        public ActionResult CreateUser()
        {
            IEnumerable<string> roleName = db.GetRoleNames();
            ViewBag.userRole = new SelectList(roleName);
            UserViewModel newUser = new UserViewModel() { 
                Departments = db.Departments()
            };
            return View(newUser);
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UserViewModel registerUser, string userRole)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = registerUser.Email, Email = registerUser.Email, Department = db.GetDepartmentByID(registerUser.DepartmentID) };
                var result = UserManager.Create(user, registerUser.Password);

                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, userRole);
                    return RedirectToAction("Index");
                }
            }
            IEnumerable<string> roleName = db.GetRoleNames();
            ViewBag.userRole = new SelectList(roleName);
            registerUser.Departments = db.Departments();
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
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.GetUserByID(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            UserViewModel editUser = MapUserToUserViewModel(applicationUser);
            ViewBag.userRole = new SelectList(db.GetRoleNames());
            return View(editUser);
        }

        //POST: Admin/EditUser/5
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserViewModel editUser, string userRole)
        {
            IEnumerable<string> roleName = db.GetRoleNames();
            ViewBag.userRole = new SelectList(roleName);
            if (ModelState.IsValid)
            {
                ApplicationUser user = db.GetUserByID(editUser.ID);
                user.Email = editUser.Email;
                user.UserName = editUser.UserName;
                user.Department = db.GetDepartmentByID(editUser.DepartmentID);

                db.SaveUser(user);

                if (editUser.Password != null)
                {
                    UserManager.RemovePassword(user.Id);
                    UserManager.AddPassword(user.Id, editUser.Password);
                }

                if (userRole != null && userRole != "")
                {
                    string currentRole = db.GetPrimaryRoleName(user.Id);
                    if (currentRole != "")
                    {
                        db.RemoveUserFromRole(user, currentRole);
                    }
                    
                    db.AddUserToRole(user, userRole);
                }
                return RedirectToAction("Index");
            }
            editUser.Departments = db.Departments();
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
            Department department = db.GetDepartmentByID(id.Value);
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
            Department department = db.GetDepartmentByID(editDepartment.CurrentDepartmentID);

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
            ApplicationUser user = db.GetUserByID(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(MapUserToUserViewModel(user));
        }

        // POST: Admin/DeleteUser/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(string id)
        {
            db.RemoveUser(id);
            return RedirectToAction("Index");
        }

        // GET: Admin/DeleteDepartment/5
        public ActionResult DeleteDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.GetDepartmentByID(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Admin/DeleteDepartment/5
        [HttpPost, ActionName("DeleteDepartment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDepartmentConfirmed(int id)
        {
            db.RemoveDepartment(id);
            return RedirectToAction("Index");
        }
    }
}
