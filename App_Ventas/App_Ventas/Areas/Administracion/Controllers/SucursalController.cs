using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Administracion.Models;
using Capa_Entidad.Administracion;
using Capa_Entidad.Base;
using App_Ventas.Areas.Administracion.Repositorio;

namespace App_Ventas.Areas.Administracion.Controllers
{
    public class SucursalController : Controller
    {
        //
        // GET: /Administracion/Sucursal/

        public ActionResult Index()
        {
            return View();
        }

 
        public ActionResult Sucursal_Listar(Cls_Ent_Sucursal entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (SucursalRepositorio repositorio = new SucursalRepositorio())
                {
                    auditoria.OBJETO = repositorio.Sucursal_Listar(entidad, ref auditoria);
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

 

        public ActionResult Mantenimiento(int id, string Accion)
        {
            SucursalModelView model = new SucursalModelView();
            model.Accion = Accion;
            model.ID_SUCURSAL = id;
            Cls_Ent_Sucursal lista = new Cls_Ent_Sucursal();

            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            using (Listado_CombosRepositorio RepositorioUbigeo = new Listado_CombosRepositorio())
            {
                model.Lista_Ubigeo = RepositorioUbigeo.Ubigeo_Listar(ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_UBIGEO,
                    Value = x.ID_UBIGEO.ToString()
                }).ToList();
                model.Lista_Ubigeo.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            if (Accion == "M")
            {
                using (SucursalRepositorio repositorioSucursal = new SucursalRepositorio())
                {
                    Cls_Ent_Sucursal entidad = new Cls_Ent_Sucursal();
                    auditoria = new Capa_Entidad.Cls_Ent_Auditoria();

                    entidad.ID_SUCURSAL = id;
                    lista = repositorioSucursal.Sucursal_Listar_Uno(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        model.ID_SUCURSAL = lista.ID_SUCURSAL;
                        model.DESC_SUCURSAL = lista.DESC_SUCURSAL;
                        model.DIRECCION = lista.DIRECCION;
                        model.TELEFONO = lista.TELEFONO;
                        model.CORREO = lista.CORREO;
                        model.URBANIZACION = lista.URBANIZACION;
                        model.COD_UBIGEO = lista.COD_UBIGEO;

                    }
                }
            }
            return View(model);
        }

        public ActionResult Sucursal_Insertar(Cls_Ent_Sucursal entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (SucursalRepositorio Sucursalrepositorio = new SucursalRepositorio())
            {
                entidad.IP_CREACION = ip_local;
                Sucursalrepositorio.Sucursal_Insertar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Sucursal_Actualizar(Cls_Ent_Sucursal entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (SucursalRepositorio Sucursalrepositorio = new SucursalRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Sucursalrepositorio.Sucursal_Actualizar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Sucursal_Eliminar(Cls_Ent_Sucursal entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            using (SucursalRepositorio Sucursalrepositorio = new SucursalRepositorio())
            {
                Sucursalrepositorio.Sucursal_Eliminar(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Sucursal_Estado(Cls_Ent_Sucursal entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (SucursalRepositorio Sucursalrepositorio = new SucursalRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Sucursalrepositorio.Sucursal_Estado(entidad, ref auditoria);

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
