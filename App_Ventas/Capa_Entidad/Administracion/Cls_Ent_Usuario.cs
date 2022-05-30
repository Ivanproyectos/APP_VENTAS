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
        public string NOMBRE { get; set; }
        public string APE_PATERNO { get; set; }
        public string APE_MATERNO { get; set; }
        public string DNI { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string CELULAR { get; set; }
        public string TELEFONO { get; set; }
        public string CORREO { get; set; }
        public string COD_USUARIO { get; set; }
        public string CLAVE_USUARIO { get; set; }
        public int FLG_ADMIN { get; set; }
        public string NOMBRES_APE { get; set; }
        public string DESC_TIPO_DOCUMENTO { get; set; }

        public List<Cls_Ent_Usuario> ListaDetalle { get; set; }
        public List<Cls_Ent_Usuario_Perfil> Lista_Sucursales { get; set; }
        
    }
}
