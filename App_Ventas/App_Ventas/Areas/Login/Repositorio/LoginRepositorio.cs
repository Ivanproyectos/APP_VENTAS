using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Negocio.Login;

namespace App_Ventas.Areas.Login.Repositorio
{
    public class LoginRepositorio : IDisposable
    {
        Cls_Rule_Login _rule = new Cls_Rule_Login(); 
        public Cls_Ent_Usuario Login_Usuario(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Login_Usuario(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Usuario();
            }
        }

        public Cls_Ent_Usuario Usuario(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Usuario(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Usuario();
            }
        }

        public Cls_Ent_Usuario Usuario_Sistema(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _rule.Usuario_Sistema(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new Cls_Ent_Usuario();
            }
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}