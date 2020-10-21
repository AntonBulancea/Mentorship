using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Context;
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

            return Content("0");

        }
    }
}