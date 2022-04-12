using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Caja;
using Capa_Negocio.Caja;
namespace App_Ventas.Areas.Caja.Repositorio
{
    public class CajaRepositorio : IDisposable
    {
        private Cls_Rule_Caja _rule = new Cls_Rule_Caja();
        public Cls_Ent_Caja Caja_Listar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Caja_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Caja();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}