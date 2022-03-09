using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Inventario;
using Capa_Datos.Inventario;

namespace Capa_Negocio.Inventario
{
    public class Cls_Rule_Categoria
    {
        private Cls_Dat_Categoria OData = new Cls_Dat_Categoria();

        public List<Cls_Ent_Categoria> Categoria_Listar(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Categoria_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Categoria>();
            }
        }

        public Cls_Ent_Categoria Categoria_Listar_Uno(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Categoria_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Categoria();
            }
        }


        public void Categoria_Insertar(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Categoria_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }



        public void Categoria_Actualizar(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Categoria_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Categoria_Estado(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Categoria_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Categoria_Eliminar(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Categoria_Eliminar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

    }
}
