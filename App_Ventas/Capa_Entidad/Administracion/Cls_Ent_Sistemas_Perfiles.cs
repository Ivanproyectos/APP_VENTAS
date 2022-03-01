using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Administracion
{
   public class Cls_Ent_Sistemas_Perfiles : Base.Cls_Ent_Base
    {
        public int ID_SISTEMA_PERFIL { get; set; }
        public int ID_SISTEMA_MODULO { get; set; }
        public string COD_PERFIL { get; set; }
        public string DESC_PERFIL { get; set; }

    }
}
