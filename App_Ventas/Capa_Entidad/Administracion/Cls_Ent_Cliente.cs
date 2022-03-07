using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Administracion
{
    public class Cls_Ent_Cliente : Base.Cls_Ent_Base
    {
        public int ID_CLIENTE { get; set; }
        public string NOMBRES_APE { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public string DIRECCION { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }
        public string CELULAR { get; set; }
        public string COD_UBIGEO { get; set; }
        public string DETALLE { get; set; }
        public string DESC_UBIGEO { get; set; }
        public string DESC_TIPO_DOCUMENTO { get; set; }
    }
}
