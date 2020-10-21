using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Context
{
    public class AttachedFilesContext : DbContext
    {
        public AttachedFilesContext() : base("DBConnection") { }
        public DbSet<AttachedFiles> files { get; set; }
    }
}
