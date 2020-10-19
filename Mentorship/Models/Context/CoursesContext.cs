using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Context
{
    public class CoursesContext : DbContext
    {
        public CoursesContext() : base("DBConnection") { }
        public DbSet<Courses> courses { get; set; }
    }
}
