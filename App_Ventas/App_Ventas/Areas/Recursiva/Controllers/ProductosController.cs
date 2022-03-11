using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Ventas.Areas.Recursiva.Models;
using App_Ventas.Areas.Administracion.Repositorio;
using Capa_Entidad;
using Capa_Entidad.Administracion;

namespace App_Ventas.Areas.Recursiva.Controllers
{
    public class ProductosController : Controller
    {
        //
        // GET: /Recursiva/Productos/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Mantenimiento_BuscarProducto(int ID_SUCURSAL, string GrillaCarga)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ProductoModelView model = new ProductoModelView();
            model.ID_SUCURSAL = ID_SUCURSAL;
            model.GrillaCarga = GrillaCarga;

            using (Listado_CombosRepositorio Repositorio = new Listado_CombosRepositorio())
            {
                model.Lista_Unidad_Medida = Repositorio.Unidad_Medida_Listar(ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_UNIDAD_MEDIDA,
                    Value = x.ID_UNIDAD_MEDIDA.ToString()
                }).ToList();
                model.Lista_Unidad_Medida.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }



            return View(model);
        }

    }
}
