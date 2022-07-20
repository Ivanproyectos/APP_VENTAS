using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Ventas.Areas.Caja.Models;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using Capa_Entidad.Caja;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Recursos;
using App_Ventas.Areas.Inventario.Repositorio;
using App_Ventas.Areas.Caja.Repositorio;

namespace App_Ventas.Areas.Caja.Controllers
{
    public class CajaController : Controller
    {
        //
        // GET: /Caja/Caja/

        public ActionResult Index()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            CajaModelView model = new CajaModelView();
            using (SucursalRepositorio Repositorio = new SucursalRepositorio())
            {

                model.Lista_Sucursal = Repositorio.Sucursal_Listar(new Cls_Ent_Sucursal { FLG_ESTADO = 1 }, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_SUCURSAL,
                    Value = x.ID_SUCURSAL.ToString()
                }).ToList();
                model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "-- Error al cargar opciones --" });
                }
            }
            using (UsuarioRepositorio Repositorio = new UsuarioRepositorio())
            {
                model.Lista_Usuario = Repositorio.Usuario_Listar(new Cls_Ent_Usuario { FLG_ESTADO = 1 }, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.NOMBRES_APE,
                    Value = x.COD_USUARIO.ToString()
                }).ToList();
                model.Lista_Usuario.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "-- Error al cargar opciones --" });
                }
            }
            return View(model);
        }

        public ActionResult Caja_Listar(Cls_Ent_Caja entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (CajaRepositorio repositorio = new CajaRepositorio())
                {
                    auditoria.OBJETO = repositorio.Caja_Listar(entidad,ref auditoria);
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

        public ActionResult Caja_Movimiento_Listar(Cls_Ent_Caja entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (CajaRepositorio repositorio = new CajaRepositorio())
                {
                    auditoria.OBJETO = repositorio.Caja_Movimiento_Listar(entidad, ref auditoria);
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

        public ActionResult View_Movimiento(int id, string Accion)
        {
            CajaModelView model = new CajaModelView();
            model.Accion = Accion;
            model.ID_TIPO_MOVIMIENTO = id;
            Cls_Ent_Caja lista = new Cls_Ent_Caja();

            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            using (SucursalRepositorio Repositorio = new SucursalRepositorio())
            {
                model.Lista_Sucursal = Repositorio.Sucursal_Listar(new Cls_Ent_Sucursal { FLG_ESTADO = 1 }, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_SUCURSAL,
                    Value = x.ID_SUCURSAL.ToString()
                }).ToList();
                model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "-- Error al cargar opciones --" });
                }
            }

            if (Accion == "M")
            {
                using (CajaRepositorio repositorio = new CajaRepositorio())
                {
                    Cls_Ent_Caja entidad = new Cls_Ent_Caja();
                    auditoria = new Capa_Entidad.Cls_Ent_Auditoria();

                    entidad.ID_TIPO_MOVIMIENTO = id;
                    lista = repositorio.Caja_Movimiento_Listar_Uno(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        model.ID_TIPO_MOVIMIENTO = lista.ID_TIPO_MOVIMIENTO;
                        model.ID_SUCURSAL = lista.ID_SUCURSAL;
                        model.DESC_MOVIMIENTO = lista.DESC_MOVIMIENTO;
                        model.MONTO = lista.MONTO;
   
                    }
                }

            }

            return View(model);
        }

        public ActionResult Caja_Movimiento_Insertar(Cls_Ent_Caja entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (CajaRepositorio Cajarepositorio = new CajaRepositorio())
            {
                entidad.IP_CREACION = ip_local;
                Cajarepositorio.Caja_Movimiento_Insertar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Caja_Movimiento_Actualizar(Cls_Ent_Caja entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (CajaRepositorio Cajarepositorio = new CajaRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Cajarepositorio.Caja_Movimiento_Actualizar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Caja_Movimiento_Eliminar(Cls_Ent_Caja entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            using (CajaRepositorio Cajarepositorio = new CajaRepositorio())
            {
                Cajarepositorio.Caja_Movimiento_Eliminar(entidad, ref auditoria);
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
