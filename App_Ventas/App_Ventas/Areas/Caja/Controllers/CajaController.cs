using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Caja.Models;

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

            model.Lista_Sucursal = new List<SelectListItem>();
            model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            model.Lista_Usuario = new List<SelectListItem>();
            model.Lista_Usuario.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            return View(model);
        }

    }
}
