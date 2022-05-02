using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Inventario;
using Capa_Negocio.Inventario;

namespace App_Ventas.Areas.Inventario.Repositorio
{
    public class Translado_ProductoRepositorio : IDisposable
    {
        private Cls_Rule_Translado_Producto _rule = new Cls_Rule_Translado_Producto();
        public void Producto_Translado_Insertar(Cls_Ent_Translado_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Producto_Translado_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Producto_Translado_Detalle_Insertar(Cls_Ent_Translado_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Producto_Translado_Detalle_Insertar(entidad, ref auditoria);
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