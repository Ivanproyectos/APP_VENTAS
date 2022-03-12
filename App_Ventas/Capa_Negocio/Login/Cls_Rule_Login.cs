using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Login;
using Capa_Datos.Login;

namespace Capa_Negocio.Login
{
    public class Cls_Rule_Login
    {
        private Cls_Dat_Login OData = new Cls_Dat_Login();

        public List<Cls_Ent_Modulo> Usuario_Sistema_Modulo(long ID_PERFIL, ref Cls_Ent_Auditoria auditoria)
        {
            List<Cls_Ent_Modulo> lista = new List<Cls_Ent_Modulo>();
            try
            {
                lista = OData.Usuario_Sistema_Modulo(ID_PERFIL, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Modulo>();
            }
            return lista;
        }


    }
}
