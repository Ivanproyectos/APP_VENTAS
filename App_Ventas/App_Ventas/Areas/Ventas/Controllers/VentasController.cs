using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Ventas.Areas.Ventas.Models;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Recursos;

namespace App_Ventas.Areas.Ventas.Controllers
{
    public class VentasController : Controller
    {
        //
        // GET: /Ventas/Ventas/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantenimiento()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            VentasModelView model = new VentasModelView();

     
            using (ClienteRepositorio RepositorioC = new ClienteRepositorio())
            {
                Cls_Ent_Cliente Entidad = new Cls_Ent_Cliente();
                Entidad.FLG_ESTADO = 1; // activos
                model.Lista_Cliente = RepositorioC.Cliente_Listar(Entidad, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.NOMBRES_APE + " - " +x.NUMERO_DOCUMENTO,
                    Value = x.ID_CLIENTE.ToString()
                }).ToList();
                model.Lista_Cliente.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            using (Listado_CombosRepositorio Repositorio = new Listado_CombosRepositorio())
            {
                model.Lista_Tipo_Comprobante = Repositorio.Tipo_Comprobante_Listar(ref auditoria).Where(e => e.ID_TIPO_COMPROBANTE == "01" || e.ID_TIPO_COMPROBANTE == "03" || e.ID_TIPO_COMPROBANTE == "88").Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_COMPROBANTE,
                    Value = x.ID_TIPO_COMPROBANTE.ToString()
                }).ToList();
                model.Lista_Tipo_Comprobante.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            return View(model);

        }





    }
}
