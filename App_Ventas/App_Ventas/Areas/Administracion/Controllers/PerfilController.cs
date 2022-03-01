using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Administracion.Models;

namespace App_Ventas.Areas.Administracion.Controllers
{
    public class PerfilController : Controller
    {
        //
        // GET: /Administracion/Perfil/

        public ActionResult Index()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            PerfilModelView model = new PerfilModelView();
            return View(model);
        }

        public ActionResult Mantenimiento(int id, string Accion)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            PerfilModelView model = new PerfilModelView();

    
            return View(model);
        }


    }
}
