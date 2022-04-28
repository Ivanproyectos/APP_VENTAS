using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Compras;
using Capa_Negocio.Compras;

namespace App_Ventas.Areas.Compras.Repositorio
{
    public class ComprasRepositorio : IDisposable
    {
        private Cls_Rule_Compras _rule = new Cls_Rule_Compras();

        public List<Cls_Ent_Compras> Compras_Paginado(string ORDEN_COLUMNA, string ORDEN, int FILAS, int START, string @WHERE, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Compras_Paginado(ORDEN_COLUMNA, ORDEN, FILAS, START, @WHERE, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Compras>();
            }
        }

        public void Compras_Insertar(Cls_Ent_Compras entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Compras_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Compras_Detalle_Insertar(Cls_Ent_Compras_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Compras_Detalle_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Compras_AnularVenta(Cls_Ent_Compras entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Compras_AnularVenta(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public List<Cls_Ent_Compras_Detalle> Compras_Detallecompras_Listar(Cls_Ent_Compras_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Compras_Detallecompras_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Compras_Detalle>();
            }
        }

        public Cls_Ent_Compras Compras_Listar_Uno(Cls_Ent_Compras entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Compras_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Compras();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}