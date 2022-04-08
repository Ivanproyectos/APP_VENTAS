using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Compras
{
    public class Cls_Ent_Compras_Detalle : Base.Cls_Ent_Base
    {
        public int ID_COMPRA_DETALLE { get; set; }
        public int ID_COMPRA { get; set; }
        public int ID_PRODUCTO { get; set; }

        public decimal CANTIDAD { get; set; }
        public decimal PRECIO { get; set; }
        public decimal IMPORTE { get; set; }
        public string DESC_PRODUCTO { get; set; }
        public string COD_UNIDAD_MEDIDA { get; set; }
        public int ID_UNIDAD_MEDIDA { get; set; }

    }
}
