using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Datos.Administracion;

namespace Capa_Negocio.Administracion
{
    public class Cls_Rule_Usuario_Perfil
    {
        private Cls_Dat_Usuario_Perfil OData = new Cls_Dat_Usuario_Perfil();

        public List<Cls_Ent_Usuario_Perfil> Usuario_Perfil_Listar(Cls_Ent_Usuario_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Usuario_Perfil_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Usuario_Perfil>();
            }
        }

    

        public void Usuario_Perfil_Insertar(Cls_Ent_Usuario_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Usuario_Perfil_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }



        public void Usuario_Perfil_Estado(Cls_Ent_Usuario_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Usuario_Perfil_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Usuario_Perfil_Eliminar(Cls_Ent_Usuario_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Usuario_Perfil_Eliminar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

    }
}
