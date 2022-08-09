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
using Capa_Token; 


namespace App_Ventas.Areas.Administracion.Controllers
{
    public class MiCuentaController : Controller
    {
        // GET: Administracion/MiCuenta
        public ActionResult Index()
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            UsuariosModelView model = new UsuariosModelView();
            using (Listado_CombosRepositorio RepositorioUbigeo = new Listado_CombosRepositorio())
            {
                model.Lista_Tipo_Documento = RepositorioUbigeo.Tipo_Documento_Listar(ref auditoria).Where(t => t.ID_TIPO_DOCUMENTO != 6).Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_DOCUMENTO,
                    Value = x.ID_TIPO_DOCUMENTO.ToString()
                }).ToList();
                model.Lista_Tipo_Documento.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            using (UsuarioRepositorio repositorioUsuario = new UsuarioRepositorio())
            {
                Cls_Ent_Usuario lista = new Cls_Ent_Usuario();
                Cls_Ent_Usuario entidad = new Cls_Ent_Usuario();
                var cook = HttpContext.Request.Cookies["IP-CyberToken"];
                string Token = cook.Value;
                entidad.ID_USUARIO = Cls_Api_Token.Claim_ID_USUARIO(Token);
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
            return View(model);
        }

       
    }
}
