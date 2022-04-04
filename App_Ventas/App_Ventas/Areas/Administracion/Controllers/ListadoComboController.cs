using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Entidad.Base;
using App_Ventas.Areas.Administracion.Models;
using App_Ventas.Areas.Administracion.Repositorio;

namespace App_Ventas.Areas.Administracion.Controllers
{
    public class ListadoComboController : Controller
    {
        //
        // GET: /Administracion/ListadoCombo/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Ubigeo_Listar()
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (Listado_CombosRepositorio repositorio = new Listado_CombosRepositorio())
                {
                    auditoria.OBJETO = repositorio.Ubigeo_Listar(ref auditoria);
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
