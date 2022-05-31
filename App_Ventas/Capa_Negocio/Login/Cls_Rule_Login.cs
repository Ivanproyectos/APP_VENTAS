using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Login;
using Capa_Entidad.Administracion; 
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

        public Cls_Ent_Usuario Login_Usuario(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Login_Usuario(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Usuario();
            }
        }

        public Cls_Ent_Usuario Usuario(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Usuario(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Usuario();
            }
        }

        public Cls_Ent_Usuario Usuario_Sistema(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Usuario_Sistema(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Usuario();
            }
        }
        

        
    }
}
