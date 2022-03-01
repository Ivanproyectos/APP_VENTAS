using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Administracion.Models;

namespace App_Ventas.Areas.Administracion.Controllers
{
    public class ConfigurarEmpresaController : Controller
    {
        //
        // GET: /Administracion/ConfigurarEmpresa/

        public ActionResult Index()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ConfiguracionEmpresaModelView model = new ConfiguracionEmpresaModelView();

            model.Lista_Ubigeo = new List<SelectListItem>();
            model.Lista_Ubigeo.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });


            return View(model);
        }

    }
}
