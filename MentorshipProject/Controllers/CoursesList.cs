using MentorshipProject.Models;
using MentorshipProject.Models.Contexts;
using MentorshipProject.Models.Others;
using MentorshipProject.Models.Tables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace MentorshipProject.Controllers
{
    public class CoursesList : Controller
    {
        List<Lesson> lessons = new List<Lesson>();
        List<Tags> tags = new List<Tags>();
        SecondaryClassIndex secondary = new SecondaryClassIndex();
        UserInformation userInformation = new UserInformation();
        SecondaryClassCoursePage SecondaryClass = new SecondaryClassCoursePage();

        string Tag = "No template";

        IWebHostEnvironment hostingEnvironment;
        public CoursesList(IWebHostEnvironment hosting)
        {
            hostingEnvironment = hosting;

            using (LessonContext db = new LessonContext())
            {
                var lesson = db.lessons;
                foreach (var u in lesson)
                {
                    lessons.Add(u);
                }
            }

            using (TagsContext db = new TagsContext())
            {
                var lesson = db.tags;
                foreach (var u in lesson)
                {
                    tags.Add(u);
                }
            }
        }
        public async Task<IActionResult> Index()
        {
            secondary = new SecondaryClassIndex() { lessons = lessons, tags = tags, TagsRec = Tag };
            return View("Index", secondary);
        }
        public IActionResult AddNew()
        {
            return View("AddNew");
        }
        [HttpPost]
        public IActionResult Hometask(string lessonTitle)
        {
            List<Hometasks> htt = new List<Hometasks>();

            using (HometaskContext db = new HometaskContext())
            {
                var ht = db.hometasks;
                foreach (var u in ht)
                {
                    htt.Add(u);
                }
            }
            HometaskSecondary sec = new HometaskSecondary() { LessonTitle = lessonTitle, hometasks = htt };
            return View("Hometask", sec);
        }
        [HttpPost]
        public IActionResult HometaskDownload(IFormFileCollection files, string title, string text, string lesson)
        {
            byte[] buffer;
            foreach (var f in files)
            {
                using (HometaskContext context = new HometaskContext())
                {
                    var db = context.hometasks;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        f.CopyTo(ms);
                        buffer = ms.ToArray();
                    }
                    Hometasks hometasks = new Hometasks()
                    {
                        HometaskTitle = title + " - " + f.FileName,
                        HometaskText = text,
                        Lesson = lesson,
                        File = buffer,
                        Sender = "Anton"
                    };
                    db.Add(hometasks);
                    context.SaveChanges();
                }
            }

            List<Hometasks> htt = new List<Hometasks>();
            List<string> AllTitle = new List<string>();

            using (HometaskContext db = new HometaskContext())
            {
                var ht = db.hometasks;
                foreach (var u in ht)
                {
                    htt.Add(u);
                    if (!AllTitle.Contains(u.HometaskTitle)) {
                        AllTitle.Add(u.HometaskTitle);
                    }
                }
            }
            HometaskSecondary sec = new HometaskSecondary() { LessonTitle = title, hometasks = htt, hometaskTitle = AllTitle};
            return View("Hometask", sec);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewLessonClick(IFormFile file, string interests, string lessonComment, string LessonTitle)
        {
            string path = @"wwwroot\AddFile\" + LessonTitle + ".png";
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            WriteToFile(path, interests, lessonComment, LessonTitle);
            {

                string[] inter = interests.Split("#");
                List<Tags> listOfTegs = new List<Tags>();

                for (int i = 0; i < inter.Length; i++)
                {
                    using (TagsContext context = new TagsContext())
                    {
                        var db = context.tags;
                        bool hasValue = false;
                        foreach (var u in db)
                        {
                            if (u.TagTitle.Equals(inter[i]))
                            {
                                u.TagUsed += 1;
                                hasValue = true;
                                break;
                            }
                        }

                        if (!hasValue)
                        {
                            listOfTegs.Add(new Tags { TagTitle = inter[i], TagUsed = 0 });
                        }
                    }

                }
                using (TagsContext context = new TagsContext())
                {
                    for (int j = 0; j < listOfTegs.Count; j++)
                    {
                        context.tags.Add(listOfTegs[j]);
                    }
                    context.SaveChanges();
                }

            }
            List<Tags> tags = new List<Tags>();
            using (TagsContext context = new TagsContext())
            {
                var db = context.tags;
                foreach (var u in db)
                {
                    tags.Add(u);
                }
            }

            return View("AdminView", tags);
        }
        public async void WriteToFile(string path, string inter, string lesCom, string LessonTitle)
        {
            byte[] buffer = new byte[10000];
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                await fs.ReadAsync(buffer, 0, 10000);
            }

            using (LessonContext lesson = new LessonContext())
            {
                Lesson ls = new Lesson
                {
                    LessonPhoto = buffer,
                    LessonTitle = LessonTitle,
                    LessonTheme = inter,
                    LeadTeacherEmail = UserInformation.Email
                };

                lesson.lessons.Add(ls);
                lesson.SaveChanges();
            }
        }
        [HttpPost]
        public IActionResult TagRequest(string tagResult)
        {
            if (tagResult == null)
            {
                tagResult = "No template";
            }
            Tag = tagResult;

            string[] str = Tag.Split('#');
            string temp = "";
            foreach (string u in str)
            {
                temp += u;
            }

            secondary = new SecondaryClassIndex() { lessons = lessons, tags = tags, TagsRec = Tag };
            return View("Index", secondary);
            // return Content(tag);
        }
        [HttpPost]
        public IActionResult CoursePage(string AddNewCourse)
        {

            List<LessonAttachedFiles> atFiles = new List<LessonAttachedFiles>();
            List<LessonNews> news = new List<LessonNews>();

            using (AtContext db = new AtContext())
            {
                var lesson = db.lessonAttacheds;
                foreach (var u in lesson)
                {
                    atFiles.Add(u);
                }
            }

            using (LessonNewsContext db = new LessonNewsContext())
            {
                var lesson = db.lessonNews;
                foreach (var u in lesson)
                {
                    news.Add(u);
                }
            }
            List<UserInformation> user = new List<UserInformation>() { userInformation };
            List<string> lsTitle = new List<string>();
            //lsTitle.Add(AddNewCourse);
            lsTitle.Add(AddNewCourse.Split('-')[0].Substring(1, AddNewCourse.Split('-')[0].Length - 1));

            SecondaryClass = new SecondaryClassCoursePage
            {
                lessonAttachedFiles = atFiles,
                lessonNews = news,
                information = user,
                LessonTitle = lsTitle
            };

            return View("CoursePage", SecondaryClass);

            return Content(AddNewCourse);
        }
        [HttpPost]
        public IActionResult PostNewsClick(IFormFileCollection file, string header, string text, string lessonTitle)
        {
            int i = 0;
            using (LessonNewsContext context = new LessonNewsContext())
            {
                LessonNews news = new LessonNews() { Head = header, LessonTitle = lessonTitle, Sender = "Anton", Text = text };
                i = news.Id;
                context.lessonNews.Add(news);
                context.SaveChanges();
            }
            string lt = lessonTitle.Split('(')[1].Substring(0, lessonTitle.Split('(')[1].Length - 1);

            byte[] buffer;
            foreach (var u in file)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    u.CopyTo(ms);
                    buffer = ms.ToArray();
                }
                using (AtContext lessonAttached = new AtContext())
                {
                    var db = lessonAttached.lessonAttacheds;
                    db.Add(new LessonAttachedFiles()
                    {
                        LessonId = lt + "|" + header,
                        File = buffer,
                        FileName = u.FileName,
                        LessonName = lt
                    });
                    lessonAttached.SaveChanges();
                }
            }


            //return Content(i.ToString());
            secondary = new SecondaryClassIndex() { lessons = lessons, tags = tags, TagsRec = Tag };
            return View("Index", secondary);
            //return Content(lessonTitle + "|" + header + "|" + text);
        }
        [HttpPost]
        public IActionResult DeleteNews(string delete)
        {
            int id = Int32.Parse(delete.Split(' ')[3]);
            LessonNews lessonNews = null;
            LessonAttachedFiles attachedFiles = null;
            string title = "";

            using (LessonNewsContext context = new LessonNewsContext())
            {
                var db = context.lessonNews;
                foreach (var u in db)
                {
                    if (u.Id == id)
                    {
                        lessonNews = u;
                        title = u.LessonTitle;
                    }
                }
            }

            using (AtContext context = new AtContext())
            {
                var db = context.lessonAttacheds;
                foreach (var u in db)
                {
                    if (u.LessonId.Equals(title))
                    {
                        attachedFiles = u;
                    }
                }
            }

            using (LessonNewsContext context = new LessonNewsContext())
            {
                var db = context.lessonNews;

                db.Attach(lessonNews);
                db.Remove(lessonNews);
                context.SaveChanges();
            }

            using (AtContext context = new AtContext())
            {
                var db = context.lessonAttacheds;
                if (attachedFiles != null)
                {
                    db.Attach(attachedFiles);
                    db.Remove(attachedFiles);
                    context.SaveChanges();
                }
            }

            secondary = new SecondaryClassIndex() { lessons = lessons, tags = tags, TagsRec = Tag };
            return View("Index", secondary);
        }
        [HttpPost]
        public IActionResult DownloadFile(string name)
        {
            string path = name.Split('-')[1].Substring(1);
            string title = name.Split('-')[0].Substring(0, name.Split('-')[0].Length - 1);

            using (AtContext context = new AtContext())
            {
                var db = context.lessonAttacheds;
                foreach (var u in db)
                {
                    using (FileStream stream = new FileStream(@"wwwroot\" + title, FileMode.Create))
                    {
                        stream.Write(u.File, 0, u.File.Length);
                    }
                }
            }

            return Content("");
        }
    }
}
