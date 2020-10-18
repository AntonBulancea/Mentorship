using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models
{
    public class LessonAttachedFiles
    {
        public int Id { get; set; }
        public string LessonId { get; set; }
        public string LessonName { get; set; }
        public string Sender { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
    }
}
