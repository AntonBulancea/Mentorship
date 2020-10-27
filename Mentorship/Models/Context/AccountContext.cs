using Mentorship.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Context
{
    public class AccountContext : DbContext
    {
        public AccountContext() : base("DBConnection") { }
        public DbSet<Account> accounts { get; set; }
    }
}
