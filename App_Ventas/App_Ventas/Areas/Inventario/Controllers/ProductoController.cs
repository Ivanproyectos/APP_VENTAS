﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using App_Ventas.Areas.Inventario.Models;
using Capa_Entidad.Administracion;
using App_Ventas.Areas.Administracion.Repositorio; 

namespace App_Ventas.Areas.Inventario.Controllers
{
    public class ProductoController : Controller
    {
        //
        // GET: /Inventario/Producto/

        public ActionResult Index()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ProductoModelView model = new ProductoModelView();

            model.Lista_Sucursal = new List<SelectListItem>();
            model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            model.Lista_Unidad_Medida = new List<SelectListItem>();
            model.Lista_Unidad_Medida.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            return View(model);
       
        }

        public ActionResult Mantenimiento(int id, string Accion)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ProductoModelView model = new ProductoModelView();

     
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
