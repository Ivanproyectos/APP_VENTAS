using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Base;

namespace Capa_Entidad.Administracion
{
    public class Cls_Ent_configurarEmpresa : Base.Cls_Ent_Base 
    {
        public int ID_CONFIGURACION { get; set; }
        public string RUC { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string NOMBRE_COMERCIAL { get; set; }
        public string URBANIZACION { get; set; }
        public string DIRECCION_FISCAL { get; set; }

        public string COD_UBIGEO { get; set; }
        public string TELEFONO { get; set; }
        public string NOMBRE_IMPUESTO { get; set; }
        public decimal IMPUESTO { get; set; }
        public int FLG_IMPRIMIR { get; set; }
        public int FLG_IMPUESTO { get; set; }
        public string SIMBOLO_MONEDA { get; set; }
        public string CORREO { get; set; }
        public string CODIGO_ARCHIVO_ISOTIPO { get; set; }
        public string NOMBRE_ARCHIVO_ISOTIPO { get; set; }
        public string EXTENSION_ARCHIVO_ISOTIPO { get; set; }
        public string CODIGO_ARCHIVO_LOGO { get; set; }
        public string NOMBRE_ARCHIVO_LOGO { get; set; }
        public string EXTENSION_ARCHIVO_LOGO { get; set; }

        public Cls_Ent_Archivo Archivo_Logo { get; set; }
        public Cls_Ent_Archivo Archivo_Isotipo { get; set; }
    }
}
