using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Context;
using Mentorship.Models.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace Mentorship.Controllers
{
    public class AddNewCourse : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LessonAddedAsync(IFormFile file, string title, string desc, string tags, string emails)
        {
            string path = "wwwroot/courses/" + title + ".png";
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                file.CopyTo(stream);
            }

            string email = HttpContext.Request.Cookies["email"];

            using (CoursesContext context = new CoursesContext())
            {
                Courses course = new Courses()
                {
                    LessonTitle = title,
                    LessonDescription = desc,
                    LessonPhoto = null,
                    LessonTags = tags,
                    PupilsEmails = email + " " + emails
                };
                context.courses.Add(course);
                context.SaveChanges();
            }
            return View("Index");
        }
    }
}
