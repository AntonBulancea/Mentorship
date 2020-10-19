using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Tables
{
    public class Courses
    {
        public int Id { get; set; }
        public string LessonTitle { get; set; }
        public string LessonTags { get; set; }
        public string LessonDescription { get; set; }
        public string PupilsEmails { get; set; }
        public byte[] LessonPhoto { get; set; }
    }
}
