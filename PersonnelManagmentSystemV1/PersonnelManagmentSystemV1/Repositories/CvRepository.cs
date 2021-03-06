﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonnelManagmentSystemV1.DataAccess;
using PersonnelManagmentSystemV1.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace PersonnelManagmentSystemV1.Repositories
{
    public class CvRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<CV> GetAllCVs()
        {
            return context.CVs.Include(c => c.Uploader);
        }

        public CV GetCvById(int id)
        {
            return GetAllCVs().SingleOrDefault(c => c.ID == id);
        }

        public void AddCv(CV cv)
        {
            cv.UploadTime = DateTime.Now;
            context.CVs.Add(cv);
            context.SaveChanges();
        }

        public void ChangeCv(CV cv)
        {
            context.Entry(cv).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteCv(int id)
        {
            context.CVs.Remove(GetCvById(id));
            context.SaveChanges();
        }

        public ApplicationUser GetUserByName(string userName) //!!!! USER !!!!
        {
            return context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public IEnumerable<CV> GetCVsForUser(string userId)
        {
            return context.CVs.Where(c => c.Uploader.Id == userId);
        }
    }
}