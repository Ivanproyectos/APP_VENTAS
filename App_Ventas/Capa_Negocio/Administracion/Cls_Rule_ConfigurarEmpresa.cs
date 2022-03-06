using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Datos.Administracion;

namespace Capa_Negocio.Administracion
{
    public class Cls_Rule_ConfigurarEmpresa
    {
        private Cls_Dat_ConfigurarEmpresa OData = new Cls_Dat_ConfigurarEmpresa();

        public Cls_Ent_configurarEmpresa configurarEmpresa_Listar(ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.configurarEmpresa_Listar(ref auditoria);
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
                OData.ConfigurarEmpresa_Insertar(entidad, ref auditoria);
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
                OData.ConfigurarEmpresa_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        
        


    }
}
