using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Administracion;
namespace App_Ventas.Areas.Administracion.Repositorio
{
    public class ProveedorRepositorio : IDisposable
    {
        private Cls_Rule_Proveedor _rule = new Cls_Rule_Proveedor();

        public List<Cls_Ent_Proveedor> Proveedor_Listar(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Proveedor_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Proveedor>();
            }
        }

        public Cls_Ent_Proveedor Proveedor_Listar_Uno(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Proveedor_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Proveedor();
            }
        }



        public void Proveedor_Insertar(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Proveedor_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Proveedor_Actualizar(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Proveedor_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Proveedor_Estado(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Proveedor_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Proveedor_Eliminar(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Proveedor_Eliminar(entidad, ref auditoria);
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