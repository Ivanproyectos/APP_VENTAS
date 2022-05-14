using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Ventas.Areas.Ventas.Models;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using Capa_Entidad.Inventario;
using Capa_Entidad.Ventas;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Recursos;
using App_Ventas.Areas.Inventario.Repositorio;
using App_Ventas.Areas.Ventas.Repositorio;

namespace App_Ventas.Areas.Ventas.Controllers
{
    public class Consulta_VentasController : Controller
    {
        //
        // GET: /Ventas/Consulta_Ventas/

        public ActionResult Index()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            VentasModelView model = new VentasModelView();
            using (Listado_CombosRepositorio Repositorio = new Listado_CombosRepositorio())
            {
                model.Lista_Tipo_Comprobante = Repositorio.Tipo_Comprobante_Listar(ref auditoria).Where(e => e.ID_TIPO_COMPROBANTE == "01" || e.ID_TIPO_COMPROBANTE == "03" || e.ID_TIPO_COMPROBANTE == "88").Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_COMPROBANTE,
                    Value = x.ID_TIPO_COMPROBANTE.ToString()
                }).ToList();
                model.Lista_Tipo_Comprobante.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            }
            using (UsuarioRepositorio Repositorio = new UsuarioRepositorio())
            {
                Cls_Ent_Usuario ent_usuario = new Cls_Ent_Usuario
                {
                    FLG_ESTADO = 2
                }; 

                model.Lista_Usuarios = Repositorio.Usuario_Listar(ent_usuario,ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.NOMBRES_APE,
                    Value = x.COD_USUARIO.ToString()
                }).ToList();
                model.Lista_Usuarios.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            using (SucursalRepositorio Repositorio = new SucursalRepositorio())
            {
                Cls_Ent_Sucursal ent_usuario = new Cls_Ent_Sucursal
                {
                    FLG_ESTADO = 1
                };

                model.Lista_Sucursal = Repositorio.Sucursal_Listar(ent_usuario, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_SUCURSAL,
                    Value = x.ID_SUCURSAL.ToString()
                }).ToList();
                model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            using (Listado_CombosRepositorio Repositorio = new Listado_CombosRepositorio())
            {
                model.Lista_Tipo_Pago = Repositorio.Tipo_Tipo_Pago_Listar(ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_PAGO,
                    Value = x.ID_TIPO_PAGO.ToString()
                }).ToList();
                model.Lista_Tipo_Pago.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            return View(model);
        }


        public ActionResult View_Exportar(VentasModelView Params)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            VentasModelView model = new VentasModelView();
            //model.ID_SUCURSAL = ID_SUCURSAL;
            //model.FLG_SERVICIO_INT = FLG_SERVICIO;
            return View(Params);
        }

     

    }
}
