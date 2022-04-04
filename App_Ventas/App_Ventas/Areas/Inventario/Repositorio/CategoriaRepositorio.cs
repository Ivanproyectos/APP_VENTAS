using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Inventario;
using Capa_Negocio.Inventario;
namespace App_Ventas.Areas.Inventario.Repositorio
{
    public class CategoriaRepositorio : IDisposable
    {
        private Cls_Rule_Categoria _rule = new Cls_Rule_Categoria();

        public List<Cls_Ent_Categoria> Categoria_Listar(Cls_Ent_Categoria entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Categoria_Listar(entidad, ref auditoria);
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
                return _rule.Categoria_Listar_Uno(entidad, ref auditoria);
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
                _rule.Categoria_Insertar(entidad, ref auditoria);
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
                _rule.Categoria_Actualizar(entidad, ref auditoria);
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
                _rule.Categoria_Estado(entidad, ref auditoria);
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
                _rule.Categoria_Eliminar(entidad, ref auditoria);
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