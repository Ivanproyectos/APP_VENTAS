using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Administracion
{
    public class Cls_Ent_Usuario : Base.Cls_Ent_Base
    {
        public int ID_USUARIO { get; set; }
        public string COD_USUARIO { get; set; }
        public string CLAVE_USUARIO { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string NOMBRES_COMPLETOS { get; set; }
        public string DNI { get; set; }
        public string TELEFONO { get; set; }
        public string CELULAR { get; set; }
        public string CORREO { get; set; }
        public string DESC_CARGO { get; set; }
        public string DESC_OFICINA { get; set; }
        public int ID_OFICINA { get; set; }
        public int ID_CARGO { get; set; }
        public int FLG_JEFE { get; set; }
        public string ACCION { get; set; }
        public int ID_USUARIO_CARGO { get; set; }
        public int ID_ENTIDAD { get; set; }
        public string DESC_ENTIDAD { get; set; }
        public string VERDOCUMENTO { get; set; }
        public int FLG_DOCUMENTO { get; set; }

        public string APE_PATERNO { get; set; }
        public string APE_MATERNO { get; set; }
        public int ID_SEDE { get; set; }
        public string REGIMEN { get; set; }
        public string DESC_SEDE { get; set; }
        public int ID_PROFESIONAL { get; set; }
        public string DESC_PROFESIONAL { get; set; }
        public string SIGLA_USUARIO { get; set; }
        public int ID_DET_PROFESIONAL { get; set; }

        public List<Cls_Ent_Usuario> ListaDetalle { get; set; }
    }
}
