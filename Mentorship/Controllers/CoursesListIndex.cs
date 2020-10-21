using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Context;
using Mentorship.Models.SecondaryFunctions;
using Mentorship.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Controllers
{
    public class CoursesListIndex : Controller
    {
        public IActionResult Index()
        {
            List<Courses> courses = new List<Courses>();
            using (CoursesContext db = new CoursesContext())
            {
                var cr = db.courses;
                foreach (var u in cr)
                {
                    courses.Add(u);
                }
            }

            CourseListModel model = new CourseListModel() { Courses = courses, Tags = "No template" };

            return View(model);
        }
        [HttpPost]
        public IActionResult Tags(string temp)
        {
            List<Courses> courses = new List<Courses>();
            using (CoursesContext db = new CoursesContext())
            {
                var cr = db.courses;
                foreach (var u in cr)
                {
                    courses.Add(u);
                }
            }

            CourseListModel model = new CourseListModel() { Courses = courses, Tags = temp };

            return View("Index", model);
        }
    }
}
