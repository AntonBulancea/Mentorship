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
    public class VideoCourses : Controller
    {
        public IActionResult CoursesVideo(string lessonTitle)
        {
            return View("CoursesVideo", SetModel(lessonTitle));
        }
        public IActionResult AddNewSection(string lessonTitle, string section)
        {
            lessonTitle = lessonTitle.Substring("Add section in ".Length);
            using (SectionsContext context = new SectionsContext())
            {
                Sections sections = new Sections() { LessonTitle = lessonTitle, SectionTitle = section };
                context.sections.Add(sections);
                context.SaveChanges();
            }
            return View("CoursesVideo", SetModel(lessonTitle));
        }
        public IActionResult ChangeLesson(string lessonTitle, string desc, string video)
        {
            lessonTitle = lessonTitle.Substring("Save changes in ".Length);

            string section = lessonTitle.Split(',')[1];
            lessonTitle = lessonTitle.Split(',')[0];

            using (SectionLessonContext context = new SectionLessonContext())
            {
                SectionLesson lesson = new SectionLesson()
                {
                    lessonTitle = lessonTitle,
                    sectionTitle = section,
                    description = desc,
                    videoLink = video.Split('/')[3].Substring(8)
                };
                context.sLessons.Add(lesson);
                context.SaveChanges();
            }
            return View("CoursesVideo", SetModel(lessonTitle));
        }
        public CoursesVideoModel SetModel(string lessonTitle)
        {
            List<Sections> sections = new List<Sections>();
            List<SectionLesson> lessons = new List<SectionLesson>();

            using (SectionsContext context = new SectionsContext())
            {
                var db = context.sections;
                foreach (var u in db)
                {
                    if (u.LessonTitle.Equals(lessonTitle))
                    {
                        sections.Add(u);
                    }
                }
            }

            using (SectionLessonContext context = new SectionLessonContext())
            {
                var db = context.sLessons;
                foreach (var u in db)
                {
                    if (u.lessonTitle.Equals(lessonTitle))
                    {
                        lessons.Add(u);
                    }
                }
            }

            CoursesVideoModel model = new CoursesVideoModel()
            {
                lessonTitle = lessonTitle,
                sections = sections,
                lessons = lessons
            };
            return model;
        }
    }
}