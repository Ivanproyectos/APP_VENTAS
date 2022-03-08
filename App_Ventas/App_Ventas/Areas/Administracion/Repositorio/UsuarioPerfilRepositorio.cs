using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Administracion;
namespace App_Ventas.Areas.Administracion.Repositorio
{
    public class Usuario_PerfilRepositorio : IDisposable
    {
        private Cls_Rule_Usuario_Perfil _rule = new Cls_Rule_Usuario_Perfil();

        public List<Cls_Ent_Usuario_Perfil> Usuario_Perfil_Listar(Cls_Ent_Usuario_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Usuario_Perfil_Listar(entidad, ref auditoria);
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
                _rule.Usuario_Perfil_Insertar(entidad, ref auditoria);
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
                _rule.Usuario_Perfil_Estado(entidad, ref auditoria);
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
                _rule.Usuario_Perfil_Eliminar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}