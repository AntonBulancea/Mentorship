using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Context
{
    public class HometaskContext : DbContext
    {
        public HometaskContext() : base("DBConnection") { }
        public DbSet<HomeworkTheme> ht { get; set; }
    }
}
