using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Administracion;

namespace App_Ventas.Areas.Administracion.Repositorio
{
    public class DevolucionRepositorio : IDisposable
    {

        private Cls_Rule_Devolucion _rule = new Cls_Rule_Devolucion();

        public List<Cls_Ent_Devolucion> Devolucion_Listar(Cls_Ent_Devolucion entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Devolucion_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Devolucion>();
            }
        }

   




        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}