using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Dashboard
{
    public class Cls_Ent_Dashboard : Base.Cls_Ent_Base
    {
        public int ID_SUCURSAL { get; set; }
        public decimal MONTO_TOTAL_VENTAS { get; set; }
        public int TOTAL_VENTAS { get; set; }
        public int TOTAL_COMPRAS { get; set; }
        public int TOTAL_DEVOLUCIONES { get; set; }
        public string MES { get; set; }
        public int NUMERO_MES { get; set; }
        public decimal TOTAL { get; set; }
        public int ANIO { get; set; }
        public string TIPO { get; set; }
        public int PORCENTAJE { get; set; }
        public int CANTIDAD { get; set; }
        public string PRODUCTO { get; set; }

        public List<Cls_Ent_Dashboard> Lista_VentaMes { get; set; }
        public List<Cls_Ent_Dashboard> Lista_Comparativa { get; set; }
        public List<Cls_Ent_Dashboard> Lista_TipoPago { get; set; }
        public List<Cls_Ent_Dashboard> Lista_ProductosMV { get; set; }
    }
}
