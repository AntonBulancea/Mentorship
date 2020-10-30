using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Context
{
    public class SectionsContext : DbContext
    {
        public SectionsContext() : base("DBConnection") { }
        public DbSet<Sections> sections { get; set; }
    }
}
