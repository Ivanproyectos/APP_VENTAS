using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Inventario.Models;
using Capa_Entidad.Inventario;
using Capa_Entidad.Base;
using App_Ventas.Areas.Inventario.Repositorio;

namespace App_Ventas.Areas.Inventario.Controllers
{
    public class CategoriaController : Controller
    {
        //
        // GET: /Inventario/Cateforia/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Categoria_Listar(Cls_Ent_Categoria entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (CategoriaRepositorio repositorio = new CategoriaRepositorio())
                {
                    auditoria.OBJETO = repositorio.Categoria_Listar(entidad, ref auditoria);
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
            CategoriaModelView model = new CategoriaModelView();
            model.Accion = Accion;
            model.ID_CATEGORIA = id;
            Cls_Ent_Categoria lista = new Cls_Ent_Categoria();

            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
      

            if (Accion == "M")
            {
                using (CategoriaRepositorio repositorioCategoria = new CategoriaRepositorio())
                {
                    Cls_Ent_Categoria entidad = new Cls_Ent_Categoria();
                    auditoria = new Capa_Entidad.Cls_Ent_Auditoria();

                    entidad.ID_CATEGORIA = id;
                    lista = repositorioCategoria.Categoria_Listar_Uno(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        model.ID_CATEGORIA = lista.ID_CATEGORIA;
                        model.DESC_CATEGORIA = lista.DESC_CATEGORIA;
                        model.DESCRIPCION = lista.DESCRIPCION;
     

                    }
                }
            }
            return View(model);
        }

        public ActionResult Categoria_Insertar(Cls_Ent_Categoria entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (CategoriaRepositorio Categoriarepositorio = new CategoriaRepositorio())
            {
                entidad.IP_CREACION = ip_local;
                Categoriarepositorio.Categoria_Insertar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Categoria_Actualizar(Cls_Ent_Categoria entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (CategoriaRepositorio Categoriarepositorio = new CategoriaRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Categoriarepositorio.Categoria_Actualizar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Categoria_Eliminar(Cls_Ent_Categoria entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            using (CategoriaRepositorio Categoriarepositorio = new CategoriaRepositorio())
            {
                Categoriarepositorio.Categoria_Eliminar(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Categoria_Estado(Cls_Ent_Categoria entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (CategoriaRepositorio Categoriarepositorio = new CategoriaRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Categoriarepositorio.Categoria_Estado(entidad, ref auditoria);

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
