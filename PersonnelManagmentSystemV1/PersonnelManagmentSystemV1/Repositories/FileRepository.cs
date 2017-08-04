using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
using System.Data.Entity;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class FileRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<File> GetAllFiles()
        {
            return context.Files.Include(f => f.Department);
        }

        public File GetFileById(int id)
        {
            return GetAllFiles().SingleOrDefault(f => f.ID == id);
        }

        public void AddFile(File file)
        {
            context.Files.Add(file);
            context.SaveChanges();
        }

        public void ChangeFile(File file)
        {
            context.Entry(file).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteFile(int id)
        {
            context.Files.Remove(GetFileById(id));
            context.SaveChanges();
        }
    }
}