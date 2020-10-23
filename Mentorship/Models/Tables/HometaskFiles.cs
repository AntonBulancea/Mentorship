using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentorship.Models.Tables
{
    public class HometaskFiles
    {
        public int Id { get; set; }
        public byte[] File { get; set; }
        public string HometaskId { get; set; }
        public string Sender { get; set; }
        public string Date { get; set; }
        public string HometaskType { get; set; }
    }
}
