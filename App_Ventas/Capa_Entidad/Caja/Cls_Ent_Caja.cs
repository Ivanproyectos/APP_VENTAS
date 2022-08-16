using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Caja
{
   public class Cls_Ent_Caja :Base.Cls_Ent_Base
    {
       public int COUNT_VENTA { get; set; }
       public decimal TOTAL_VENTA { get; set; }
       public int COUNT_COBRAR { get; set; }
       public decimal TOTAL_COBRAR { get; set; }
       public int COUNT_ADELANTO { get; set; }
       public decimal TOTAL_ADELANTO { get; set; }
       public int COUNT_INGRESO { get; set; }
       public decimal TOTAL_INGRESO { get; set; }
       public int COUNT_EGRESO { get; set; }
       public decimal TOTAL_EGRESO { get; set; }

       public string FEC_INICIO { get; set; }
       public string FEC_FIN { get; set; }
       public string COD_USUARIO { get; set; }
       public int ID_SUCURSAL { get; set; }

       public int ID_TIPO_MOVIMIENTO { get; set; }
       public int FLG_TIPO { get; set; }
       public string DESC_MOVIMIENTO { get; set; }
       public decimal MONTO { get; set; }


        public int COUNT_COMPRAS { get; set; }
        public decimal TOTAL_COMPRAS { get; set; }
        public decimal EGRESOS_NETO { get; set; }
        public decimal INGRESOS_NETO { get; set; }
        public decimal TOTAL_NETO { get; set; }

    }
}
