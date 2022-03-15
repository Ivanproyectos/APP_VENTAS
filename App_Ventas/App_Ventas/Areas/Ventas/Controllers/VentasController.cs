﻿using System;
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
    public class VentasController : Controller
    {
        //
        // GET: /Ventas/Ventas/

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

            return View(model);
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

        public ActionResult Producto_Buscar_Listar(Cls_Ent_Producto entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            //Cls_Ent_Producto entidad = new Cls_Ent_Producto();
            //entidad.DESC_PRODUCTO = DESC_PRODUCTO;
            //entidad.ID_SUCURSAL = ID_SUCURSAL;
            try
            {
                using (ProductoRepositorio repositorio = new ProductoRepositorio())
                {
                    auditoria.OBJETO = repositorio.Producto_Buscar_Listar(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
            }
            return Json(auditoria.OBJETO, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Ventas_Paginado(Recursos.Paginacion.GridTable grid)
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
                            item.SUB_TOTAL.ToString(),
                            item.IGV.ToString(), 
                            item.TOTAL.ToString(),                               
                            item.FLG_ANULADO.ToString(),
                            item.FLG_TIPO_VENTA.ToString(),
                            item.FEC_CREACION.ToString(),
                            item.COD_COMPROBANTE.ToString(),
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

        public ActionResult Ventas_Insertar(Cls_Ent_Ventas entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            try{
            using (VentasRepositorio Ventasrepositorio = new VentasRepositorio())
            {
                entidad.IP_CREACION = ip_local;

                Ventasrepositorio.Ventas_Insertar(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
                else {
                    int _ID_VENTA = Convert.ToInt32(auditoria.OBJETO);
                    string _Codigo_Comprobante = Convert.ToString(auditoria.OBJETO2); 
                    if (entidad.ListaDetalle != null && entidad.ListaDetalle.Count > 0)
                    {                  
                        //Cls_Ent_Ventas_Detalle EntidadDet = new Cls_Ent_Ventas_Detalle(); 
                        foreach ( Cls_Ent_Ventas_Detalle EntidadDet in entidad.ListaDetalle)
                        {
                            EntidadDet.ID_VENTA = _ID_VENTA;
                            EntidadDet.USU_CREACION = entidad.USU_CREACION; 
                            Ventasrepositorio.Ventas_Detalle_Insertar(EntidadDet,ref auditoria); 
                        }
                    }
                    else {
                        auditoria.Rechazar("Lista producto no puede estar vacio."); 
                    }
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        auditoria.OBJETO = _ID_VENTA;
                        auditoria.OBJETO2 = _Codigo_Comprobante; 
                    }
                }
              }
            } catch(Exception ex){
                string CODIGOLOG = Recursos.Clases.Css_Log.Guardar(ex.Message);
                auditoria.Rechazar(CODIGOLOG); 
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ventas_AnularVenta(Cls_Ent_Ventas entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            try{
                using (VentasRepositorio Ventasrepositorio = new VentasRepositorio())
                {
                    entidad.IP_CREACION = ip_local;

                    Ventasrepositorio.Ventas_AnularVenta(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }
            } catch(Exception ex){
                string CODIGOLOG = Recursos.Clases.Css_Log.Guardar(ex.Message);
                auditoria.Rechazar(CODIGOLOG); 
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Mantenimiento_DevolverProducto(int ID_VENTA)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            VentasModelView model = new VentasModelView();
            model.ID_VENTA = ID_VENTA; 
            return View(model);

        }


        public ActionResult ventas_Detalle_Listar(Cls_Ent_Ventas_Detalle entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (VentasRepositorio repositorio = new VentasRepositorio())
                {
                    auditoria.OBJETO = repositorio.Ventas_Detalleventas_Listar(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Ventas_Detalle_DevolverProducto(Cls_Ent_Ventas_Detalle entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            try{
                using (VentasRepositorio Ventasrepositorio = new VentasRepositorio())
                {
                    entidad.IP_CREACION = ip_local;

                    Ventasrepositorio.Ventas_Detalle_DevolverProducto(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }
            } catch(Exception ex){
                string CODIGOLOG = Recursos.Clases.Css_Log.Guardar(ex.Message);
                auditoria.Rechazar(CODIGOLOG); 
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }


        



    }
}
