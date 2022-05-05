using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Ventas.Areas.Caja.Models;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using Capa_Entidad.Dashboard;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Areas.Dashboard.Repositorio;

namespace App_Ventas.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/Dashboard/

        public ActionResult Index()
        {
            List<Cls_Ent_Combo> ListaAnio = new List<Cls_Ent_Combo>(); 
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            DashboardModelView model = new DashboardModelView();
            using (SucursalRepositorio Repositorio = new SucursalRepositorio())
            {

                model.Lista_Sucursal = Repositorio.Sucursal_Listar(new Cls_Ent_Sucursal { FLG_ESTADO = 1 }, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_SUCURSAL,
                    Value = x.ID_SUCURSAL.ToString()
                }).ToList();
                model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "-- Error al cargar opciones --" });
                }
            }

            string Anio = DateTime.Now.ToString("yyyy");
            for (int i = 2022; i <= Convert.ToInt32(Anio); i++)
            {
                ListaAnio.Add(new Cls_Ent_Combo{ ID =i, DESCRIPCION = i.ToString()}); 
            }

            model.Lista_Anio = ListaAnio.Select(x => new SelectListItem()
            {
                Text = x.DESCRIPCION,
                Value = x.ID.ToString()
            }).ToList();
            //model.Lista_Anio.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
            return View(model);
        }

        public ActionResult Dashboard_Count_Listar(Cls_Ent_Dashboard entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            entidad.IP_CREACION = ip_local;
            try
            {
                using (DashboardRepositorio Dashboardrepositorio = new DashboardRepositorio())
                {

                   auditoria.OBJETO = Dashboardrepositorio.Dashboard_Listar_Uno(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                  
                }
            }
            catch (Exception ex)
            {
                string CODIGOLOG = Recursos.Clases.Css_Log.Guardar(ex.Message);
                auditoria.Rechazar(CODIGOLOG);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

    }
}
