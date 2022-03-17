using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Ventas.Areas.CuentasCobrar.Models;
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

        public JsonResult CuentasCobrar_Paginado(Recursos.Paginacion.GridTable grid)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                grid.page = (grid.page == 0) ? 1 : grid.page;
                grid.rows = (grid.rows == 0) ? 100 : grid.rows;

                var @where = (Recursos.Paginacion.Css_Paginacion.GetWhere(grid.filters, grid.rules));
                if (!string.IsNullOrEmpty(@where))
                {
                    grid._search = true;
                    if (!string.IsNullOrEmpty(grid.searchString))
                    {
                        @where = @where + " and ";
                    }
                }
                else
                {
                    @where = @where + " 1=1 ";
                }

                using (VentasRepositorio repositorio = new VentasRepositorio())
                {
                    IList<Cls_Ent_Ventas> lista = repositorio.Ventas_Paginado(grid.sidx, grid.sord, grid.rows, grid.page, @where, ref auditoria);
                    if (auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        var generic = Recursos.Paginacion.Css_Paginacion.BuscarPaginador(grid.page, grid.rows, (int)auditoria.OBJETO, lista);
                        generic.Value.rows = generic.List.Select(item => new Recursos.Paginacion.Css_Row
                        {
                            id = item.ID_VENTA.ToString(),
                            cell = new string[] {
                            null, 
                            item.FILA.ToString(),   
                            item.ID_VENTA.ToString(), 
                            item.ID_VENTA.ToString(), 
                            item.DESC_TIPO_COMPROBANTE,
                             item.CLIENTE, 
                            item.DESCUENTO.ToString(), 
                            item.ADELANTO.ToString(),
                            item.TOTAL.ToString(),     
                            item.DEBE.ToString(),
                            item.FEC_CREACION.ToString(),
                            item.COD_COMPROBANTE.ToString(),
                            item.FLG_ESTADO_CREDITO.ToString(),                       
                            item.DESC_ESTADO_CREDITO,
                            item.FLG_ANULADO.ToString(),   

                            }
                        }).ToArray();

                        var jsonResult = Json(generic.Value, JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                // Recursos.Clases.Css_Log.Guardar(ex.ToString());
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                return null;
            }
        }


        public ActionResult Mantenimiento(int ID_VENTA)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            VentasModelView model = new VentasModelView();
            model.ID_VENTA = ID_VENTA; 
     
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
