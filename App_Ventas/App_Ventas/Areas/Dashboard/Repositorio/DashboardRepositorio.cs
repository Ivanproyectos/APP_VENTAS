using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Dashboard;
using Capa_Negocio.Dashboard;

namespace App_Ventas.Areas.Dashboard.Repositorio
{
    public class DashboardRepositorio : IDisposable
    {
        private Cls_Rule_Dashboard _rule = new Cls_Rule_Dashboard();
        public Cls_Ent_Dashboard Dashboard_Listar_Uno(Cls_Ent_Dashboard entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Dashboard_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Dashboard();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}