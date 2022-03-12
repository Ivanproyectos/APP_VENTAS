using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Login
{
    public class Cls_Ent_Modulo
    {

        public long ID_MODULO { get; set; }
        public long ID_MODULO_PADRE { get; set; }
        public string ID_A { get; set; }
        public string ID_LI { get; set; }
        public string IMAGEN { get; set; }
        public string URL_MODULO { get; set; }
        public string DESC_MODULO { get; set; }
        public int NIVEL { get; set; }
        public int FLG_DEFAULT { get; set; }
        public int FLG_LINK { get; set; }
        public int FLG_SINGRUPO { get; set; }
        
        public List<Cls_Ent_Modulo> Modulos_Hijos { get; set; } 
    }
}
