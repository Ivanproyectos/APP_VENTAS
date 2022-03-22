using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Datos.Administracion;
namespace Capa_Negocio.Administracion
{
    public class Cls_Rule_Devolucion
    {

        private Cls_Dat_Devolucion OData = new Cls_Dat_Devolucion();

        public List<Cls_Ent_Devolucion> Devolucion_Listar(Cls_Ent_Devolucion entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Devolucion_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Devolucion>();
            }
        }

    }
}
