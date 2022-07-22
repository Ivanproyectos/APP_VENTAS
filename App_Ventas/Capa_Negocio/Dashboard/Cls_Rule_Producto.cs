using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Inventario;
using Capa_Datos.Dashboard;

namespace Capa_Negocio.Dashboard
{
   public class Cls_Rule_Producto
    {
       private Cls_Dat_Producto OData = new Cls_Dat_Producto();

       public List<Cls_Ent_Movimiento_Producto> Dashboard_ProductoMovimiento_Listar(Cls_Ent_Movimiento_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Dashboard_ProductoMovimiento_Listar(entidad, ref auditoria);
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
                return OData.Dashboard_ProductoTranslados_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Translado_Producto>();
            }
        }
        
    }
}
