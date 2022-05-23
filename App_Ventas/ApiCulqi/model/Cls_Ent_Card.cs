using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCulqi.model
{
    public class Cls_Ent_Card 
    {
        public int card_number { get; set; }
        public int cvv { get; set; }
        public int expiration_month { get; set; }
        public int expiration_year { get; set; }
        public string email { get; set; } 
    }
   
}
