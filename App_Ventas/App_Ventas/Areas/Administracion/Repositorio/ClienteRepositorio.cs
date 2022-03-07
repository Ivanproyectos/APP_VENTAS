using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Administracion;
namespace App_Ventas.Areas.Administracion.Repositorio
{
    public class ClienteRepositorio : IDisposable
    {
        private Cls_Rule_Cliente _rule = new Cls_Rule_Cliente();

        public List<Cls_Ent_Cliente> Cliente_Listar(Cls_Ent_Cliente entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Cliente_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Cliente>();
            }
        }

        public Cls_Ent_Cliente Cliente_Listar_Uno(Cls_Ent_Cliente entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Cliente_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Cliente();
            }
        }



        public void Cliente_Insertar(Cls_Ent_Cliente entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Cliente_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Cliente_Actualizar(Cls_Ent_Cliente entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Cliente_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Cliente_Estado(Cls_Ent_Cliente entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Cliente_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Cliente_Eliminar(Cls_Ent_Cliente entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.Cliente_Eliminar(entidad, ref auditoria);
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