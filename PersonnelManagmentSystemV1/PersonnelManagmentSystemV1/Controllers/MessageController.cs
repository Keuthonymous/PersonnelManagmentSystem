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
    [Authorize]
    public class MessageController : Controller
    {
        private MessageRepository repo = new MessageRepository();

        private MessageViewModel MapMessageToMessageVM(Message message)
        {
            return new MessageViewModel()
            {
                ID = message.ID,
                Title = message.Title,
                BodyContent = message.BodyContent,
                JobOpeningID = message.JobOpening.ID,
                SenderID = message.Sender.Id,
                RecipientID = message.Recipient.Id,
                SendTime = message.SendTime,
                FirstMessageinThreadID = message.FirstMessageInThreadID,
                RecipientName = message.Recipient.UserName,
                JobOpeningName = message.JobOpening.Title,
                SenderName = message.Sender.UserName
            };
        }

        // GET: Message
        public ActionResult Index()
        {
            return View(repo.GetMessagesForUser(User.Identity.Name).OrderByDescending(m => m.SendTime));
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
            message.Sender = repo.GetUserByName(User.Identity.Name);
            message.JobOpening = repo.GetJobopeningByID(messageViewModel.JobOpeningID);
            message.Recipient = message.JobOpening.Department.Manager;
            if (ModelState.IsValid)
            {
                repo.SendMessage(message); //Method sets the date / time sent
                return RedirectToAction("Index");
            }

            return View(messageViewModel);
        }

        // GET: Message/Reply: Replying to message
        public ActionResult Reply(int Id)
        {
            Message previousMessage = repo.GetMessageById(Id);
            MessageViewModel messageViewModel = new MessageViewModel() { 
                PreviousMessageID = Id, 
                FirstMessageinThreadID = previousMessage.FirstMessageInThreadID,
                Title = "Re:" + previousMessage.Title, 
                JobOpeningID = previousMessage.JobOpening.ID,
                RecipientID = previousMessage.Sender.Id,
                RecipientName = previousMessage.Sender.UserName,
                JobOpeningName = previousMessage.JobOpening.Title
            };
            messageViewModel.MessagesInThread = new List<MessageViewModel>() { MapMessageToMessageVM(previousMessage) };


            return View(messageViewModel);
        }

        // POST: Message/Reply: Replying to message
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply([Bind(Include = "ID,Title,BodyContent,PreviousMessageID")] MessageViewModel messageViewModel)
        {
            Message previousMessage = repo.GetMessageById(messageViewModel.PreviousMessageID);
            Message message = new Message() 
                { Title = messageViewModel.Title, 
                    BodyContent = messageViewModel.BodyContent, 
                    Sender = repo.GetUserByName(User.Identity.Name),
                    JobOpening = previousMessage.JobOpening,
                    Recipient = previousMessage.Sender,
                    FirstMessageInThreadID = previousMessage.FirstMessageInThreadID
                };
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
