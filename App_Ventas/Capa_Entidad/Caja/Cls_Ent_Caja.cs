using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Caja
{
   public class Cls_Ent_Caja
    {
       public int COUNT_VENTA { get; set; }
       public decimal TOTAL_VENTA { get; set; }
       public int COUNT_COBRAR { get; set; }
       public decimal TOTAL_COBRAR { get; set; }
       public int COUNT_ADELANTO { get; set; }
       public decimal TOTAL_ADELANTO { get; set; }

       public string FEC_INICIO { get; set; }
       public string FEC_FIN { get; set; }
       public string COD_USUARIO { get; set; }
       public int ID_SUCURSAL { get; set; }

    }
}
