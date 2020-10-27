using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Tables
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Teach { get; set; }
        public string Learn { get; set; }

        public byte[] Photo { get; set; }
    }
}
