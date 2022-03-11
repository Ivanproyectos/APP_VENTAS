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

        public List<Cls_Ent_Tipo_Documento> Tipo_Documento_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Tipo_Documento_Listar(ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Tipo_Documento>();
            }
        }

        public List<Cls_Ent_Unidad_Medida> Unidad_Medida_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Unidad_Medida_Listar(ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Unidad_Medida>();
            }
        }

        public List<Cls_Ent_Tipo_Comprobante> Tipo_Comprobante_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Tipo_Comprobante_Listar(ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Tipo_Comprobante>();
            }
        }


        
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }



    }
}