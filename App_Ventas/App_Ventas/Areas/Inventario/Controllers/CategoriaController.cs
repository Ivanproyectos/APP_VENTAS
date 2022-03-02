using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Inventario.Models;

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

        public ActionResult Mantenimiento()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            CategoriaModelView model = new CategoriaModelView();
 
            return View(model);

        }

    }
}
