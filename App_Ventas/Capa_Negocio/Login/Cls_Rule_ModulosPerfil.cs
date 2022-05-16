using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Administracion; 
using Capa_Entidad.Login;
using Capa_Datos.Login;
namespace Capa_Negocio.Login
{
    public class Cls_Rule_ModulosPerfil
    {
        private Cls_Dat_ModulosPerfil OData = new Cls_Dat_ModulosPerfil();

        public List<Cls_Ent_Modulo> Modulos_Listar( ref Cls_Ent_Auditoria auditoria)
        {
            List<Cls_Ent_Modulo> lista = new List<Cls_Ent_Modulo>();
            try
            {
                lista = OData.Modulos_Listar(ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Modulo>();
            }
            return lista;
        }

        public List<Cls_Ent_Modulo> Perfiles_Modulos_Listar(Cls_Ent_Sistemas_Perfiles entidad, ref Cls_Ent_Auditoria auditoria)
        {
            List<Cls_Ent_Modulo> lista = new List<Cls_Ent_Modulo>();
            try
            {
                lista = OData.Perfiles_Modulos_Listar(entidad,ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Modulo>();
            }
            return lista;
        }

        public void Perfiles_Modulos_Registrar(Cls_Ent_Sistemas_Perfiles entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Perfiles_Modulos_Registrar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Perfiles_Modulos_Eliminar(Cls_Ent_Sistemas_Perfiles entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Perfiles_Modulos_Eliminar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


    }
}
