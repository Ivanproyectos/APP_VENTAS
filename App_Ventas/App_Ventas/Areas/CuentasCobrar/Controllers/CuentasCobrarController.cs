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
using App_Ventas.Areas.CuentasCobrar.Repositorio;
using App_Ventas.Areas.Ventas.Repositorio;
using System.Configuration;
using Microsoft.Reporting.WebForms;

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
            try
            {
                Cls_Ent_SetUpLogin SetUp = (Cls_Ent_SetUpLogin)Session["SetUpLogin"];
                model.ID_SUCURSAL = SetUp.ID_SUCURSAL;
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
                    model.Lista_Sucursal.Insert(0, new SelectListItem() { Value = "0", Text = "--Seleccione--" });
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
            }
            catch (Exception ex)
            {
                Recursos.Clases.Css_Log.Guardar(ex.Message.ToString());
            }
            return View(model);
        }

        public JsonResult CuentasCobrar_Paginado(Recursos.Paginacion.GridTable grid)
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
                Cls_Ent_Ventas lista = new Cls_Ent_Ventas();
               using (VentasRepositorio repositorioCliente = new VentasRepositorio())
                {
                    Cls_Ent_Ventas entidad = new Cls_Ent_Ventas();
                    auditoria = new Capa_Entidad.Cls_Ent_Auditoria();

                    entidad.ID_VENTA = ID_VENTA;
                    lista = repositorioCliente.Ventas_Listar_Uno(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        model.ID_CLIENTE = lista.ID_CLIENTE;
                        model.ID_TIPO_COMPROBANTE = lista.ID_TIPO_COMPROBANTE;
                        model.TOTAL = lista.TOTAL;
                        model.ADELANTO = lista.ADELANTO;
                        model.FECHA_VENTA = lista.FEC_CREACION;
                        model.SUBTOTAL = lista.SUB_TOTAL;
                        model.IGV = lista.IGV;
                        model.DESCUENTO = lista.DESCUENTO;
                        model.DEBE = lista.DEBE;
                        model.DETALLE_VENTA = lista.DETALLE;
                        model.Cliente = lista.Cliente; 
                    }
                }

               model.Lista_Tipo_Pago = new List<SelectListItem>();
               model.Lista_Tipo_Pago.Insert(0, new SelectListItem() { Value = "1", Text = "Al Contado" });
               model.Lista_Tipo_Pago.Insert(1, new SelectListItem() { Value = "3", Text = "Deposito" });


            return View(model);

        }

        public ActionResult CuentasCobrar_Insertar(Cls_Ent_Ventas entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var ip_local = Recursos.Clases.Css_IP.ObtenerIp();
            entidad.IP_CREACION = ip_local;
            try{
                using (CuentasCobrarRepositorio Ventasrepositorio = new CuentasCobrarRepositorio())
                {

                Ventasrepositorio.CuentasCobrar_Insertar(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
                auditoria.OBJETO = entidad.ID_VENTA; 
                }
            } catch(Exception ex){
                string CODIGOLOG = Recursos.Clases.Css_Log.Guardar(ex.Message);
                auditoria.Rechazar(CODIGOLOG); 
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult  CuentasCobrar_NotificarCredito(Cls_Ent_Ventas entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            try{
                byte[] PdfByte; 
                List<Cls_Ent_Ventas> ListaCabecera = new List<Cls_Ent_Ventas>();
                List<Cls_Ent_Cliente> ListaCliente = new List<Cls_Ent_Cliente>();
                Cls_Ent_configurarEmpresa Empresa = null; 
                List<Cls_Ent_Ventas_Detalle> ListaDetalle = null;
                using (ConfigurarEmpresaRepositorio repositorio = new ConfigurarEmpresaRepositorio())
                {
                    Empresa = repositorio.configurarEmpresa_Listar(ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }
                using (VentasRepositorio repositorio = new VentasRepositorio())
                {
                    Cls_Ent_Ventas ListaVenta = repositorio.Ventas_Listar_Uno(entidad, ref auditoria);
                    ListaCabecera.Add(ListaVenta);
                    ListaCliente.Add(ListaVenta.Cliente);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }

                using (VentasRepositorio repositorio = new VentasRepositorio())
                {
                    ListaDetalle = repositorio.Ventas_Detalleventas_Listar(new Cls_Ent_Ventas_Detalle {ID_VENTA = entidad.ID_VENTA}, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }

               using (VentasRepositorio Ventasrepositorio = new VentasRepositorio())
                {
                    int Total_items = 0; 
                    string RutaLogo = Recursos.Clases.Css_Ruta.Ruta_Logo() + Empresa.CODIGO_ARCHIVO_LOGO + Empresa.EXTENSION_ARCHIVO_LOGO; 
                    using (var viewer = new LocalReport())
                    {
                        byte[] ImagenBytes = Recursos.Clases.Css_Convertir.FileToByteArray(RutaLogo); //convertir imagen bytes
                        string strB64 = Convert.ToBase64String(ImagenBytes); // convertir bytes en base64
                        viewer.ReportPath = Recursos.Clases.Css_Ruta.Ruta_Reporting() + "ComprobanteA4.rdlc";
                        ReportParameter[] parameters = new ReportParameter[9];
                        parameters[0] = new ReportParameter("RutaLogo", strB64);
                        parameters[1] = new ReportParameter("Razon_social", Empresa.RAZON_SOCIAL);
                        parameters[2] = new ReportParameter("Ruc", Empresa.RUC);
                        parameters[3] = new ReportParameter("Telefono", Empresa.TELEFONO);
                        parameters[4] = new ReportParameter("Direccion", Empresa.DIRECCION_FISCAL);
                        parameters[5] = new ReportParameter("Ubigeo", Empresa.DESC_UBIGEO);
                        parameters[6] = new ReportParameter("Venta_TotalEnLetras", Recursos.Clases.Css_Convertir.NumeroEnletras(ListaCabecera[0].TOTAL.ToString()));
                        parameters[7] = new ReportParameter("Igv", Empresa.NOMBRE_IMPUESTO + "(" + Convert.ToInt32(Empresa.IMPUESTO).ToString() + "%)");
                        parameters[8] = new ReportParameter("SimboloMoneda", Empresa.SIMBOLO_MONEDA);
                        viewer.SetParameters(parameters);
                        viewer.ReportPath = Recursos.Clases.Css_Ruta.Ruta_Reporting() + "ComprobanteA4.rdlc";
                        viewer.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Ds_Cliente", ListaCliente));
                        viewer.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Ds_Cabecera", ListaCabecera));
                        viewer.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Ds_DetalleVenta", ListaDetalle));
                         PdfByte = viewer.Render("PDF");
                    }
                    Total_items = ListaDetalle.Count(); 
                   //Cls_Ent_Ventas VentaUno = Ventasrepositorio.Ventas_Listar_Uno(entidad, ref auditoria);
                    string BodyCorreo = Recursos.Clases.Css_GenerarPlantilla.PlantillaCorreo_NotificarCredito(ListaCabecera[0], Empresa, Total_items);
                   Recursos.Clases.Css_Mail.MailHelper.SendMailMessage(ref auditoria, ListaCliente[0].CORREO, "", "", "Noticación credito pendiente",
                                                                        BodyCorreo, "Comprobante.pdf", "", PdfByte, RutaLogo, ConfigurationManager.AppSettings["CorreoEnvio"], Empresa.RAZON_SOCIAL);
                }
            } catch(Exception ex){
                string CODIGOLOG = Recursos.Clases.Css_Log.Guardar(ex.Message);
                auditoria.Rechazar(CODIGOLOG); 
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

    }
}
