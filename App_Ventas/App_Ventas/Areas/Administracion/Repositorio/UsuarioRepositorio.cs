using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Administracion;
namespace App_Ventas.Areas.Administracion.Repositorio
{
    public class UsuarioRepositorio : IDisposable
    {
        private Cls_Rule_Usuario _rule = new Cls_Rule_Usuario();

        public List<Cls_Ent_Usuario> Usuario_Listar(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Usuario_Listar(entidad, ref auditoria);
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
                return _rule.Usuario_Listar_Uno(entidad, ref auditoria);
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
                _rule.Usuario_Insertar(entidad, ref auditoria);
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
                _rule.Usuario_Actualizar(entidad, ref auditoria);
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
                _rule.Usuario_Estado(entidad, ref auditoria);
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
                _rule.Usuario_Eliminar(entidad, ref auditoria);
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