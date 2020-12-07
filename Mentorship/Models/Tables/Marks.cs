using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Tables
{
    public class Marks
    {
        public int Id { get; set; }

        public string HometaskId { get; set; }
        public string Mark { get; set; }
        public string Comment { get; set; }
    }
}
