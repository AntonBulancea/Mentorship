using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Context;
using Mentorship.Models.SecondaryFunctions;
using Mentorship.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Controllers
{
    public class ManageClassroom : Controller
    {
        public IActionResult CoursesManage()
        {
            List<Courses> cs = new List<Courses>();
            List<Courses> csl = new List<Courses>();

            string email;

            using (StreamReader reader = new StreamReader("wwwroot/Account.txt"))
            {
                email = reader.ReadToEnd().Split('&')[2];
            }

            using (CoursesContext courses = new CoursesContext())
            {
                var db = courses.courses;
                foreach (var u in db)
                {
                    if (u.PupilsEmails.Split(' ')[0].Equals(email))
                    {
                        cs.Add(u);
                    }
                    if (u.PupilsEmails.Split(' ').Contains(email) &&
                       ! u.PupilsEmails.Split(' ')[0].Equals(email))
                    {
                        csl.Add(u);
                    }
                }
            }

            ManageModel md = new ManageModel() { courses = cs, Learn_courses = csl };
            return View("CoursesManage", md);
        }
    }
}
