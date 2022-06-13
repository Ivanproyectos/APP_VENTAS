using System;
using System.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Entidad.Login;
using Capa_Entidad.Base;
using App_Ventas.Areas.Login.Repositorio;
using Capa_Token; 

namespace App_Ventas.Areas.Login.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login_Validar(Cls_Ent_Usuario entidad, int RememberMe)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (LoginRepositorio repositorio = new LoginRepositorio())
                {
                    Cls_Ent_Usuario Usuario = repositorio.Login_Usuario(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        if (!auditoria.RECHAZAR)
                        {
                            auditoria.OBJETO = Cls_Api_Token.Generar(Usuario.ID_USUARIO.ToString(),
                                                                        Usuario.COD_USUARIO, 
                                                                        RememberMe.ToString(),
                                                                        Convert.ToString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Usuario(string TOKEN)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            int ID_USUARIO = 0; 
            try
            {
                if (!string.IsNullOrEmpty(TOKEN))
                {
                    ID_USUARIO = Cls_Api_Token.Claim_ID_USUARIO(TOKEN);
                    using (LoginRepositorio repositorio = new LoginRepositorio())
                    {
                        Cls_Ent_Usuario Usuario = repositorio.Usuario(new Cls_Ent_Usuario { ID_USUARIO = ID_USUARIO }, ref auditoria);
                        if (!auditoria.EJECUCION_PROCEDIMIENTO)
                        {
                            string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                            auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                        }
                        else {
                            if (!auditoria.RECHAZAR)
                            {
                                auditoria.OBJETO = Usuario; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult View_UsuarioLogeado() {

            return PartialView();
        }




    }
}
