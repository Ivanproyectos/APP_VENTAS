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
    public class ProveedorController : Controller
    {
        //
        // GET: /Administracion/Proveedors/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Proveedor_Listar(Cls_Ent_Proveedor entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (ProveedorRepositorio repositorio = new ProveedorRepositorio())
                {
                    auditoria.OBJETO = repositorio.Proveedor_Listar(entidad, ref auditoria);
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
            ClientesModelView model = new ClientesModelView();
            model.Accion = Accion;
            model.ID_PROVEEDOR = id;
            Cls_Ent_Proveedor lista = new Cls_Ent_Proveedor();

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

            using (Listado_CombosRepositorio RepositorioUbigeo = new Listado_CombosRepositorio())
            {
                model.Lista_Tipo_Documento = RepositorioUbigeo.Tipo_Documento_Listar(ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_DOCUMENTO,
                    Value = x.ID_TIPO_DOCUMENTO.ToString()
                }).ToList();
                model.Lista_Tipo_Documento.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }


            if (Accion == "M")
            {
                using (ProveedorRepositorio repositorioProveedor = new ProveedorRepositorio())
                {
                    Cls_Ent_Proveedor entidad = new Cls_Ent_Proveedor();
                    auditoria = new Capa_Entidad.Cls_Ent_Auditoria();

                    entidad.ID_PROVEEDOR = id;
                    lista = repositorioProveedor.Proveedor_Listar_Uno(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        model.ID_PROVEEDOR = lista.ID_PROVEEDOR;
                        model.NOMBRES_APE = lista.NOMBRES_APE;
                        model.NUMERO_DOCUMENTO = lista.NUMERO_DOCUMENTO;
                        model.DIRECCION = lista.DIRECCION;
                        model.CORREO = lista.CORREO;
                        model.TELEFONO = lista.TELEFONO;
                        model.CELULAR = lista.CELULAR;
                        model.COD_UBIGEO = lista.COD_UBIGEO;
                        model.DETALLE = lista.DETALLE;
                        model.ID_TIPO_DOCUMENTO = lista.ID_TIPO_DOCUMENTO;

                    }
                }

            }

            return View(model);
        }

        public ActionResult Proveedor_Insertar(Cls_Ent_Proveedor entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (ProveedorRepositorio Proveedorrepositorio = new ProveedorRepositorio())
            {
                entidad.IP_CREACION = ip_local;
                Proveedorrepositorio.Proveedor_Insertar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Proveedor_Actualizar(Cls_Ent_Proveedor entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (ProveedorRepositorio Proveedorrepositorio = new ProveedorRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Proveedorrepositorio.Proveedor_Actualizar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Proveedor_Eliminar(Cls_Ent_Proveedor entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            using (ProveedorRepositorio Proveedorrepositorio = new ProveedorRepositorio())
            {
                Proveedorrepositorio.Proveedor_Eliminar(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Proveedor_Estado(Cls_Ent_Proveedor entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (ProveedorRepositorio Proveedorrepositorio = new ProveedorRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Proveedorrepositorio.Proveedor_Estado(entidad, ref auditoria);

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
