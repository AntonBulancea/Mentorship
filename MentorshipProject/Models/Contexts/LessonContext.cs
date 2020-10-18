using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models
{
    public class LessonContext : DbContext
    {
        public LessonContext() : base("DBConnection") { }
        public DbSet<Lesson> lessons { get; set; }
    }
}
