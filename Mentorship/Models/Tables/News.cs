using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Tables
{
    public class News
    {
        public int Id { get; set; }
        public string NewsTitle { get; set; }
        public string NewsText { get; set; }
        public string Sender { get; set; }
        public string LessonTitle { get; set; }
        public string Time { get; set; }
    }
}
