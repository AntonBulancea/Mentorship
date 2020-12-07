using Mentorship.Models.Context;
using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.SecondaryFunctions
{
    public class CoursesVideoModel
    {
        public List<Sections> sections { get; set; }
        public List<SectionLesson> lessons { get; set; }
        public bool isCreator { get; set; }

        public string lessonTitle { get; set; }
    }
}
