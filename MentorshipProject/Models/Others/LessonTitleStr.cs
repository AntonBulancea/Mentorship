using MentorshipProject.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models.Others
{
    public class LessonTitleStr
    {
        public string lessonTitle { get; set; }
        public List<Hometasks> hometasks { get; set; }
        public List<string> hometaskTitle { get; set; }
    }
}
