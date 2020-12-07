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
using MimeKit.Encodings;

namespace Mentorship.Controllers
{
    public class Homework : Controller
    {
        public IActionResult Index(string lessonTitle)
        {
            return View("Index", setModel(lessonTitle));
        }
        [HttpPost]
        public IActionResult AddHometask(string get_date, string get_time, string theme,
            string newsLink, string lessonTitle, string emails)
        {
            newsLink = "";

            lessonTitle = lessonTitle.Substring(12);
            using (HometaskContext context = new HometaskContext())
            {
                string get_tm = get_date.Split('-')[0] + "/" + get_date.Split('-')[1] + "/" + get_date.Split('-')[2];
                string date = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                context.ht.Add(new HomeworkTheme()
                {
                    Date = date + "*" + get_time + "-" + get_tm,
                    Theme = theme,
                    NewsLink = newsLink,
                    Pupils = emails,
                    Hometask = lessonTitle
                });
                context.SaveChanges();
            }
            return View("Index", setModel(lessonTitle.Substring(4)));
        }
        [HttpPost]
        public IActionResult HometaskAdded(string id, IFormFileCollection files)
        {
            string lessonTitle = " ";
            bool isUpToDate = false;

            string _date = "";
            string time = "";

            id = id.Substring(10);
            using (HometaskContext context = new HometaskContext())
            {
                foreach (var u in context.ht)
                {
                    if (u.Id == Int32.Parse(id))
                    {
                        lessonTitle = u.Hometask.Substring(3);

                        string a = u.Date.Split('*')[1].Split('-')[1];
                        string b = u.Date.Split('*')[1].Split('-')[0];

                        _date = a;
                        time = b;

                        string c = DateTime.Now.ToString("yyyy/MM/dd");
                        string d = DateTime.Now.ToString("HH:mm");

                        if (Int64.Parse(a.Split('/')[0]) <= Int64.Parse(c.Split('/')[0]) &&
                           Int64.Parse(a.Split('/')[1]) <= Int64.Parse(c.Split('/')[1]) &&
                           Int64.Parse(a.Split('/')[2]) > Int64.Parse(c.Split('/')[2]))
                        {
                            isUpToDate = true;
                        }

                        if (Int64.Parse(a.Split('/')[0]) <= Int64.Parse(c.Split('/')[0]) &&
                          Int64.Parse(a.Split('/')[1]) <= Int64.Parse(c.Split('/')[1]) &&
                          Int64.Parse(a.Split('/')[2]) == Int64.Parse(c.Split('/')[2]))
                        {
                            if (Int64.Parse(b.Split(':')[0]) <= Int64.Parse(d.Split(':')[0]) &&
                              Int64.Parse(b.Split(':')[1]) <= Int64.Parse(d.Split(':')[1]))
                            {
                                isUpToDate = true;
                            }
                        }

                    }
                }
            }
            if (isUpToDate)
            {
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
                return View("Index", setModel(lessonTitle.Substring(1)));
            }
            else
            {
                return View("LateHometask", new LateHtModel { date = _date + " - " + time });
            }
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
            return View("Index", setModel(lessonTitle.Substring(1)));
        }
        [HttpPost]
        public IActionResult AddMarks(string button,string lessonTitle, string comment, string mark, string id)
        {
            lessonTitle = button.Substring("Add Marks in ".Length).Split('-')[0];
            id = button.Substring("Add Marks in ".Length).Split('-')[1];

            using (MarksContext context = new MarksContext())
            {
                Marks marks = new Marks() { Comment = comment, Mark = mark, HometaskId = id };
                context.accounts.Add(marks);
                context.SaveChanges();
            }

            return View("Index", setModel(lessonTitle));
        }
        private HomeworkModel setModel(string lessonTitle)
        {
            string email = HttpContext.Request.Cookies["email"];

            bool isCreator = false;

            List<HometaskFiles> files = new List<HometaskFiles>();
            List<HomeworkTheme> homeworks = new List<HomeworkTheme>();
            List<Marks> marks = new List<Marks>();

            using (MarksContext context1 = new MarksContext())
            {
                foreach (var u in context1.accounts)
                {
                    marks.Add(u);
                }
            }
            using (HometaskContext context = new HometaskContext())
            {
                foreach (var u in context.ht)
                {
                    homeworks.Add(u);
                }
            }
            using (CoursesContext context = new CoursesContext())
            {
                foreach (var u in context.courses)
                {
                    if (u.PupilsEmails.Split(' ')[0].Equals(HttpContext.Request.Cookies["email"]))
                    {
                        isCreator = true;
                    }
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
                email = email,
                isCreator = isCreator,
                date = DateTime.Now.ToString("yyyy/MM/dd"),
                time = DateTime.Now.ToString("HH:mm")
            };
            return model;
        }
    }
}
