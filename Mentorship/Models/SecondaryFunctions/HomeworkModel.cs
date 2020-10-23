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

        public List<HometaskFiles> htFiles { get; set; }
        public List<HomeworkTheme> htTheme { get; set; }
    }
}
