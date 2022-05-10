using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.CargaExcel;
using Capa_Negocio.CargaExcel;

namespace App_Ventas.Areas.Recursiva.Repositorio
{
    public class CargaExcelRepostiorio : IDisposable
    {
        private Cls_Rule_Campo _rule = new Cls_Rule_Campo();

        public List<Cls_Ent_Campo> Campo_Listar( ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Campo_Listar( ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Campo>();
            }
        }

         public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        
    }
}