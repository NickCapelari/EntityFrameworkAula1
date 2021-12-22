using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAulaEntity.Models
{
    public class Email
    {
        public int id { get; set; }
        public string email { get; set; }
        public Pessoa pessoa { get; set; }
                
    }
}
