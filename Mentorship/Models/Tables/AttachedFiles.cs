using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Tables
{
    public class AttachedFiles
    {
        public int Id { get; set; }
        public string LessonTitle { get; set; }
        public string NewsTitle { get; set; }
        public byte[] AttachedFile { get; set; }
        public string FileName { get; set; }
    }
}
