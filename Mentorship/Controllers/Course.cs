using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.SecondaryFunctions;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Controllers
{
    public class Course : Controller
    {
        public IActionResult CoursePage(string lessonTitle)
        {
            CoursePageModel model = new CoursePageModel() {lessonTitle  = lessonTitle};
            return View("CoursePage", model);
        }
    }
}
