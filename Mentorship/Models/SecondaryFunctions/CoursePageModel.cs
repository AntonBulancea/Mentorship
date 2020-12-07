using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Models.SecondaryFunctions
{
    public class CoursePageModel
    {
        public string lessonTitle { get; set; }
        public string description { get; set; }
        public string PercipantsEmails { get; set; }

        public bool isCreator { get; set; }
        public bool isSub { get; set; }

        public List<News> news { get; set; }
        public List<AttachedFiles> at { get; set; }
    }
}
