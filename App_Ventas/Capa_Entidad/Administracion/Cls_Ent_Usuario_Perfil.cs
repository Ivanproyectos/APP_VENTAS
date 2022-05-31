using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Administracion
{
    public class Cls_Ent_Usuario_Perfil : Base.Cls_Ent_Base
    {
        public int ID_USUARIO_PERFIL { get; set; }
        public int ID_SUCURSAL { get; set; }
        public int ID_USUARIO { get; set; }
        public int ID_PERFIL { get; set; }
        public string FEC_ACTIVACION { get; set; }
        public string FEC_DESACTIVACION { get; set; }
        public string DESC_SUCURSAL { get; set; }
        public string DESC_PERFIL { get; set; }

        public string ID_USUARIO_PERFIL_HASH { get; set; }

    }
}
