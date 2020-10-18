using MentorshipProject.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MentorshipProject.Models.Contexts
{
    public class HometaskContext : DbContext
    {
        public HometaskContext() : base("DBConnection") { }
        public DbSet<Hometasks> hometasks { get; set; }
    }
}
