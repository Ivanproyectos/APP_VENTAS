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
    public class Cls_Rule_Sucursal
    {
        private Cls_Dat_Sucursal OData = new Cls_Dat_Sucursal();

        public List<Cls_Ent_Sucursal> Sucursal_Listar(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Sucursal_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Sucursal>();
            }
        }

        public Cls_Ent_Sucursal Sucursal_Listar_Uno(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Sucursal_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Sucursal();
            }
        }


        public void Sucursal_Insertar(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Sucursal_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }



        public void Sucursal_Actualizar(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Sucursal_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Sucursal_Estado(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Sucursal_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Sucursal_Eliminar(Cls_Ent_Sucursal entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Sucursal_Eliminar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

    }
}
