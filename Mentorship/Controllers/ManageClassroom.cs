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

            string email = HttpContext.Request.Cookies["email"];
            using (CoursesContext course = new CoursesContext())
            {
                var db = course.courses;
                foreach (var u in db)
                {
                    if (u.PupilsEmails.Split(' ')[0].Equals(email))
                    {
                        cs.Add(u);
                    }
                    if (u.PupilsEmails.Split(' ').Contains(email) &&
                       !u.PupilsEmails.Split(' ')[0].Equals(email))
                    {
                        csl.Add(u);
                    }
                }
            }

            List<string> courses = new List<string>();

            using (CoursesContext coursesContext = new CoursesContext())
            {
                foreach (var u in coursesContext.courses) {
                    if (HttpContext.Request.Cookies.ContainsKey("email"))
                    {
                        if (u.PupilsEmails.Contains(HttpContext.Request.Cookies["email"]))
                        {
                            courses.Add(u.LessonTitle);
                        }
                    }
                }
            }

            Dictionary<string, string> hometaskTimes = new Dictionary<string, string>();

            using (HometaskContext context = new HometaskContext())
            {
                foreach (var u in context.ht)
                {
                    if (courses.Contains(u.Hometask.Substring(4)))
                    {
                        string isLate = " (You are late)";
                        string a = u.Date.Split('*')[1].Split('-')[1];
                        string b = u.Date.Split('*')[1].Split('-')[0];

                        string c = DateTime.Now.ToString("yyyy/MM/dd");
                        string d = DateTime.Now.ToString("HH:mm");

                        if (Int64.Parse(a.Split('/')[0]) <= Int64.Parse(c.Split('/')[0]) &&
                           Int64.Parse(a.Split('/')[1]) <= Int64.Parse(c.Split('/')[1]) &&
                           Int64.Parse(a.Split('/')[2]) > Int64.Parse(c.Split('/')[2]))
                        {
                            isLate = "";
                        }

                        if (Int64.Parse(a.Split('/')[0]) <= Int64.Parse(c.Split('/')[0]) &&
                          Int64.Parse(a.Split('/')[1]) <= Int64.Parse(c.Split('/')[1]) &&
                          Int64.Parse(a.Split('/')[2]) == Int64.Parse(c.Split('/')[2]))
                        {
                            if (Int64.Parse(b.Split(':')[0]) <= Int64.Parse(d.Split(':')[0]) &&
                              Int64.Parse(b.Split(':')[1]) <= Int64.Parse(d.Split(':')[1]))
                            {
                                isLate = "";
                            }
                        }

                        if (hometaskTimes.ContainsKey(u.Hometask.Substring(4)))
                        {
                            hometaskTimes[u.Hometask.Substring(4)] += "|" + a + " - " + b + isLate;
                        }
                        else
                        {
                            hometaskTimes.Add(u.Hometask.Substring(4), a + " - " + b + isLate);
                        }
                    }
                }
            }

            ManageModel md = new ManageModel() { courses = cs, Learn_courses = csl,times = hometaskTimes };
            return View("CoursesManage", md);
        }
    }
}
