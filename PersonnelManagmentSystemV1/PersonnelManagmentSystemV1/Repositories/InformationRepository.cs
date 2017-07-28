using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.Models;
using PersonnelManagmentSystemV1.DataAccess;
using System.Data.Entity;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class InformationRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Information> Informations(bool publicOnly = false)
        {
            if (publicOnly)
            {
                return db.Information.Where(i => i.IsPublic);
            }
            return db.Information;
        }

        public Information Information(int id)
        {
            return db.Information.SingleOrDefault(d => d.ID == id);
        }

        public void AddInformation(Information info)
        {
            db.Information.Add(info);
            db.SaveChanges();
        }

        public void EditInformation(Information info)
        {
            db.Entry(info).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteInformation(int id)
        {
            Information information = Information(id);
            db.Information.Remove(information);
            db.SaveChanges();
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        // This code added to correctly implement the disposable pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}