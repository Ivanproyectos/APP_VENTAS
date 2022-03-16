using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Ventas.Areas.CuentasCobrar.Models;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using Capa_Entidad.Inventario;
using Capa_Entidad.Ventas;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Recursos;
using App_Ventas.Areas.Inventario.Repositorio;
using App_Ventas.Areas.Ventas.Repositorio;

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

 

            using (SucursalRepositorio RepositorioUbigeo = new SucursalRepositorio())
            {
                Cls_Ent_Sucursal entidad = new Cls_Ent_Sucursal
                {
                    FLG_ESTADO = 2
                }; 
                model.Lista_Sucursal = RepositorioUbigeo.Sucursal_Listar(entidad, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_SUCURSAL,
                    Value = x.ID_SUCURSAL.ToString()
                }).ToList();
                model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }


            using (ClienteRepositorio RepositorioC = new ClienteRepositorio())
            {
                Cls_Ent_Cliente Entidad = new Cls_Ent_Cliente();
                Entidad.FLG_ESTADO = 1; // activos
                model.Lista_Cliente = RepositorioC.Cliente_Listar(Entidad, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.NOMBRES_APE + " - " + x.NUMERO_DOCUMENTO,
                    Value = x.ID_CLIENTE.ToString()
                }).ToList();
                model.Lista_Cliente.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }



            return View(model);
        }




    }
}
