using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using App_Ventas.Areas.Inventario.Models;
using Capa_Entidad.Inventario;
using Capa_Entidad.Base;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Areas.Inventario.Repositorio;

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

              using (SucursalRepositorio Repositorio = new SucursalRepositorio())
            {
                Cls_Ent_Sucursal entidad = new Cls_Ent_Sucursal();
                entidad.FLG_ESTADO = 1;
                model.Lista_Sucursal = Repositorio.Sucursal_Listar(entidad, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_SUCURSAL,
                    Value = x.ID_SUCURSAL.ToString()
                }).ToList();
                model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "", Text = "--Todos--" });
            }

            

            using (CategoriaRepositorio Repositorio = new CategoriaRepositorio())
            {
                Cls_Ent_Categoria entidad = new Cls_Ent_Categoria();
                entidad.FLG_ESTADO = 1; 
                model.Lista_Categoria = Repositorio.Categoria_Listar(entidad, ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_CATEGORIA,
                    Value = x.ID_CATEGORIA.ToString()
                }).ToList();
                model.Lista_Categoria.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            return View(model);
       
        }

        public ActionResult Producto_ListarxId(Cls_Ent_Producto entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (ProductoRepositorio repositorio = new ProductoRepositorio())
                {
                    auditoria.OBJETO = repositorio.Producto_Listar_Uno(entidad, ref auditoria);
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


        public ActionResult Producto_Listar(Cls_Ent_Producto entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                using (ProductoRepositorio repositorio = new ProductoRepositorio())
                {
                    auditoria.OBJETO = repositorio.Producto_Listar(entidad, ref auditoria);
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


        public ActionResult Mantenimiento(int id, string Accion, int ID_SUCURSAL, string DESC_SUCURSAL)
        {
            
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ProductoModelView model = new ProductoModelView();
         
            model.ID_PRODUCTO = id;
            model.Accion = Accion;
            model.ID_SUCURSAL = ID_SUCURSAL;
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
                using (ProductoRepositorio repositorioProducto = new ProductoRepositorio())
                {
                    Cls_Ent_Producto entidad = new Cls_Ent_Producto();
                    auditoria = new Capa_Entidad.Cls_Ent_Auditoria();

                    Cls_Ent_Producto lista = new Cls_Ent_Producto(); 
                    entidad.ID_PRODUCTO = id;
                    lista = repositorioProducto.Producto_Listar_Uno(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        if (lista.FLG_SERVICIO == 0)
                        {
                            model.ID_PRODUCTO = lista.ID_PRODUCTO;
                            model.COD_PRODUCTO = lista.COD_PRODUCTO;
                            model.DESC_PRODUCTO = lista.DESC_PRODUCTO;
                            model.ID_UNIDAD_MEDIDA = lista.ID_UNIDAD_MEDIDA;
                            model.STOCK = ConvertirUnidadMedida( lista.ID_UNIDAD_MEDIDA , lista.STOCK);
                            model.PRECIO_COMPRA = lista.PRECIO_COMPRA; 
                            model.PRECIO_VENTA = lista.PRECIO_VENTA;
                            model.STOCK_MINIMO = ConvertirUnidadMedida(lista.ID_UNIDAD_MEDIDA, lista.STOCK_MINIMO);
                            model.MARCA = lista.MARCA;
                            model.MODELO = lista.MODELO;
                            model.FLG_VENCE = lista.FLG_VENCE ==1 ? true : false;
                            model.FLG_SERIVICIO = lista.FLG_SERVICIO == 1 ? true : false;
                            model.FECHA_VENCIMIENTO = lista.FECHA_VENCIMIENTO;
                            if(lista.MiArchivo != null)
                                model.MiArchivo = lista.MiArchivo; 

                        }
                        else {

                            model.ID_PRODUCTO = lista.ID_PRODUCTO;
                            model.COD_PRODUCTO_SERVICIO = lista.COD_PRODUCTO;
                            model.DESC_SERVICIO = lista.DESC_PRODUCTO;
                            model.ID_UNIDAD_MEDIDA_SERVICIO = lista.ID_UNIDAD_MEDIDA;
                            model.PRECIO_VENTA_SERVICIO = Convert.ToString(lista.PRECIO_VENTA);
                            model.FLG_SERIVICIO = lista.FLG_SERVICIO == 1 ? true : false;
                            model.DETALLE = lista.DETALLE;
                        }

                    }
                }
            }

            return View(model);
        }

        public ActionResult Producto_Insertar(Cls_Ent_Producto entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (ProductoRepositorio Productorepositorio = new ProductoRepositorio())
            {
                entidad.IP_CREACION = ip_local;

                string ruta_temporal= "";

                if (entidad.MiArchivo != null)
                {
                    string _archivo = entidad.MiArchivo.CODIGO_ARCHIVO + entidad.MiArchivo.EXTENSION;
                    ruta_temporal = Recursos.Clases.Css_Ruta.Ruta_Temporal() + @"" + _archivo;
                    string ruta_Logo = Recursos.Clases.Css_Ruta.Ruta_ImagenProducto() + @"" + _archivo;
                    System.IO.File.Create(ruta_Logo).Close();
                    System.IO.File.WriteAllBytes(ruta_Logo, System.IO.File.ReadAllBytes(ruta_temporal));

                }


                Productorepositorio.Producto_Insertar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
                else { 
                  if (!auditoria.RECHAZAR)
                      {
                          if (System.IO.File.Exists(ruta_temporal))
                              System.IO.File.Delete(ruta_temporal);
                     }
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Producto_Actualizar(Cls_Ent_Producto entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (ProductoRepositorio Productorepositorio = new ProductoRepositorio())
            {

                string ruta_temporal = "";

                if (entidad.MiArchivo != null)
                {
                    string _archivo = entidad.MiArchivo.CODIGO_ARCHIVO + entidad.MiArchivo.EXTENSION;
                    ruta_temporal = Recursos.Clases.Css_Ruta.Ruta_Temporal() + @"" + _archivo;
                    string ruta_Logo = Recursos.Clases.Css_Ruta.Ruta_ImagenProducto() + @"" + _archivo;
                    System.IO.File.Create(ruta_Logo).Close();
                    System.IO.File.WriteAllBytes(ruta_Logo, System.IO.File.ReadAllBytes(ruta_temporal));

                }

                entidad.IP_MODIFICACION = ip_local;
                Productorepositorio.Producto_Actualizar(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
                else
                {
                    if (!auditoria.RECHAZAR)
                    {
                        if (System.IO.File.Exists(ruta_temporal))
                            System.IO.File.Delete(ruta_temporal);
                    }
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Producto_Eliminar(Cls_Ent_Producto entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            using (ProductoRepositorio Productorepositorio = new ProductoRepositorio())
            {
                Productorepositorio.Producto_Eliminar(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Producto_Estado(Cls_Ent_Producto entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            using (ProductoRepositorio Productorepositorio = new ProductoRepositorio())
            {
                entidad.IP_MODIFICACION = ip_local;
                Productorepositorio.Producto_Estado(entidad, ref auditoria);

                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public string ConvertirUnidadMedida(int Id_unida_medida, decimal STOCK)
        {
            string Stock;
            if (Id_unida_medida == 1)
            {
                Stock = Convert.ToString(STOCK / 1000); // valor en kilos cuando sea kilos
            }
            else {
                Stock = Convert.ToString(Convert.ToUInt32(STOCK));
            }
            return Stock; 

        }



    }
}
