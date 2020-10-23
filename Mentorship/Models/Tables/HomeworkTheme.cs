using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Tables
{
    public class HomeworkTheme
    {
        public int Id { get; set; }
        public string Hometask { get; set; }
        public string Theme { get; set; }
        public string NewsLink { get; set; }
        public string Date { get; set; }
        public string Pupils { get; set; }
    }
}
