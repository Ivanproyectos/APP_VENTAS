using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad.Login;
using Capa_Entidad;
using Capa_Entidad.Administracion; 
using Capa_Negocio.Login; 
namespace App_Ventas.Areas.Login.Repositorio
{
    public class RepositorioModulosPerfil : IDisposable
    {
        private Cls_Rule_ModulosPerfil _Rule = new Cls_Rule_ModulosPerfil();

        public List<Cls_Ent_Modulo> Modulos_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            List<Cls_Ent_Modulo> lista = new List<Cls_Ent_Modulo>();
            try
            {
                lista = _Rule.Modulos_Listar(ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Modulo>();
            }
            return lista;
        }

        public List<Cls_Ent_Modulo> Perfiles_Modulos_Listar(Cls_Ent_Sistemas_Perfiles entidad, ref Cls_Ent_Auditoria auditoria)
        {
            List<Cls_Ent_Modulo> lista = new List<Cls_Ent_Modulo>();
            try
            {
                lista = _Rule.Perfiles_Modulos_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Modulo>();
            }
            return lista;
        }

        public void Perfiles_Modulos_Registrar(Cls_Ent_Sistemas_Perfiles entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _Rule.Perfiles_Modulos_Registrar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Perfiles_Modulos_Eliminar(Cls_Ent_Sistemas_Perfiles entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _Rule.Perfiles_Modulos_Eliminar(entidad, ref auditoria);
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