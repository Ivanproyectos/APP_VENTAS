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
    public class NotificacionController : Controller
    {
        // GET: Administracion/Notificacion
        public ActionResult Index()
        {
            NotificacioModelView model = new NotificacioModelView(); 
            using (NotificacionRepositorio repositorio = new NotificacionRepositorio())
            {
                Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
                model.Lista = repositorio.Notificacion_Listar(new Cls_Ent_Notificacion {ESTADO = 0}, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return View(model);
        }

        public ActionResult Notificacion_Listar(Cls_Ent_Notificacion entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (NotificacionRepositorio repositorio = new NotificacionRepositorio())
                {
                    auditoria.OBJETO = repositorio.Notificacion_Listar(entidad, ref auditoria);
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


        public ActionResult Notificacion_Estado()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            using (NotificacionRepositorio Clienterepositorio = new NotificacionRepositorio())
            {
                Clienterepositorio.Notificacion_Estado( ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }
    }
}
