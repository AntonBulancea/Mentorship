using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Context;
using Mentorship.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Controllers
{
    public class ClearDB : Controller
    {
        public IActionResult Index()
        {
            
            using (CoursesContext context = new CoursesContext())
            {
                foreach (var u in context.courses)
                {
                    context.courses.Remove(u);
                }
                context.SaveChanges();
            }
            using (NewsContext context = new NewsContext())
            {
                foreach (var u in context.news)
                {
                    context.news.Remove(u);
                }
                context.SaveChanges();
            }
            using (AttachedFilesContext context = new AttachedFilesContext())
            {
                foreach (var u in context.files)
                {
                    context.files.Remove(u);
                }
                context.SaveChanges();
            }
            using (HometaskContext context = new HometaskContext())
            {
                foreach (var u in context.ht)
                {
                    context.ht.Remove(u);
                }
                context.SaveChanges();
            }
            using (HtFilesContext context = new HtFilesContext())
            {
                foreach (var u in context.htFiles)
                {
                    context.htFiles.Remove(u);
                }
                context.SaveChanges();
            }
            using (AccountContext context = new AccountContext())
            {
                foreach (var u in context.accounts)
                {
                    context.accounts.Remove(u);
                }
                context.SaveChanges();
            }
            using (SectionLessonContext context = new SectionLessonContext())
            {
                foreach (var u in context.sLessons)
                {
                    context.sLessons.Remove(u);
                }
                context.SaveChanges();
            }
            using (SectionsContext context = new SectionsContext())
            {
                foreach (var u in context.sections)
                {
                    context.sections.Remove(u);
                }
                context.SaveChanges();
            }
            return Content("0");

        }
    }
}