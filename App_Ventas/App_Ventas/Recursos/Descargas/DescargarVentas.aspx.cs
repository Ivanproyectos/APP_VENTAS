using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using Capa_Entidad.Ventas;
using Capa_Entidad;
using System.IO;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Areas.Ventas.Repositorio;
using App_Ventas.Areas.Recursiva.Repositorio;

namespace App_Ventas.Recursos.Descargas
{
    public partial class DescargarVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID_SUCURSAL = int.Parse(Request.QueryString["ID_SUCURSAL"].ToString());
                string USU_CREACION = Request.QueryString["ID_USUARIO"].ToString();
                string FECHA_INICIO = Request.QueryString["FECHA_INICIO"].ToString();
                string FECHA_FIN = Request.QueryString["FECHA_FIN"].ToString();
                int FLG_ANULADO = int.Parse(Request.QueryString["FLG_ANULADO"].ToString());
                int TIPO = int.Parse(Request.QueryString["TIPO"].ToString());
                Descargar(ID_SUCURSAL, USU_CREACION, FECHA_INICIO, FECHA_FIN, FLG_ANULADO, TIPO);
            }
        }
        private void Descargar(int ID_SUCURSAL, string USU_CREACION, string FECHA_INICIO, string FECHA_FIN, int FLG_ANULADO, int TIPO)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                List<Cls_Ent_Ventas> lista = new List<Cls_Ent_Ventas>();
                using (VentasRepositorio repositorio = new VentasRepositorio())
                {
                    lista = repositorio.Ventas_Listar(
                      new Cls_Ent_Ventas
                      { 
                        ID_SUCURSAL = ID_SUCURSAL,
                        USU_CREACION = USU_CREACION,
                        FECHA_INICIO = FECHA_INICIO,
                        FECHA_FIN = FECHA_FIN,
                        FLG_ANULADO = FLG_ANULADO
                    }, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }
                Cls_Ent_configurarEmpresa Empresa = new Cls_Ent_configurarEmpresa();
                using (ConfigurarEmpresaRepositorio Repositorio = new ConfigurarEmpresaRepositorio())
                {
                    Empresa = Repositorio.configurarEmpresa_Listar(ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }

                string NOMBRE_ARCHIVO = "";
                string NombreHoja = "";
                string RUTA_LOGO = Recursos.Clases.Css_Ruta.Ruta_Logo() + Empresa.CODIGO_ARCHIVO_LOGO + Empresa.EXTENSION_ARCHIVO_LOGO; 
                string CODIGO_TEMPORAL = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                string RUTA_TEMPORAL = Recursos.Clases.Css_Ruta.Ruta_Temporal();
                string RUTA_ARCHIVO_TEMPORAL = ""; 
                List<Cls_Ent_Columnas> columnas = new List<Cls_Ent_Columnas>();
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "DESC_TIPO_COMPROBANTE", DESCRIPCION_COLUMNA = "Tipo Comprobante" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "COD_COMPROBANTE", DESCRIPCION_COLUMNA = "Nro. Comprobante" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "NOMBRES_APE_CLIENTE", DESCRIPCION_COLUMNA = "Cliente" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "DESCUENTO", DESCRIPCION_COLUMNA = "Descuento" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "SUB_TOTAL", DESCRIPCION_COLUMNA = "Sub Total" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "IGV", DESCRIPCION_COLUMNA = "Igv" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "TOTAL", DESCRIPCION_COLUMNA = "Total" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "DESC_ESTADO_VENTA", DESCRIPCION_COLUMNA = "Estado Venta" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "DESC_TIPO_PAGO", DESCRIPCION_COLUMNA = "Tipo Pago" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "STR_FECHA_VENTA", DESCRIPCION_COLUMNA = "Fecha Venta" });

                if (TIPO == 1)
                { // EXCEL
                    NOMBRE_ARCHIVO = "Ventas.xlsx";
                    NombreHoja = "Ventas";
                    RUTA_ARCHIVO_TEMPORAL=  string.Format("{0}/{1}", RUTA_TEMPORAL, CODIGO_TEMPORAL + ".xlsx");
                    Handlers.CreateExcelFile.CreateExcelDocument(lista.ToList(), RUTA_ARCHIVO_TEMPORAL, null, false, NombreHoja, columnas);
                } 
                else if (TIPO == 2) // PDF
                {
                    NOMBRE_ARCHIVO = "Ventas.pdf";
                    RUTA_ARCHIVO_TEMPORAL = string.Format("{0}/{1}", RUTA_TEMPORAL, CODIGO_TEMPORAL + ".pdf");
                    Handlers.CreatePdfFile.CreatePdfDocument(lista.ToList(), RUTA_ARCHIVO_TEMPORAL, new Cls_Ent_Titulo { TITULO = "Listado Ventas" }, RUTA_LOGO, true, columnas);
                }
                if (RUTA_ARCHIVO_TEMPORAL != "")
                {
                    byte[] bytes = File.ReadAllBytes(RUTA_ARCHIVO_TEMPORAL);
                    if (System.IO.File.Exists(RUTA_ARCHIVO_TEMPORAL))
                        System.IO.File.Delete(RUTA_ARCHIVO_TEMPORAL);
                    Response.Clear();
                    Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", NOMBRE_ARCHIVO.Replace(",", "")));
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(bytes);
                    Response.End();
                }


            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                //Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
        }




    }
}