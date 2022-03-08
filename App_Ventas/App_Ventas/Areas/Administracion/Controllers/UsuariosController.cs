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
    public class UsuariosController : Controller
    {
        //
        // GET: /Administracion/Usuarios/

        public ActionResult Index()
        {
            return View();
        }

    


        public ActionResult Usuario_Listar(Cls_Ent_Usuario entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (UsuarioRepositorio repositorio = new UsuarioRepositorio())
                {
                    auditoria.OBJETO = repositorio.Usuario_Listar(entidad, ref auditoria);
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
    
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            UsuariosModelView model = new UsuariosModelView();
            model.Accion = Accion;
            model.ID_USUARIO = id; 

            
            using (Listado_CombosRepositorio RepositorioUbigeo = new Listado_CombosRepositorio())
            {
                model.Lista_Tipo_Documento = RepositorioUbigeo.Tipo_Documento_Listar(ref auditoria).Where(t => t.ID_TIPO_DOCUMENTO != 6).Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_DOCUMENTO,
                    Value = x.ID_TIPO_DOCUMENTO.ToString()
                }).ToList();
                model.Lista_Tipo_Documento.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            using (PerfilRepositorio RepositorioPerfil = new PerfilRepositorio())
            {
                Cls_Ent_Perfil Entidadperfil = new  Cls_Ent_Perfil();
                Entidadperfil.FLG_ESTADO = 1; // activos
                model.Lista_Perfil = RepositorioPerfil.Perfil_Listar(Entidadperfil, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_PERFIL,
                    Value = x.ID_PERFIL.ToString()
                }).ToList();
                model.Lista_Perfil.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            using (SucursalRepositorio RepositorioUsuario = new SucursalRepositorio())
            {
                Cls_Ent_Sucursal EntidadUsuario= new Cls_Ent_Sucursal();
                EntidadUsuario.FLG_ESTADO = 1; // activos
                model.Lista_Sucursal = RepositorioUsuario.Sucursal_Listar(EntidadUsuario, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_SUCURSAL,
                    Value = x.ID_SUCURSAL.ToString()
                }).ToList();
                model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            if (Accion == "M")
            {
                using (UsuarioRepositorio repositorioUsuario = new UsuarioRepositorio())
                {
                    Cls_Ent_Usuario entidad = new Cls_Ent_Usuario();
                    auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
                    Cls_Ent_Usuario lista = new Cls_Ent_Usuario(); 
                    entidad.ID_USUARIO = id;
                    lista = repositorioUsuario.Usuario_Listar_Uno(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        model.ID_USUARIO = lista.ID_USUARIO;
                        model.NOMBRE = lista.NOMBRE;
                        model.APE_PATERNO = lista.APE_PATERNO;
                        model.APE_MATERNO = lista.APE_MATERNO;
                        model.DNI = lista.DNI;
                        model.ID_TIPO_DOCUMENTO = lista.ID_TIPO_DOCUMENTO;
                        model.CELULAR = lista.CELULAR;
                        model.TELEFONO = lista.TELEFONO;
                        model.CORREO = lista.CORREO;
                        model.COD_USUARIO = lista.COD_USUARIO;
                        model.CLAVE_USUARIO = lista.CLAVE_USUARIO;
                        model.FLG_ADMIN = lista.FLG_ADMIN == 1 ? true : false;
                    }
                }
            }
            return View(model);
        }

        public ActionResult Usuario_Insertar(Cls_Ent_Usuario entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (UsuarioRepositorio Usuariorepositorio = new UsuarioRepositorio())
            {
                entidad.IP_CREACION = ip_local;
                Usuariorepositorio.Usuario_Insertar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Usuario_Actualizar(Cls_Ent_Usuario entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (UsuarioRepositorio Usuariorepositorio = new UsuarioRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Usuariorepositorio.Usuario_Actualizar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Usuario_Eliminar(Cls_Ent_Usuario entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            using (UsuarioRepositorio Usuariorepositorio = new UsuarioRepositorio())
            {
                Usuariorepositorio.Usuario_Eliminar(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Usuario_Estado(Cls_Ent_Usuario entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (UsuarioRepositorio Usuariorepositorio = new UsuarioRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Usuariorepositorio.Usuario_Estado(entidad, ref auditoria);

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
