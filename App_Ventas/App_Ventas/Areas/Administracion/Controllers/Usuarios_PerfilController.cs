using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Administracion.Models;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Recursos;

namespace App_Ventas.Areas.Administracion.Controllers
{
    public class Usuario_PerfilController : Controller
    {
        //
        // GET: /Administracion/Usuario_Perfils/

        public ActionResult Index()
        {
            return View();
        }

    


        public ActionResult Usuario_Perfil_Listar(Cls_Ent_Usuario_Perfil entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (Usuario_PerfilRepositorio repositorio = new Usuario_PerfilRepositorio())
                {
                    auditoria.OBJETO = repositorio.Usuario_Perfil_Listar(entidad, ref auditoria);
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




        public ActionResult Usuario_Perfil_Insertar(Cls_Ent_Usuario_Perfil entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (Usuario_PerfilRepositorio Usuario_Perfilrepositorio = new Usuario_PerfilRepositorio())
            {
                entidad.IP_CREACION = ip_local;
                Usuario_Perfilrepositorio.Usuario_Perfil_Insertar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

  

        public ActionResult Usuario_Perfil_Eliminar(Cls_Ent_Usuario_Perfil entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            using (Usuario_PerfilRepositorio Usuario_Perfilrepositorio = new Usuario_PerfilRepositorio())
            {
                Usuario_Perfilrepositorio.Usuario_Perfil_Eliminar(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Usuario_Perfil_Estado(Cls_Ent_Usuario_Perfil entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (Usuario_PerfilRepositorio Usuario_Perfilrepositorio = new Usuario_PerfilRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Usuario_Perfilrepositorio.Usuario_Perfil_Estado(entidad, ref auditoria);

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
