using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_Ventas.Areas.Administracion.Controllers
{
    public class PersonalController : Controller
    {
        //
        // GET: /Administracion/Personal/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantenimiento()
        {
            return View();
        }

    }
}
