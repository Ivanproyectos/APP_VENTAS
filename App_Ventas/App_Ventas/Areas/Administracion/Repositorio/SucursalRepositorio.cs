using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Administracion;
namespace App_Ventas.Areas.Administracion.Repositorio
{
    public class SucursalRepositorio : IDisposable
    {
        private Cls_Rule_Sucursal _rule = new Cls_Rule_Sucursal();

        public List<Cls_Ent_Sucursal> Sucursal_Listar(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Sucursal_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Sucursal>();
            }
        }

        public Cls_Ent_Sucursal Sucursal_Listar_Uno(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Sucursal_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Sucursal();
            }
        }



        public void Sucursal_Insertar(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Sucursal_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Sucursal_Actualizar(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Sucursal_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Sucursal_Estado(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Sucursal_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Sucursal_Eliminar(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Sucursal_Eliminar(entidad, ref auditoria);
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