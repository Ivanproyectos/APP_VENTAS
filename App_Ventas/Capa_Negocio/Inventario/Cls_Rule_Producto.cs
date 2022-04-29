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
    public class Cls_Rule_Producto
    {
        private Cls_Dat_Producto OData = new Cls_Dat_Producto();


        public List<Cls_Ent_Producto> Productos_Paginado(string ORDEN_COLUMNA, string ORDEN, int FILAS, int START, string @WHERE, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Productos_Paginado(ORDEN_COLUMNA, ORDEN, FILAS, START, @WHERE, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Producto>();
            }
        }
        public List<Cls_Ent_Producto> Producto_Listar(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Producto_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Producto>();
            }
        }

        public List<Cls_Ent_Producto> Producto_Buscar_Listar(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Producto_Buscar_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Producto>();
            }
        }

        


        public Cls_Ent_Producto Producto_Listar_Uno(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Producto_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Producto();
            }
        }


        public void Producto_Insertar(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Producto_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }



        public void Producto_Actualizar(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Producto_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Producto_Estado(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Producto_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Producto_Eliminar(Cls_Ent_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Producto_Eliminar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Producto_Movimiento_Insertar(Cls_Ent_Movimiento_Producto entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Producto_Movimiento_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }
        


    }
}
