using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Ventas;
using Capa_Negocio.CuentasCobrar;

namespace App_Ventas.Areas.CuentasCobrar.Repositorio
{
    public class CuentasCobrarRepositorio : IDisposable
    {
        private Cls_Rule_CuentasCobrar _rule = new Cls_Rule_CuentasCobrar();

        public void CuentasCobrar_Insertar(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.CuentasCobrar_Insertar(entidad, ref auditoria);
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