using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Administracion.Models;

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

        public ActionResult Mantenimiento()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ClientesModelView model = new ClientesModelView();
            model.Lista_Ubigeo = new List<SelectListItem>();
            model.Lista_Ubigeo.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            model.Lista_Tipo_Documento = new List<SelectListItem>();
            model.Lista_Tipo_Documento.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            return View(model);
    
        }



    }
}
