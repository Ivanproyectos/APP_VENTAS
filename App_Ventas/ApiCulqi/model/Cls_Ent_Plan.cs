using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCulqi.model
{
   public class Cls_Ent_Plan
    {
        public int MONTO { get; set; }
        public string CODIGO_MONEDA { get; set; }
        public string INTERVALO { get; set; }
        public int INTERVALO_COUNT { get; set; }
        public int LIMIT { get; set; }
        public string NOMBRE_PLAN { get; set; }
        public int DIAS_PRUEBA { get; set; }
    }
}
