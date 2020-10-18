using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models
{
    public class TagsContext : DbContext
    {
        public TagsContext() : base("DBConnection") { }
        public DbSet<Tags> tags { get; set; }
    }
}
