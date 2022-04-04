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
    public class ClientesController : Controller
    {
        //
        // GET: /Administracion/Clientes/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Cliente_Listar(Cls_Ent_Cliente entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (ClienteRepositorio repositorio = new ClienteRepositorio())
                {
                    auditoria.OBJETO = repositorio.Cliente_Listar(entidad, ref auditoria);
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
            model.ID_CLIENTE = id;
            Cls_Ent_Cliente lista = new Cls_Ent_Cliente();

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
                using (ClienteRepositorio repositorioCliente = new ClienteRepositorio())
                {
                    Cls_Ent_Cliente entidad = new Cls_Ent_Cliente();
                    auditoria = new Capa_Entidad.Cls_Ent_Auditoria();

                    entidad.ID_CLIENTE = id;
                    lista = repositorioCliente.Cliente_Listar_Uno(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        model.ID_CLIENTE = lista.ID_CLIENTE;
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

        public ActionResult Cliente_Insertar(Cls_Ent_Cliente entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (ClienteRepositorio Clienterepositorio = new ClienteRepositorio())
            {
                entidad.IP_CREACION = ip_local;
                Clienterepositorio.Cliente_Insertar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cliente_Actualizar(Cls_Ent_Cliente entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (ClienteRepositorio Clienterepositorio = new ClienteRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Clienterepositorio.Cliente_Actualizar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cliente_Eliminar(Cls_Ent_Cliente entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            using (ClienteRepositorio Clienterepositorio = new ClienteRepositorio())
            {
                Clienterepositorio.Cliente_Eliminar(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cliente_Estado(Cls_Ent_Cliente entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (ClienteRepositorio Clienterepositorio = new ClienteRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Clienterepositorio.Cliente_Estado(entidad, ref auditoria);

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
