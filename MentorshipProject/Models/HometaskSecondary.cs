using MentorshipProject.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models
{
    public class HometaskSecondary
    {
        public string LessonTitle { get; set; }
        public List <Hometasks> hometasks { get; set; }
        public List<string> hometaskTitle { get; set; }
    }
}
