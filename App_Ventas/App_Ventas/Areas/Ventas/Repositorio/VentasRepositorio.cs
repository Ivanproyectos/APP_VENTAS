using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Ventas;
using Capa_Negocio.Ventas;
namespace App_Ventas.Areas.Ventas.Repositorio
{
    public class VentasRepositorio : IDisposable
    {
        private Cls_Rule_Ventas _rule = new Cls_Rule_Ventas();


        public List<Cls_Ent_Ventas> Ventas_Paginado(string ORDEN_COLUMNA, string ORDEN, int FILAS, int PAGINA, string @WHERE, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Ventas_Paginado(ORDEN_COLUMNA, ORDEN, FILAS, PAGINA, @WHERE, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Ventas>();
            }
        }


        public void Ventas_Insertar(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Ventas_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Ventas_Detalle_Insertar(Cls_Ent_Ventas_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Ventas_Detalle_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Ventas_AnularVenta(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Ventas_AnularVenta(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public List<Cls_Ent_Ventas_Detalle> Ventas_Detalleventas_Listar(Cls_Ent_Ventas_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Ventas_Detalleventas_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Ventas_Detalle>();
            }
        }

        public void Ventas_Detalle_DevolverProducto(Cls_Ent_Ventas_Detalle entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Ventas_Detalle_DevolverProducto(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Ventas_ValidarCliente_Credito(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Ventas_ValidarCliente_Credito(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }



        public Cls_Ent_Ventas Ventas_Listar_Uno(Cls_Ent_Ventas entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Ventas_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Ventas();
            }
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}