using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Compras.Models;

namespace App_Ventas.Areas.Compras.Controllers
{
    public class ComprasController : Controller
    {
        //
        // GET: /Compras/Compras/

        public ActionResult Index()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ComprasModelView model = new ComprasModelView();
  
            model.Lista_Sucursal = new List<SelectListItem>();
            model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            return View(model);
        }

        public ActionResult Mantenimiento()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ComprasModelView model = new ComprasModelView();
            model.Lista_Proveedor = new List<SelectListItem>();
            model.Lista_Proveedor.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            model.Lista_Sucursal = new List<SelectListItem>();
            model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            model.Lista_Tipo_Comprobante = new List<SelectListItem>();
            model.Lista_Tipo_Comprobante.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            return View(model);

        }


    }
}
