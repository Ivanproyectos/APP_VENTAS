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
    public class Cls_Rule_Usuario
    {
        private Cls_Dat_Usuario OData = new Cls_Dat_Usuario();

        public List<Cls_Ent_Usuario> Usuario_Listar(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Usuario_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Usuario>();
            }
        }

        public Cls_Ent_Usuario Usuario_Listar_Uno(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Usuario_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Usuario();
            }
        }


        public void Usuario_Insertar(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Usuario_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }



        public void Usuario_Actualizar(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Usuario_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Usuario_Estado(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Usuario_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Usuario_Eliminar(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Usuario_Eliminar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Usuario_ActualizarClave(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Usuario_ActualizarClave(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }
        

    }
}
