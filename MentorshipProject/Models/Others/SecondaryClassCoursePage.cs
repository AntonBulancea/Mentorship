using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models
{
    public class SecondaryClassCoursePage
    {
        public List<LessonAttachedFiles> lessonAttachedFiles { get; set; }
        public List<LessonNews> lessonNews { get; set; }

        public List<UserInformation> information { get; set; }
        public List<string> LessonTitle { get; set; }
    }
}
