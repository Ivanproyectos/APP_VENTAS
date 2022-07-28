using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Inventario
{
    public class Cls_Ent_Movimiento_Producto : Base.Cls_Ent_Base
    {
        public int ID_MOVIMIENTO { get; set; }
        public long ID_PRODUCTO { get; set; }
        public int CANTIDAD { get; set; }
        public string DETALLE { get; set; }
        public int FLG_MOVIMIENTO { get; set; }
        public string MOVIMIENTO { get; set; }

        public string COD_USUARIO { get; set; }
        public string FECHA_INICIO { get; set; } 
        public string FECHA_FIN { get; set; }
        public string DESC_PRODUCTO { get; set; }
        public int ID_UNIDAD_MEDIDA { get; set; }
        public string COD_UNIDAD_MEDIDA { get; set; }
        public int ID_SUCURSAL { get; set; }
        
        
    }
}
