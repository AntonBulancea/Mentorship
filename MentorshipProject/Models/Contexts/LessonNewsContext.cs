using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models.Contexts
{
    public class LessonNewsContext : DbContext
    {
        public LessonNewsContext() : base("DBConnection") { }
        public DbSet<LessonNews> lessonNews { get; set; }
    }
}
