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
    public class Cls_Rule_Venta
    {
        private Cls_Dat_Ventas OData = new Cls_Dat_Ventas();

        public List<Cls_Ent_Venta> Dashboard_Venta_Listar(Cls_Ent_Venta entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Dashboard_Venta_Listar(entidad, ref auditoria);
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
                return OData.Dashboard_Compras_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Venta>();
            }
        }


    }
}
