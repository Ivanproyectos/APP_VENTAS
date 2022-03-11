using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Inventario;
using Capa_Negocio.Inventario;

namespace App_Ventas.Areas.Recursiva.Repositorio
{
    public class ProductosRepostiorio : IDisposable
    {
          private Cls_Rule_Producto _rule = new Cls_Rule_Producto();

          public List<Cls_Ent_Producto> Producto_Buscar_Listar(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Producto_Buscar_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Producto>();
            }
        }

             public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        
    }
}