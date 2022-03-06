using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.CuentasCobrar.Models;

namespace App_Ventas.Areas.CuentasCobrar.Controllers
{
    public class CuentasCobrarController : Controller
    {
        //
        // GET: /CuentasCobrar/CuentasCobrar/

        public ActionResult Index()
        {

            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            CuentasCobrarModelView model = new CuentasCobrarModelView();

            model.Lista_Sucursal = new List<SelectListItem>();
            model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            return View(model);
        }




    }
}
