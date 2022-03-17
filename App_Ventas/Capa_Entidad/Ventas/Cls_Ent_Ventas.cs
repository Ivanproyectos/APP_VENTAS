﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Ventas
{
    public class Cls_Ent_Ventas : Base.Cls_Ent_Base
    {
        public int ID_VENTA { get; set; }
        public int ID_SUCURSAL { get; set; }
        public string COD_COMPROBANTE { get; set; }
        public int FLG_TIPO_VENTA { get; set; }
        public string FECHA_VENTA { get; set; }
        public int ID_CLIENTE { get; set; }
        public string ID_TIPO_COMPROBANTE { get; set; }
        public decimal SUB_TOTAL { get; set; }
        public decimal IGV { get; set; }
        public decimal DESCUENTO { get; set; }
        public decimal TOTAL { get; set; }
        public decimal ADELANTO { get; set; }
        public string DETALLE { get; set; }

        public string CLIENTE { get; set; }
        public string DESC_TIPO_COMPROBANTE { get; set; }
        public List<Cls_Ent_Ventas_Detalle> ListaDetalle { get; set; }
        public int FILA { get; set; }
        public int FLG_ANULADO { get; set; }

        public string DESC_TIPO_VENTA { get; set; }
        public string DESC_ESTADO_CREDITO { get; set; }
        public string DESC_ESTADO_VENTA { get; set; }
        public int FLG_ESTADO_CREDITO { get; set; }
        public decimal DEBE { get; set; }
 
        
    }
}