using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Administracion
{
    public class Cls_Ent_Notificacion : Base.Cls_Ent_Base
    {
        public long ID_NOTIFICACION { get; set; }
        public string FECHA_REGISTRO { get; set; }
        public int ESTADO { get; set; }
        public string MENSAJE { get; set; }
        public string IMAGE { get; set; }
        public string COLOR { get; set; }
        public string HORA { get; set; }
        public string FECHA_INICIO { get; set; }
        public string FECHA_FIN { get; set; }
    }
}
