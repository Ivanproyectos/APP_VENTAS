using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_Ventas.Areas.Compras.Models;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using Capa_Entidad.Inventario;
using Capa_Entidad.Compras;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Recursos;
using App_Ventas.Areas.Inventario.Repositorio;
using App_Ventas.Areas.Ventas.Models;
using App_Ventas.Areas.Compras.Repositorio;

namespace App_Ventas.Areas.Compras.Controllers
{
    public class ComprasController : Controller
    {
        //
        // GET: /Compras/Compras/

        public ActionResult Index()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ComprasModelView model = new ComprasModelView();
            try
            {
                Cls_Ent_SetUpLogin SetUp = (Cls_Ent_SetUpLogin)Session["SetUpLogin"];
                model.ID_SUCURSAL = SetUp.ID_SUCURSAL;
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

                using (ProveedorRepositorio Repositorio = new ProveedorRepositorio())
                {
                    model.Lista_Proveedor = Repositorio.Proveedor_Listar(new Cls_Ent_Proveedor { FLG_ESTADO = 1 }, ref auditoria).Select(x => new SelectListItem()
                    {
                        Text = x.NOMBRES_APE + " Nro Doc: " + x.NUMERO_DOCUMENTO,
                        Value = x.ID_PROVEEDOR.ToString()
                    }).ToList();
                    model.Lista_Proveedor.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                        model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "-- Error al cargar opciones --" });
                    }

                }
            }
            catch (Exception ex)
            {
                Recursos.Clases.Css_Log.Guardar(ex.Message.ToString());
            }
            return View(model);
        }

        public ActionResult Mantenimiento(int ID_SUCURSAL, string DESC_SUCURSAL)
        {

            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ComprasModelView model = new ComprasModelView();
            model.ID_SUCURSAL = ID_SUCURSAL;
            model.DESC_SUCURSAL = DESC_SUCURSAL; 

            using (ProveedorRepositorio Repositorio = new ProveedorRepositorio())
            {

                model.Lista_Proveedor = Repositorio.Proveedor_Listar(new Cls_Ent_Proveedor { FLG_ESTADO = 1 }, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.NOMBRES_APE + " Nro. Doc: " + x.NUMERO_DOCUMENTO,
                    Value = x.ID_PROVEEDOR.ToString()
                }).ToList();
                model.Lista_Proveedor.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    model.Lista_Proveedor.Insert(0, new SelectListItem() { Value = "", Text = "-- Error al cargar opciones --" });
                }

            }

            using (Listado_CombosRepositorio Repositorio = new Listado_CombosRepositorio())
            {
                model.Lista_Tipo_Comprobante = Repositorio.Tipo_Comprobante_Listar(ref auditoria).Where(e => e.ID_TIPO_COMPROBANTE == "01" || e.ID_TIPO_COMPROBANTE == "03" ).Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_COMPROBANTE,
                    Value = x.ID_TIPO_COMPROBANTE.ToString()
                }).ToList();
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    model.Lista_Tipo_Comprobante.Insert(0, new SelectListItem() { Value = "", Text = "--Error al cargar opciones--" });
                }

            }

            using (Listado_CombosRepositorio Repositorio = new Listado_CombosRepositorio())
            {
                model.Lista_Tipo_Pago = Repositorio.Tipo_Tipo_Pago_Listar(ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_PAGO,
                    Value = x.ID_TIPO_PAGO.ToString()
                }).ToList();
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    model.Lista_Tipo_Comprobante.Insert(0, new SelectListItem() { Value = "", Text = "--Error al cargar opciones--" });
                }
            }


            return View(model);

        }

        [HttpGet]
        public ActionResult View_BuscarProducto(int ID_SUCURSAL, int ID_PRODUCTO, decimal PRECIO,
            decimal IMPORTE, string _CANTIDAD, string Accion, string TIPO_PROCESO)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            BuscarProductoModelView model = new BuscarProductoModelView();
            try
            {
                model.ID_SUCURSAL = ID_SUCURSAL;
                model.ID_PRODUCTO = ID_PRODUCTO;
                model.Accion = Accion;
                model.TIPO_PROCESO = TIPO_PROCESO;
                using (Listado_CombosRepositorio Repositorio = new Listado_CombosRepositorio())
                {
                    model.Lista_Unidad_Medida = Repositorio.Unidad_Medida_Listar(ref auditoria).Select(x => new SelectListItem()
                    {
                        Text = x.DESC_UNIDAD_MEDIDA,
                        Value = x.ID_UNIDAD_MEDIDA.ToString()
                    }).ToList();
                    model.Lista_Unidad_Medida.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
                }
                if (Accion == "M")
                {
                    Cls_Ent_Producto lista = new Cls_Ent_Producto();
                    using (ProductoRepositorio repositorioCliente = new ProductoRepositorio())
                    {
                        Cls_Ent_Producto entidad = new Cls_Ent_Producto();
                        auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
                        string _Stock; 
                        entidad.ID_PRODUCTO = ID_PRODUCTO;
                        lista = repositorioCliente.Producto_Listar_Uno(entidad, ref auditoria);
                        if (!auditoria.EJECUCION_PROCEDIMIENTO)
                        {
                            string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                            auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                        }
                        else
                        {
                            //string _CANTIDAD = CANTIDAD; 
                            _Stock =  Convert.ToString(lista.STOCK); 
                            if (lista.ID_UNIDAD_MEDIDA == 1) // convertir gramos a kilos para editar
                            {
                                _Stock = Convert.ToString(Convert.ToDecimal(lista.STOCK)/1000); // gramos a kilos
                            }
                            model.ID_PRODUCTO = ID_PRODUCTO;
                            model.SEARCH_PRODUCTO = lista.DESC_PRODUCTO;
                            model.ID_UNIDAD_MEDIDA = lista.ID_UNIDAD_MEDIDA;
                            model.COD_PRODUCTO = lista.COD_PRODUCTO;
                            model.PRECIO_VENTA = PRECIO;
                            model.STOCK = _Stock;
                            model.CANTIDAD = Convert.ToUInt16(_CANTIDAD);
                            model.TOTAL = IMPORTE;
                            model.FLG_SERVICIO = lista.FLG_SERVICIO;
                            model.COD_UNIDAD_MEDIDA = lista.COD_UNIDAD_MEDIDA ;
                        }
                    }

                }
            }
            catch (Exception ex) {
                string Mensaje = Recursos.Clases.Css_Log.Guardar(ex.Message);
                auditoria.Rechazar(Mensaje); 
            }

            return View(model);
        }



        public JsonResult Compras_Paginado(Recursos.Paginacion.GridTable grid)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                grid.rows = (grid.rows == 0) ? 100 : grid.rows;
                var @where = (Recursos.Paginacion.Css_Paginacion.GetWhere(grid.SearchFields, grid.searchString, grid.rules));
                if (string.IsNullOrEmpty(@where))
                {
                    @where = @where + " 1=1 ";
                }

                using (ComprasRepositorio repositorio = new ComprasRepositorio())
                {
                    IList<Cls_Ent_Compras> lista = repositorio.Compras_Paginado(grid.sidx, grid.sord, grid.rows, grid.start, @where, ref auditoria);
                    if (auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        var generic = Recursos.Paginacion.Css_Paginacion.BuscarPaginador(grid.draw, (int)auditoria.OBJETO, lista);
                        generic.Value.data = generic.List;
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
                Recursos.Clases.Css_Log.Guardar(ex.ToString());
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                return null;
            }

        }



        public ActionResult Compras_Insertar(Cls_Ent_Compras entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            entidad.IP_CREACION = ip_local;
            try
            {
                using (ComprasRepositorio Comprasrepositorio = new ComprasRepositorio())
                {
              
                        Comprasrepositorio.Compras_Insertar(entidad, ref auditoria);
                        if (!auditoria.EJECUCION_PROCEDIMIENTO)
                        {
                            string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                            auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                        }
                        else
                        {
                            if (!auditoria.RECHAZAR)
                            {
                                int _ID_COMPRA = Convert.ToInt32(auditoria.OBJETO);
                                string _Codigo_Comprobante = Convert.ToString(auditoria.OBJETO2);
                                if (entidad.ListaDetalle != null && entidad.ListaDetalle.Count > 0)
                                {
                                    foreach (Cls_Ent_Compras_Detalle EntidadDet in entidad.ListaDetalle)
                                    {
                                        EntidadDet.ID_COMPRA = _ID_COMPRA;
                                        EntidadDet.USU_CREACION = entidad.USU_CREACION;
                                        Comprasrepositorio.Compras_Detalle_Insertar(EntidadDet, ref auditoria);
                                    }
                                }
                                else
                                {
                                    auditoria.Rechazar("Lista producto no puede estar vacio.");
                                }
                                if (auditoria.EJECUCION_PROCEDIMIENTO)
                                {
                                    auditoria.OBJETO = _ID_COMPRA;
                                    auditoria.OBJETO2 = _Codigo_Comprobante;
                                }
                            }
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

        public ActionResult Compras_AnularVenta(Cls_Ent_Compras entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            try
            {
                using (ComprasRepositorio Comprasrepositorio = new ComprasRepositorio())
                {
                    entidad.IP_CREACION = ip_local;

                    Comprasrepositorio.Compras_AnularVenta(entidad, ref auditoria);
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


        public ActionResult Compras_Detalle_Listar(Cls_Ent_Compras_Detalle entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (ComprasRepositorio repositorio = new ComprasRepositorio())
                {
                    auditoria.OBJETO = repositorio.Compras_Detallecompras_Listar(entidad, ref auditoria);
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

        public ActionResult View_DetalleCompra(int ID_COMPRA, string TIPO)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ComprasModelView model = new ComprasModelView();
            model.ID_COMPRA = ID_COMPRA;
            model.TIPO_GRILLA = TIPO;
            Cls_Ent_Compras lista = new Cls_Ent_Compras();
            using (ComprasRepositorio repositotioventas = new ComprasRepositorio())
            {
                Cls_Ent_Compras entidad = new Cls_Ent_Compras();
                auditoria = new Capa_Entidad.Cls_Ent_Auditoria();

                entidad.ID_COMPRA = ID_COMPRA;
                lista = repositotioventas.Compras_Listar_Uno(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
                else
                {
                    model.TOTAL = lista.TOTAL;
                    model.SUBTOTAL = lista.SUB_TOTAL;
                    model.IGV = lista.IGV;
                    model.DESCUENTO = lista.DESCUENTO;
                }
            }

            return View(model);

        }
 
    


    }
}
