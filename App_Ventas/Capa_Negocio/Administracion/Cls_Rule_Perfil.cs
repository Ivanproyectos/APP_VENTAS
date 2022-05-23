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
    public class Cls_Rule_Perfil
    {

        private Cls_Dat_Perfil OData = new Cls_Dat_Perfil();

        public List<Cls_Ent_Perfil> Perfil_Listar(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Perfil_Listar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Perfil>();
            }
        }


        public Cls_Ent_Perfil Perfil_Listar_Uno(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return OData.Perfil_Listar_Uno(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Perfil();
            }
        }
        
     

        public void Perfil_Insertar(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Perfil_Insertar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }



        public void Perfil_Actualizar(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Perfil_Actualizar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Perfil_Estado(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Perfil_Estado(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }


        public void Perfil_Eliminar(Cls_Ent_Perfil entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                OData.Perfil_Eliminar(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

    }
}
