using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Administracion.Models;

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

        public ActionResult Mantenimiento(int id, string Accion)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            UsuariosModelView model = new UsuariosModelView();
            model.Accion = Accion;
            model.ID_USUARIO = id; 

            model.Lista_Tipo_Documento = new List<SelectListItem>();
            model.Lista_Tipo_Documento.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });


            model.Lista_Sucursal = new List<SelectListItem>();
            model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            model.Lista_Perfil = new List<SelectListItem>();
            model.Lista_Perfil.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            return View(model);
        }


    }
}
