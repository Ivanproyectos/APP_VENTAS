using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Ventas
{
    public class Cls_Ent_Ventas_Detalle : Base.Cls_Ent_Base
    {
        public int ID_VENTA_DETALLE { get; set; }
        public int ID_PRODUCTO { get; set; }
        public int ID_VENTA { get; set; }
        public decimal PRECIO { get; set; }
        public int CANTIDAD { get; set; }
        public decimal IMPORTE { get; set; }
        public string DESC_PRODUCTO { get; set; }
        public int FLG_DEVUELTO { get; set; }
        public string COD_UNIDAD_MEDIDA { get; set; }
        public string MOTIVO { get; set; }
        
    }
}
