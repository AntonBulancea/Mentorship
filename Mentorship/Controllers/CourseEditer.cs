using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Context;
using Mentorship.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Controllers
{
    public class CourseEditer : Controller
    {
        public IActionResult ChangeDesctiption(string ls, string new_description)
        {
            string lessonTitle = ls.Substring("Change description for ".Length);

            Courses c = new Courses();

            using (CoursesContext courses = new CoursesContext())
            {
                var db = courses.courses;
                foreach (var u in db)
                {
                    if (u.LessonTitle.Equals(lessonTitle))
                    {
                        c = u;
                    }
                }
            }

            using (CoursesContext context = new CoursesContext())
            {
                context.courses.Attach(c);
                c.LessonDescription = new_description;
                context.SaveChanges();
            }

            return Content("Succes");
        }
        public IActionResult ChangePhoto()
        {
            return View();
        }
    }
}
