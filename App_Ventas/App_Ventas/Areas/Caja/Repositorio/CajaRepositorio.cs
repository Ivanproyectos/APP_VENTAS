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
        public Cls_Ent_Caja Caja_Movimiento_Listar_Uno(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Caja_Movimiento_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Caja();
            }
        }


        public List<Cls_Ent_Caja> Caja_Movimiento_Listar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Caja_Movimiento_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Caja>();
            }
        }

        public void Caja_Movimiento_Insertar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Caja_Movimiento_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Caja_Movimiento_Actualizar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Caja_Movimiento_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Caja_Movimiento_Eliminar(Cls_Ent_Caja entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Caja_Movimiento_Eliminar(entidad, ref auditoria);
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