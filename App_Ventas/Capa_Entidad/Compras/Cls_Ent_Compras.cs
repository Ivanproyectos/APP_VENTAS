using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Administracion; 
namespace Capa_Entidad.Compras
{
    public class Cls_Ent_Compras : Base.Cls_Ent_Base
    {
        public int ID_COMPRA { get; set; }
        public string COD_COMPROBANTE { get; set; }
        public string FECHA_COMPROBANTE { get; set; }
        public int ID_TIPO_PAGO { get; set; }
        public int ID_SUCURSAL { get; set; }
        public string ID_TIPO_COMPROBANTE { get; set; }
        public int ID_PROVEEDOR { get; set; }
        public decimal SUB_TOTAL { get; set; } 
        public decimal DESCUENTO { get; set; }  
        public decimal IGV { get; set; } 
        public decimal TOTAL { get; set; }
        public string DETALLE { get; set; }
        public Cls_Ent_Proveedor Proveedor { get; set; }
        public int FILA { get; set; }
        public int FLG_ANULADO { get; set; }
        public string DESC_TIPO_COMPROBANTE { get; set; }
        public string NRO_OPERACION { get; set; }
        public string DESC_TIPO_PAGO { get; set; }
        public string DESC_ESTADO_COMPRA { get; set; }

        public List<Cls_Ent_Compras_Detalle> ListaDetalle { get; set; }
        
    }
}
