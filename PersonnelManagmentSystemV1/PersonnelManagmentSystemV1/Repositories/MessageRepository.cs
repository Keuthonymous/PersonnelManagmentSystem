using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class MessageRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private IEnumerable<Message> GetAllMessages()
        {
            return db.Messages
                .Include(m => m.JobOpening)
                .Include(m => m.JobOpening.Department)
                .Include(m => m.JobOpening.Department.Manager)
                .Include(m => m.Recipient)
                .Include(m => m.Sender);
        }

        public IEnumerable<Message> GetMessagesForUser(string userName)
        {
            return GetAllMessages().Where(m => m.Sender.UserName == userName || m.Recipient.UserName == userName);
        }

        public Message GetMessageById(int id)
        {
            return GetAllMessages().SingleOrDefault(m => m.ID == id);
        }

        public ApplicationUser GetUserByName(string userName) //!!!! USER !!!!
        {
            return db.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public void SendMessage(Message message)
        {
            message.SendTime = DateTime.Now;
            db.Messages.Add(message);
            db.SaveChanges();
        }

        public void DeleteMessage(int id)
        {
            Message message = GetMessageById(id);
            db.Messages.Remove(message);
            db.SaveChanges();
        }

        public JobOpening GetJobopeningByID(int jobOpeningId) //!!!! JOBOPENING !!!!!
        {
            return db.Jobs
                .Include(j => j.Department)
                .Include(j => j.Department.Manager)
                .SingleOrDefault(j => j.ID == jobOpeningId);
        }

        public ApplicationUser GetUserById(string userID) //!!!! USER !!!!
        {
            return db.Users.SingleOrDefault(u => u.Id == userID);
        }
    }
}