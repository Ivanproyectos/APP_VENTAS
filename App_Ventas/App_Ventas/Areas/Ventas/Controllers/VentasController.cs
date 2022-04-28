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
using Microsoft.Reporting.WebForms;
namespace App_Ventas.Areas.Ventas.Controllers
{
    public class VentasController : Controller
    {
        //
        // GET: /Ventas/Ventas/

        public ActionResult Ventas_ClientesXComprobante(string ID_TIPO_COMPROBANTE)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (Listado_CombosRepositorio repositorio = new Listado_CombosRepositorio())
                {
                    auditoria.OBJETO = repositorio.Clientes_ListarXComprobante(ID_TIPO_COMPROBANTE, ref auditoria);
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

        public ActionResult Mantenimiento()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            VentasModelView model = new VentasModelView();

            model.Lista_Cliente = new List<SelectListItem>();
            model.Lista_Cliente.Insert(0, new SelectListItem() { Value = "", Text = "--Selccione--" });

            using (Listado_CombosRepositorio Repositorio = new Listado_CombosRepositorio())
            {
                model.Lista_Tipo_Pago = Repositorio.Tipo_Tipo_Pago_Listar(ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_PAGO ,
                    Value = x.ID_TIPO_PAGO.ToString()
                }).ToList();
                model.Lista_Tipo_Pago.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            using (Listado_CombosRepositorio Repositorio = new Listado_CombosRepositorio())
            {
                model.Lista_Tipo_Comprobante = Repositorio.Tipo_Comprobante_Listar(ref auditoria).Where(e => e.ID_TIPO_COMPROBANTE == "01" || e.ID_TIPO_COMPROBANTE == "03" || e.ID_TIPO_COMPROBANTE == "88").Select(x => new SelectListItem()
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
            //model.Lista_Tipo_Pago = new List<SelectListItem>();
            //model.Lista_Tipo_Pago.Insert(0, new SelectListItem() { Value = "1", Text = "Al Contado" });
            //model.Lista_Tipo_Pago.Insert(1, new SelectListItem() { Value = "2", Text = "Credito" });
            //model.Lista_Tipo_Pago.Insert(2, new SelectListItem() { Value = "3", Text = "Deposito" });


            return View(model);

        }

        [HttpGet]
        public ActionResult View_BuscarProducto(int ID_SUCURSAL, int ID_PRODUCTO, decimal PRECIO, 
            decimal IMPORTE, string _CANTIDAD, string Accion,string TIPO_PROCESO)
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
                            if (lista.ID_UNIDAD_MEDIDA == 1) // convertir gramos a kilos para editar
                            {
                                _CANTIDAD = Convert.ToString(Convert.ToInt32(Convert.ToDecimal(_CANTIDAD) * 1000));
                                //lista.STOCK = (lista.STOCK * 1000);
                            }
                            model.ID_PRODUCTO = ID_PRODUCTO;
                            model.SEARCH_PRODUCTO = lista.DESC_PRODUCTO;
                            model.ID_UNIDAD_MEDIDA = lista.ID_UNIDAD_MEDIDA;
                            model.COD_PRODUCTO = lista.COD_PRODUCTO;
                            model.PRECIO_VENTA = PRECIO;
                            model.STOCK = Convert.ToString(lista.STOCK);
                            model.CANTIDAD = Convert.ToUInt16(_CANTIDAD);
                            model.TOTAL = IMPORTE;
                            model.FLG_SERIVICIO = lista.FLG_SERVICIO;
                            model.COD_UNIDAD_MEDIDA = lista.COD_UNIDAD_MEDIDA == "Kg" ? "Gr." : lista.COD_UNIDAD_MEDIDA;
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
                grid.rows = (grid.rows == 0) ? 100 : grid.rows;
                var @where = (Recursos.Paginacion.Css_Paginacion.GetWhere(grid.SearchFields, grid.searchString, grid.rules));
                if (string.IsNullOrEmpty(@where))
                {
                    @where = @where + " 1=1 ";
                }

                using (VentasRepositorio repositorio = new VentasRepositorio())
                {
                    IList<Cls_Ent_Ventas> lista = repositorio.Ventas_Paginado(grid.sidx, grid.sord, grid.rows, grid.start, @where, ref auditoria);
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

        public ActionResult Ventas_Insertar(Cls_Ent_Ventas entidad)
        {
       

            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            entidad.IP_CREACION = ip_local;
            try{
            using (VentasRepositorio Ventasrepositorio = new VentasRepositorio())
            {
                if (!entidad.FLG_CREDITO_PENDIENTE) // nueva venta 
                {
                    Ventasrepositorio.Ventas_Insertar(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        if (!auditoria.RECHAZAR)
                        {
                            int _ID_VENTA = Convert.ToInt32(auditoria.OBJETO);
                            string _Codigo_Comprobante = Convert.ToString(auditoria.OBJETO2);
                            if (entidad.ListaDetalle != null && entidad.ListaDetalle.Count > 0)
                            {
                                foreach (Cls_Ent_Ventas_Detalle EntidadDet in entidad.ListaDetalle)
                                {
                                    EntidadDet.ID_VENTA = _ID_VENTA;
                                    EntidadDet.USU_CREACION = entidad.USU_CREACION;
                                    EntidadDet.ID_TIPO_COMPROBANTE = entidad.ID_TIPO_COMPROBANTE;
                                    Ventasrepositorio.Ventas_Detalle_Insertar(EntidadDet, ref auditoria);
                                }
                            }
                            else
                            {
                                auditoria.Rechazar("Lista producto no puede estar vacio.");
                            }
                            if (auditoria.EJECUCION_PROCEDIMIENTO)
                            {
                                auditoria.OBJETO = _ID_VENTA;
                                auditoria.OBJETO2 = _Codigo_Comprobante;
                            }
                        }
                    }
                }
                else {  //////////////////////////////////// adicionar a credito axistente del cliente
                    Ventasrepositorio.Ventas_ActualizarVenta_Credito(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        if (!auditoria.RECHAZAR)
                        {
                            int _ID_VENTA = entidad.ID_VENTA_CREDITO;
                            if (entidad.ListaDetalle != null && entidad.ListaDetalle.Count > 0)
                            {
                                foreach (Cls_Ent_Ventas_Detalle EntidadDet in entidad.ListaDetalle)
                                {
                                    EntidadDet.ID_VENTA = _ID_VENTA;
                                    EntidadDet.USU_CREACION = entidad.USU_CREACION;
                                    EntidadDet.ID_TIPO_COMPROBANTE = entidad.ID_TIPO_COMPROBANTE;
                                    Ventasrepositorio.Ventas_Detalle_Insertar(EntidadDet, ref auditoria);
                                }
                            }
                            else
                            {
                                auditoria.Rechazar("Lista producto no puede estar vacio.");
                            }
                            if (auditoria.EJECUCION_PROCEDIMIENTO)
                            {
                                auditoria.OBJETO = _ID_VENTA;
                            }
                        }
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


        public ActionResult Mantenimiento_ViewDetalleProducto(int ID_VENTA, string TIPO)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            VentasModelView model = new VentasModelView();
            model.ID_VENTA = ID_VENTA;
            model.TIPO_GRILLA = TIPO;
            Cls_Ent_Ventas lista = new Cls_Ent_Ventas();
            using (VentasRepositorio repositotioventas = new VentasRepositorio())
            {
                Cls_Ent_Ventas entidad = new Cls_Ent_Ventas();
                auditoria = new Capa_Entidad.Cls_Ent_Auditoria();

                entidad.ID_VENTA = ID_VENTA;
                lista = repositotioventas.Ventas_Listar_Uno(entidad, ref auditoria);
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

        public ActionResult Ventas_ValidarCliente_Credito(Cls_Ent_Ventas entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            try{
                using (VentasRepositorio Ventasrepositorio = new VentasRepositorio())
                {
                    Ventasrepositorio.Ventas_ValidarCliente_Credito(entidad, ref auditoria);
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
