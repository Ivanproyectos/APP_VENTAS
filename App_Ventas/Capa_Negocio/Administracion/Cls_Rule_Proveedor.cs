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
    public class Cls_Rule_Proveedor
    {
        private Cls_Dat_Proveedor OData = new Cls_Dat_Proveedor();

        public List<Cls_Ent_Proveedor> Proveedor_Listar(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Proveedor_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Proveedor>();
            }
        }

        public Cls_Ent_Proveedor Proveedor_Listar_Uno(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Proveedor_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Proveedor();
            }
        }


        public void Proveedor_Insertar(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Proveedor_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }



        public void Proveedor_Actualizar(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Proveedor_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Proveedor_Estado(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Proveedor_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Proveedor_Eliminar(Cls_Ent_Proveedor entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Proveedor_Eliminar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


    }
}
