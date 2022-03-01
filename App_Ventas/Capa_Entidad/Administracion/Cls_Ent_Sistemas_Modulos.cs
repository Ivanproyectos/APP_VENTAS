using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Administracion
{
    public class Cls_Ent_Sistemas_Modulos : Base.Cls_Ent_Base
    {

        public int ID_SISTEMA_MODULO { get; set; }
        public string ID_PERFIL { get; set; }
        public long ID_USUARIO { get; set; }
        public int ID_TIPO_MODULO { get; set; }
        public string DESC_TIPO_MODULO { get; set; }
        public int ID_SISTEMA_MODULO_PADRE { get; set; }
        public string ID_A { get; set; }
        public string ID_LI { get; set; }
        public string IMAGEN { get; set; }
        public string URL_MODULO { get; set; }
        public string DESC_MODULO { get; set; }
        public int ORDEN { get; set; }
        public int NIVEL { get; set; }
        public List<Cls_Ent_Sistemas_Modulos> Modulos_Hijos { get; set; } 

    }
}
