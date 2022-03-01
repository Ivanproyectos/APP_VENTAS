using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Administracion
{
  public   class Cls_Ent_sistemas_Usuarios :Base.Cls_Ent_Base
    {

        public int ID_SISTEMAS_PERFIL_USUARIO { get; set; }
        public int ID_USUARIO { get; set; }
        public int ID_PERFIL { get; set; }
        public int ID_OFICINA { get; set; }
        public int FLG_PRINCIPAL { get; set; }
        public string DESC_FLG_PRINCIPAL { get; set; }
      
        public string DESC_OFICINA { get; set; }
        public string DESC_PERFIL { get; set; }
        public string FEC_ACTIVACION { get; set; }
        public string FEC_DESACTIVACION { get; set; }
        public string COD_USUARIO { get; set; }
        public bool bool_FLG_ESTADO { get; set; }
        public string DESC_CARGO { get; set; }
        public int ID_CARGO { get; set; }
        public int ID_SEDE { get; set; }
        public string DESC_SEDE { get; set; }
        public int FLG_REACTIVACION { get; set; }
        public string FEC_REACTIVACION { get; set; }
        public string REACTIVACION { get; set; }
        public int ID_PROFESIONAL { get; set; }
      
    }
}
