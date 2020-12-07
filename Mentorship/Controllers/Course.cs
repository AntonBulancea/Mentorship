using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Context;
using Mentorship.Models.SecondaryFunctions;
using Mentorship.Models.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Controllers
{
    public class Course : Controller
    {
        public IActionResult CoursePage(string lessonTitle)
        {
            string email = HttpContext.Request.Cookies["email"];

            using (CoursesContext context = new CoursesContext())
            {
                var db = context.courses;
                foreach (var u in db)
                {
                    if (u.LessonTitle.Equals(lessonTitle))
                    {
                        if (u.PupilsEmails.Split(' ')[u.PupilsEmails.Split(' ').Length - 1].Equals(":private"))
                        {
                            if (!u.PupilsEmails.Split(' ').Contains(email))
                            {
                                return View("WrongEmail");
                            }
                        }
                        else
                        {
                            if (!u.PupilsEmails.Split(' ').Contains(email))
                            {
                                return View("CoursePage", SetModel(lessonTitle, true));
                            }
                            else
                            {
                                return View("CoursePage", SetModel(lessonTitle, false));
                            }
                        }
                    }
                }
            }
            return View("CoursePage", SetModel(lessonTitle, false));
        }
        [HttpPost]
        public IActionResult AddNews(IFormFileCollection file, string lessonTitle, string NewsText, string NewsTitle)
        {
            lessonTitle = lessonTitle.Substring(12);

            foreach (var u in file)
            {
                using (AttachedFilesContext context = new AttachedFilesContext())
                {
                    byte[] buffer;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        u.CopyTo(ms);
                        buffer = ms.ToArray();
                    }

                    AttachedFiles at = new AttachedFiles() { AttachedFile = buffer, LessonTitle = lessonTitle, FileName = u.FileName, NewsTitle = NewsTitle };
                    context.files.Add(at);
                    context.SaveChanges();
                }
            }
            using (NewsContext context = new NewsContext())
            {
                News news = new News() { LessonTitle = lessonTitle, NewsText = NewsText, NewsTitle = NewsTitle };
                context.news.Add(news);
                context.SaveChanges();
            }

            return View("CoursePage", SetModel(lessonTitle));
        }
        public IActionResult DownloadFile(string LessonTitle, string FileName, string NewsTitle)
        {
            FileName = FileName.Substring("                   ".Length);
            using (AttachedFilesContext context = new AttachedFilesContext())
            {
                var db = context.files;
                foreach (var u in db)
                {
                    if (u.FileName.Equals(FileName)
                        && u.NewsTitle.Equals(NewsTitle) && u.LessonTitle.Equals(LessonTitle))
                    {
                        using (FileStream stream = new FileStream(@"wwwroot\download\" + FileName, FileMode.Create))
                        {
                            stream.Write(u.AttachedFile, 0, u.AttachedFile.Length);
                        }
                    }
                }
            }

            return View("CoursePage", SetModel(LessonTitle));
        }
        [HttpPost]
        public IActionResult Subscribe(string lesson)
        {
            string lessonTitle = lesson.Substring("Subscribe on ".Length);
            Courses courses = new Courses();

            using (CoursesContext context = new CoursesContext())
            {
                var db = context.courses;
                foreach (var u in db)
                {
                    if (u.LessonTitle.Equals(lessonTitle))
                    {
                        courses = u;
                    }
                }
            }
            using (CoursesContext context = new CoursesContext())
            {

                context.courses.Attach(courses);
                string emails = courses.PupilsEmails;
                courses.PupilsEmails = emails.Insert(emails.Length - 2, " " +
                    HttpContext.Request.Cookies["email"]);
                context.SaveChanges();

            }

            return View("CoursePage", SetModel(lessonTitle));
        }
        [HttpPost]
        public IActionResult UnSubscribe(string lesson)
        {
            string lessonTitle = lesson.Substring("UnSubscribe from ".Length);
            Courses courses = new Courses();

            using (CoursesContext context = new CoursesContext())
            {
                var db = context.courses;
                foreach (var u in db)
                {
                    if (u.LessonTitle.Equals(lessonTitle))
                    {
                        courses = u;
                    }
                }
            }
            using (CoursesContext context = new CoursesContext())
            {
                context.courses.Attach(courses);
                string emails = courses.PupilsEmails;
                List<string> a = emails.Split(' ').ToList();
                a.Remove(HttpContext.Request.Cookies["email"]);
                string emailString = "";
                foreach (var u in a) {
                    emailString += u + " ";
                }
                courses.PupilsEmails = emailString;

                context.SaveChanges();

            }

            return View("CoursePage", SetModel(lessonTitle));
        }

        public CoursePageModel SetModel(string lessonTitle, bool isSub = false)
        {
            string email = HttpContext.Request.Cookies["email"];
            string desc = "No description";
            string emails = "";

            bool isCreator = false;

            using (CoursesContext context = new CoursesContext())
            {
                foreach (var u in context.courses)
                {
                    if (u.LessonTitle.Equals(lessonTitle))
                    {
                        desc = u.LessonDescription;
                        emails = u.PupilsEmails;
                    }
                    if (HttpContext.Request.Cookies["email"] == u.PupilsEmails.Split(' ')[0])
                    {
                        isCreator = true;
                    }
                }
            }
            using (CoursesContext context = new CoursesContext())
            {
                foreach (var u in context.courses)
                {
                    if (u.LessonTitle.Equals(lessonTitle))
                    {
                        desc = u.LessonDescription;
                    }
                }
            }
            List<News> news_list = new List<News>();
            List<AttachedFiles> at_list = new List<AttachedFiles>();
            using (NewsContext context = new NewsContext())
            {
                var db = context.news;
                foreach (var u in db)
                {
                    news_list.Add(u);
                }
            }
            using (AttachedFilesContext context = new AttachedFilesContext())
            {
                var db = context.files;
                foreach (var u in db)
                {
                    at_list.Add(u);
                }
            }
            using (CoursesContext context = new CoursesContext())
            {
                var db = context.courses;
                foreach (var u in db)
                {
                    if (u.LessonTitle.Equals(lessonTitle) && !isSub)
                    {
                        if (!u.PupilsEmails.Split(' ').Contains(email))
                        {
                            isSub = true;
                        }
                    }
                }
            }

            CoursePageModel model = new CoursePageModel()
            {
                isSub = isSub,
                isCreator = isCreator,
                lessonTitle = lessonTitle,
                description = desc,
                news = news_list,
                at = at_list,
                PercipantsEmails = emails
            };
            return model;
        }
    }
}