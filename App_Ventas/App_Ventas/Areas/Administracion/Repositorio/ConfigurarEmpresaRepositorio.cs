using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Administracion;

namespace App_Ventas.Areas.Administracion.Repositorio
{
    public class ConfigurarEmpresaRepositorio : IDisposable
    {
        private Cls_Rule_ConfigurarEmpresa _rule = new Cls_Rule_ConfigurarEmpresa();

        public Cls_Ent_configurarEmpresa configurarEmpresa_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.configurarEmpresa_Listar(ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_configurarEmpresa();
            }
        }


        public void ConfigurarEmpresa_Insertar(Cls_Ent_configurarEmpresa entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.ConfigurarEmpresa_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }
        public void ConfigurarEmpresa_Actualizar(Cls_Ent_configurarEmpresa entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                _rule.ConfigurarEmpresa_Actualizar(entidad, ref auditoria);
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