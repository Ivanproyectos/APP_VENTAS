using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Login.Repositorio; 

namespace App_Ventas.Controllers
{
    public class ModulosController : Controller
    {
        //
        // GET: /Modulos/

        public ActionResult Usuario_Modulos(long ID_PERFIL)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (ModulosRepositorio repositorio = new ModulosRepositorio())
                {
                    var lista = repositorio.Usuario_Sistema_Modulo(ID_PERFIL, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        if (!auditoria.RECHAZAR)
                        {
                            string html = "";
                            repositorio.Generar_Vista(lista, ref html, 1);
                            auditoria.OBJETO = html;
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



    }
}
