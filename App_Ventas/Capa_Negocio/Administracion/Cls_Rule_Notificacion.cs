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
    public class Cls_Rule_Notificacion
    {
        private Cls_Dat_Notificacion OData = new Cls_Dat_Notificacion();

        public List<Cls_Ent_Notificacion> Notificacion_Listar(Cls_Ent_Notificacion entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Notificacion_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Notificacion>();
            }
        }

        public void Notificacion_Estado(ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Notificacion_Estado( ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

    }
}
