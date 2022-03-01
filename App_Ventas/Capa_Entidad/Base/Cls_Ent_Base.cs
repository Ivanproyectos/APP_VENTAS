using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Base
{
    public class Cls_Ent_Base
    {
        public long ID_SISTEMA { get; set; }
        public int FLG_ESTADO { get; set; }
        public string USU_CREACION { get; set; }
        public string FEC_CREACION { get; set; }
        public string STR_FEC_CREACION { get; set; }
        public string IP_CREACION { get; set; }
        public string USU_MODIFICACION { get; set; }
        public string FEC_MODIFICACION { get; set; }
        public string STR_FEC_MODIFICACION { get; set; }
        public string IP_MODIFICACION { get; set; }

        public string USU_PROCESO { get; set; } 
        public string IP_PROCESO { get; set; }
    }
}
