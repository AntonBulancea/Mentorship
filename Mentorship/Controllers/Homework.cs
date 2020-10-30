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
    public class Homework : Controller
    {
        public IActionResult Index(string lessonTitle)
        {
            return View("Index", setModel(lessonTitle));
        }
        [HttpPost]
        public IActionResult AddHometask(string theme, string newsLink, string lessonTitle, string emails)
        {
            lessonTitle = lessonTitle.Substring(12);
            using (HometaskContext context = new HometaskContext())
            {
                string date = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                context.ht.Add(new HomeworkTheme()
                {
                    Date = date,
                    Theme = theme,
                    NewsLink = newsLink,
                    Pupils = emails,
                    Hometask = lessonTitle,
                });
                context.SaveChanges();
            }
            string email;

            return View("Index", setModel(lessonTitle.Substring(3)));
        }
        [HttpPost]
        public IActionResult HometaskAdded(string id, IFormFileCollection files)
        {
            string lessonTitle = " ";
            id = id.Substring(10);
            using (HometaskContext context = new HometaskContext())
            {
                foreach (var u in context.ht)
                {
                    if (u.Id == Int32.Parse(id))
                    {
                        lessonTitle = u.Hometask.Substring(3);
                        //return Content(lessonTitle);
                    }

                }
            }

            foreach (var u in files)
            {
                using (HtFilesContext context = new HtFilesContext())
                {
                    byte[] buffer;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        u.CopyTo(ms);
                        buffer = ms.ToArray();
                    }

                    string email = HttpContext.Request.Cookies["email"];
                    string date = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                    HometaskFiles at = new HometaskFiles()
                    {
                        HometaskId = id + "|" + u.FileName,
                        Date = date,
                        File = buffer,
                        Sender = email
                    };
                    context.htFiles.Add(at);
                    context.SaveChanges();
                }
            }

            return View("Index", setModel(lessonTitle.Substring(3)));
        }
        public IActionResult DownloadHt(string id, string filename)
        {
            string lessonTitle = " ";

            using (HometaskContext context = new HometaskContext())
            {
                foreach (var u in context.ht)
                {
                    if (u.Id == Int32.Parse(id))
                    {
                        lessonTitle = u.Hometask.Substring(3);
                        //return Content(lessonTitle);
                    }

                }
            }
            using (HtFilesContext context = new HtFilesContext())
            {
                var db = context.htFiles;
                foreach (var u in db)
                {
                    if (u.HometaskId.Split('|')[1].Equals(filename))
                    {
                        using (FileStream stream = new FileStream(@"wwwroot\download\" + filename, FileMode.Create))
                        {
                            stream.Write(u.File, 0, u.File.Length);
                        }
                    }
                }
            }

            return View("Index", setModel(lessonTitle));
        }
        private HomeworkModel setModel(string lessonTitle)
        {
            string email = HttpContext.Request.Cookies["email"];

            List<HometaskFiles> files = new List<HometaskFiles>();
            List<HomeworkTheme> homeworks = new List<HomeworkTheme>();

            using (HometaskContext context = new HometaskContext())
            {
                foreach (var u in context.ht)
                {
                    homeworks.Add(u);
                }
            }
            using (HtFilesContext context = new HtFilesContext())
            {
                foreach (var u in context.htFiles)
                {
                    files.Add(u);
                }
            }

            HomeworkModel model = new HomeworkModel()
            {
                htFiles = files,
                htTheme = homeworks,
                lessonTitle = lessonTitle,
                email = email
            };
            return model;
        }
    }
}
