using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Base;

namespace Capa_Entidad.Inventario
{
    public class Cls_Ent_Producto : Base.Cls_Ent_Base
    {
        public long ID_PRODUCTO { get; set; }
        public string COD_PRODUCTO { get; set; }
        public string DESC_PRODUCTO { get; set; }
        public int ID_SUCURSAL { get; set; }
        public int ID_UNIDAD_MEDIDA { get; set; }
        public int ID_CATEGORIA { get; set; }

        public decimal PRECIO_COMPRA { get; set; }
        public decimal PRECIO_VENTA { get; set; }
        public int STOCK { get; set; }
        public int STOCK_MINIMO { get; set; }
        public int FLG_SERVICIO { get; set; }
        public int FLG_VENCE { get; set; }
        public string FECHA_VENCIMIENTO { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string DETALLE { get; set; }

        public string DESC_UNIDAD_MEDIDA { get; set; }
        public string DESC_CATEGORIA { get; set; }

        public Cls_Ent_Archivo MiArchivo { get; set; }
        public string COD_UNIDAD_MEDIDA { get; set; }
        public int FILA { get; set; }
        
      
        
    }
}
