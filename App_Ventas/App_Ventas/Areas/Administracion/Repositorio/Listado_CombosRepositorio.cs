using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Listados_Combos;

namespace App_Ventas.Areas.Administracion.Repositorio
{
    public class Listado_CombosRepositorio : IDisposable
    {

        private Cls_Rule_Listados _rule = new Cls_Rule_Listados();

        public List<Cls_Ent_Ubigeo> Ubigeo_Listar(  ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Ubigeo_Listar( ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Ubigeo>();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }



    }
}