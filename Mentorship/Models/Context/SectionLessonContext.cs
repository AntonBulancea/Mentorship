using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Context
{
    public class SectionLessonContext : DbContext
    {
        public SectionLessonContext() : base("DBConnection") { }
        public DbSet<SectionLesson> sLessons { get; set; }
    }
}
