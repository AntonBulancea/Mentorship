using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Context;
using Mentorship.Models.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult LessonAdded(IFormFile file,string title,string desc,string tags)
        {
            string path = "wwwroot/courses/" + title + ".png";
            using (FileStream stream = new FileStream(path,FileMode.OpenOrCreate))
            {
                file.CopyTo(stream);
            }

            using (CoursesContext context = new CoursesContext()) {
                Courses course = new Courses() {
                    LessonTitle = title,
                    LessonDescription = desc,
                    LessonPhoto = null,
                    LessonTags = tags
                };
                context.courses.Add(course);
                context.SaveChanges();
            }
                return View("Index");
        }
    }
}
