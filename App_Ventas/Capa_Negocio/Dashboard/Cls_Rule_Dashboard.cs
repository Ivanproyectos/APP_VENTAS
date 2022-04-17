using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Dashboard;
using Capa_Datos.Dashboard;
namespace Capa_Negocio.Dashboard
{
   public  class Cls_Rule_Dashboard
    {
       private Cls_Dat_Dashboard OData = new Cls_Dat_Dashboard();

       public Cls_Ent_Dashboard Dashboard_Listar_Uno(Cls_Ent_Dashboard entidad, ref Cls_Ent_Auditoria auditoria)
       {
           try
           {
               return OData.Dashboard_Listar_Uno(entidad, ref auditoria);
           }
           catch (Exception ex)
           {
               auditoria.Error(ex);
               return new Cls_Ent_Dashboard();
           }
       }
    }
}
