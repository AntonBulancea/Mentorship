using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models.Tables
{
    public class Hometasks
    {
        public int Id { get; set; }
        public string HometaskTitle { get; set; }
        public string HometaskText { get; set; }
        public string Sender { get; set; }
        public string Lesson { get; set; }
        public byte[] File { get; set; }
    }
}
