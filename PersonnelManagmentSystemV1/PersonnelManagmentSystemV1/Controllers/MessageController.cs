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
    [Authorize]
    public class MessageController : Controller
    {
        private MessageRepository repo = new MessageRepository();

        // GET: Message
        public ActionResult Index()
        {
            return View(repo.GetMessagesForUser(User.Identity.Name));
        }

        // GET: Message/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = repo.GetMessageById(id.Value);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Message/Create: Reacting to a job opening
        public ActionResult Create(int jobOpeningID)
        {
            MessageViewModel messageViewModel = new MessageViewModel() { JobOpeningID = jobOpeningID };
            JobOpening job = repo.GetJobopeningByID(jobOpeningID);
            messageViewModel.Title = job.Title;

            return View(messageViewModel);
        }

        // POST: Message/Create: reacting to a job opening
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,BodyContent,JobOpeningID")] MessageViewModel messageViewModel)
        {
            Message message = new Message() { Title = messageViewModel.Title, BodyContent = messageViewModel.BodyContent };
            message.Sender = repo.GetUserByname(User.Identity.Name);
            message.JobOpening = repo.GetJobopeningByID(messageViewModel.JobOpeningID);
            message.Recipient = message.JobOpening.Department.Manager;
            if (ModelState.IsValid)
            {
                repo.SendMessage(message); //Method sets the date / time sent
                return RedirectToAction("Index");
            }

            return View(messageViewModel);
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = repo.GetMessageById(id.Value);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteMessage(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
