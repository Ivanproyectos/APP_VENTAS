using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Ventas.Areas.Administracion.Models;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Entidad.Base;
using App_Ventas.Areas.Administracion.Repositorio;
namespace App_Ventas.Areas.Administracion.Controllers
{
    public class DevolucionController : Controller
    {
        //
        // GET: /Administracion/Devolucion/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Devolucion_Listar(Cls_Ent_Devolucion entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (DevolucionRepositorio repositorio = new DevolucionRepositorio())
                {
                    auditoria.OBJETO = repositorio.Devolucion_Listar(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
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
