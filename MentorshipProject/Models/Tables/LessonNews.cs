using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models
{
    public class LessonNews
    {
        public int Id { get; set; }
        public string LessonId { get; set; }
        public string LessonTitle { get; set; }
        public string Head { get; set; }
        public string Text { get; set; }
       public string Sender { get; set; }
    }
}
