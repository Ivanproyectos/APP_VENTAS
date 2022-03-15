using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Ventas;
using Capa_Datos.Ventas;
namespace Capa_Negocio.Ventas
{
   public class Cls_Rule_Ventas
    {
       private Cls_Dat_Ventas OData = new Cls_Dat_Ventas();



       public List<Cls_Ent_Ventas> Ventas_Paginado(string ORDEN_COLUMNA, string ORDEN, int FILAS, int PAGINA, string @WHERE, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               return OData.Ventas_Paginado(ORDEN_COLUMNA, ORDEN, FILAS, PAGINA, @WHERE, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
               return new List<Cls_Ent_Ventas>();
           }
       }



       public void Ventas_Insertar(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               OData.Ventas_Insertar(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
           }
       }


       public void Ventas_Detalle_Insertar(Cls_Ent_Ventas_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               OData.Ventas_Detalle_Insertar(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
           }
       }

       public void Ventas_AnularVenta(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               OData.Ventas_AnularVenta(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
           }
       }

       public List<Cls_Ent_Ventas_Detalle> Ventas_Detalleventas_Listar(Cls_Ent_Ventas_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Ventas_Detalleventas_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Ventas_Detalle>();
            }
        }


       public void Ventas_Detalle_DevolverProducto(Cls_Ent_Ventas_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               OData.Ventas_Detalle_DevolverProducto(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
           }
       }

       


    }
}
