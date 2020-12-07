using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Context
{
    public class MarksContext : DbContext
    {
        public MarksContext() : base("DBConnection") { }
        public DbSet<Marks> accounts { get; set; }
    }
}
