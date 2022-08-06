using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Administracion;

namespace App_Ventas.Areas.Administracion.Repositorio
{
    public class NotificacionRepositorio : IDisposable
    {
        private Cls_Rule_Notificacion _rule = new Cls_Rule_Notificacion();

        public List<Cls_Ent_Notificacion> Notificacion_Listar(Cls_Ent_Notificacion entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Notificacion_Listar(entidad, ref auditoria);
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
                _rule.Notificacion_Estado(ref auditoria);
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