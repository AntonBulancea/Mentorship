using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Context
{
    public class HtFilesContext : DbContext
    {
        public HtFilesContext() : base("DBConnection") { }
        public DbSet<HometaskFiles> htFiles { get; set; }
    }
}
