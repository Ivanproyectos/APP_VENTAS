using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Ventas.Models;

namespace App_Ventas.Areas.Ventas.Controllers
{
    public class VentasController : Controller
    {
        //
        // GET: /Ventas/Ventas/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantenimiento()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            VentasModelView model = new VentasModelView();

            model.Lista_Cliente = new List<SelectListItem>();
            model.Lista_Cliente.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            model.Lista_Tipo_Comprobante = new List<SelectListItem>();
            model.Lista_Tipo_Comprobante.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            return View(model);

        }


    }
}
