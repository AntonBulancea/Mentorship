using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string LessonTitle { get; set; }
        public string LessonId { get; set; }
        public string LeadTeacherEmail { get; set; }
        public string LessonTheme { get; set; }
        public string Pupils { get; set; }
        public byte[] LessonPhoto { get; set; }
    }
}
