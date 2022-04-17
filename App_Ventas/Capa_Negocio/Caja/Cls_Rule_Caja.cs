using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Caja;
using Capa_Datos.Caja;

namespace Capa_Negocio.Caja
{
   public class Cls_Rule_Caja
    {
       private Cls_Dat_Caja OData = new Cls_Dat_Caja();

       public Cls_Ent_Caja Caja_Listar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               return OData.Caja_Listar(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
               return new Cls_Ent_Caja();
           }
       }

       public Cls_Ent_Caja Caja_Movimiento_Listar_Uno(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               return OData.Caja_Movimiento_Listar_Uno(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
               return new Cls_Ent_Caja();
           }
       }

       public List<Cls_Ent_Caja> Caja_Movimiento_Listar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               return OData.Caja_Movimiento_Listar(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
               return new List<Cls_Ent_Caja>();
           }
       }
       public void Caja_Movimiento_Insertar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               OData.Caja_Movimiento_Insertar(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
           }
       }

       public void Caja_Movimiento_Actualizar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               OData.Caja_Movimiento_Actualizar(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
           }
       }


       public void Caja_Movimiento_Eliminar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               OData.Caja_Movimiento_Eliminar(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
           }
       }
    }
}
