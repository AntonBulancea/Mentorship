using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models
{
    public class SecondaryClassIndex
    {
        public List<Lesson> lessons { get; set; }
        public List<Tags> tags { get; set; }
        public string TagsRec { get; set; }
    }
}
