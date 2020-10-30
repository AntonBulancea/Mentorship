using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Tables
{
    public class SectionLesson
    {
        public int Id { get; set; }
        public string lessonTitle { get; set; }
        public string sectionTitle { get; set; }
        public string videoLink { get; set; }
        public string description { get; set; }
    }
}
