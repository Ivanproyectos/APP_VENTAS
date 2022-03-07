using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Administracion
{
    public class Cls_Ent_Sucursal : Base.Cls_Ent_Base
    {
        public int ID_SUCURSAL { get; set; }
        public string DESC_SUCURSAL { get; set; }
        public string DIRECCION { get; set; }
        public int TELEFONO { get; set; }
        public string CORREO { get; set; }
        public string URBANIZACION { get; set; }
        public string COD_UBIGEO { get; set; }
        public string DESC_UBIGEO { get; set; }
        
    }
}
