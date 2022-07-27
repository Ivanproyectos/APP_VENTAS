using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Dashboard;
using Capa_Entidad.Inventario; 
using Capa_Negocio.Dashboard;

namespace App_Ventas.Areas.Dashboard.Repositorio
{
    public class DashboardRepositorio : IDisposable
    {
        private Cls_Rule_Dashboard _rule = new Cls_Rule_Dashboard();
        private Cls_Rule_Producto _rule_Producto = new Cls_Rule_Producto();
        private Cls_Rule_Venta _rule_venta = new Cls_Rule_Venta();

        public Cls_Ent_Dashboard Dashboard_Listar_Uno(Cls_Ent_Dashboard entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Dashboard_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Dashboard();
            }
        }

        public List<Cls_Ent_Movimiento_Producto> Dashboard_ProductoMovimiento_Listar(Cls_Ent_Movimiento_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule_Producto.Dashboard_ProductoMovimiento_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Movimiento_Producto>();
            }
        }

        public List<Cls_Ent_Translado_Producto> Dashboard_ProductoTranslados_Listar(Cls_Ent_Translado_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule_Producto.Dashboard_ProductoTranslados_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Translado_Producto>();
            }
        }
        public List<Cls_Ent_Venta> Dashboard_Venta_Listar(Cls_Ent_Venta entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule_venta.Dashboard_Venta_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Venta>();
            }
        }

        public List<Cls_Ent_Venta> Dashboard_Compras_Listar(Cls_Ent_Venta entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule_venta.Dashboard_Compras_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Venta>();
            }
        }



        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}