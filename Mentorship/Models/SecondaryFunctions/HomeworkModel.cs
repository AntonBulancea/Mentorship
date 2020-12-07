using Mentorship.Models.Context;
using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.SecondaryFunctions
{
    public class HomeworkModel
    {
        public string lessonTitle { get; set; }
        public string email { get; set; }
        public string date { get; set; }
        public string time { get; set; }


        public List<HometaskFiles> htFiles { get; set; }
        public List<HomeworkTheme> htTheme { get; set; }
        public List<Marks> marks { get; set; }

        public bool isCreator { get; set; }
    }
}
