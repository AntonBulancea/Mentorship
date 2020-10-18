using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models.Contexts
{
    public class AtContext : DbContext
    {
        public AtContext() : base("DBConnection") { }
        public DbSet<LessonAttachedFiles> lessonAttacheds { get; set; }
    }
}
