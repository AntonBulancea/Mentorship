using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Context
{
    public class NewsContext : DbContext
    {
        public NewsContext() : base("DBConnection") { }
        public DbSet<News> news { get; set; }
    }
}
