using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using App_Ventas.Areas.Inventario.Models;
using App_Ventas.Areas.Ventas.Models;
using Capa_Entidad.Inventario;
using Capa_Entidad.Base;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Areas.Inventario.Repositorio;
namespace App_Ventas.Areas.Inventario.Controllers
{
    public class Translado_ProductoController : Controller
    {
        //
        // GET: /Inventario/Translado_Producto/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View_Translados()
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            TransladoModelView model = new TransladoModelView();
            using (SucursalRepositorio Repositorio = new SucursalRepositorio())
            {
                model.Lista_Sucursal = Repositorio.Sucursal_Listar(new Cls_Ent_Sucursal { FLG_ESTADO = 1 }, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_SUCURSAL,
                    Value = x.ID_SUCURSAL.ToString()
                }).ToList();
                model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult View_BuscarProducto(int ID_PRODUCTO, string _CANTIDAD, string Accion, string DESC_SUCURSAL)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            BuscarProductoModelView model = new BuscarProductoModelView();
            try
            {
                model.ID_PRODUCTO = ID_PRODUCTO;
                model.Accion = Accion;
                model.DESC_SUCURSAL = DESC_SUCURSAL; 
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
                            _Stock = Convert.ToString(lista.STOCK);
                            if (lista.ID_UNIDAD_MEDIDA == 1) // convertir gramos a kilos para editar
                            {
                                _Stock = Convert.ToString(Convert.ToDecimal(lista.STOCK) / 1000); // gramos a kilos
                            }
                            model.ID_PRODUCTO = ID_PRODUCTO;
                            model.SEARCH_PRODUCTO = lista.DESC_PRODUCTO;
                            model.ID_UNIDAD_MEDIDA = lista.ID_UNIDAD_MEDIDA;
                            model.COD_PRODUCTO = lista.COD_PRODUCTO;
                            model.STOCK = _Stock;
                            model.CANTIDAD = Convert.ToUInt16(_CANTIDAD);
                            model.FLG_SERIVICIO = lista.FLG_SERVICIO;
                            model.COD_UNIDAD_MEDIDA = lista.COD_UNIDAD_MEDIDA;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string Mensaje = Recursos.Clases.Css_Log.Guardar(ex.Message);
                auditoria.Rechazar(Mensaje);
            }

            return View(model);
        }

        public ActionResult Producto_Translado_Insertar(Cls_Ent_Translado_Producto entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            entidad.IP_CREACION = ip_local;
            try
            {
                using (Translado_ProductoRepositorio repositorio = new Translado_ProductoRepositorio())
                {

                    repositorio.Producto_Translado_Insertar(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        if (!auditoria.RECHAZAR)
                        {
                            int _ID_TRANSLADO = Convert.ToInt32(auditoria.OBJETO);
                            if (entidad.ListaDetalle != null && entidad.ListaDetalle.Count > 0)
                            {
                                foreach (Cls_Ent_Translado_Producto EntidadDet in entidad.ListaDetalle)
                                {
                                    EntidadDet.ID_TRANSLADO = _ID_TRANSLADO;
                                    EntidadDet.ID_SUCURSAL_DESTINO = entidad.ID_SUCURSAL_DESTINO;
                                    repositorio.Producto_Translado_Detalle_Insertar(EntidadDet, ref auditoria);
                                }
                            }
                            else
                            {
                                auditoria.Rechazar("Lista producto no puede estar vacio.");
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


    }
}
