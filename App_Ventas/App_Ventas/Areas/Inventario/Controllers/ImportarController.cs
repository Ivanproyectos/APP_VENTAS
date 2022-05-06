﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using App_Ventas.Areas.Inventario.Models;
using App_Ventas.Areas.Ventas.Models;
using Capa_Entidad.Inventario;

namespace App_Ventas.Areas.Inventario.Controllers
{
    public class ImportarController : Controller
    {
        //
        // GET: /Inventario/Importar_Producto/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View_Importar(int ID_SUCURSAL, string DESC_SUCURSAL)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            BuscarProductoModelView model = new BuscarProductoModelView();
            model.ID_SUCURSAL = ID_SUCURSAL;
            model.DESC_SUCURSAL = DESC_SUCURSAL;
            return View(model);
        }
        public ActionResult Importar_DescagarPlantilla()
        {
            return View();
        }

        public ActionResult Importar_CargarExcel(IEnumerable<HttpPostedFileBase> fileArchivo, FormCollection forms)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            if (fileArchivo != null)
            {
                if (fileArchivo.ToList()[0] != null)
                {
                    long ID_SUCURSAL = long.Parse(forms["ID_SUCURSAL"].ToString());

                }
                else {
                    auditoria.Rechazar("No se encontró ningun archivo, seleccione alguno");
                }
            }
            else
            {
                auditoria.Rechazar("No se encontró ningun archivo, seleccione alguno");
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }
    }
}
