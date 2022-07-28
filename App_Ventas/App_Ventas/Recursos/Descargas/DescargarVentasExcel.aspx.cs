using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using Capa_Entidad.Dashboard;
using Capa_Entidad;
using System.IO;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Areas.Dashboard.Repositorio;
using App_Ventas.Areas.Recursiva.Repositorio;

namespace App_Ventas.Recursos.Descargas
{
    public partial class DescargarVentasExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string FECHA_INICIO = Request.QueryString["FECHA_INICIO"].ToString();
                string FECHA_FIN = Request.QueryString["FECHA_FIN"].ToString();
                int ID_SUCURSAL = int.Parse(Request.QueryString["ID_SUCURSAL"].ToString());
                string DESC_SUCURSAL = Request.QueryString["DESC_SUCURSAL"].ToString();
                Descargar_VentasExcel(FECHA_INICIO, FECHA_FIN, ID_SUCURSAL, DESC_SUCURSAL);
            }
        }

        private void Descargar_VentasExcel(string FECHA_INICIO, string FECHA_FIN, int ID_SUCURSAL, string DESC_SUCURSAL)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                List<Cls_Ent_Venta> lista = new List<Cls_Ent_Venta>();
                using (DashboardRepositorio repositorio = new DashboardRepositorio())
                {
                    lista = repositorio.Dashboard_Venta_Listar(new Cls_Ent_Venta {
                        FECHA_INICIO = FECHA_INICIO,
                        FECHA_FIN = FECHA_FIN,
                        ID_SUCURSAL = ID_SUCURSAL 
                    }, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }
                string NOMBRE_ARCHIVO = "Reporte_Ventas.xlsx";
                string NombreHoja = "Reporte_Ventas";
                string CODIGO_TEMPORAL = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                string RUTA_TEMPORAL = Recursos.Clases.Css_Ruta.Ruta_Temporal();
                string RUTA_ARCHIVO_TEMPORAL = string.Format("{0}/{1}", RUTA_TEMPORAL, CODIGO_TEMPORAL + ".xlsx");

                // PLANTILLA 1 productos 
                List<Cls_Ent_Columnas> columnas = new List<Cls_Ent_Columnas>();
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "FECHA", DESCRIPCION_COLUMNA = "Fecha", CELDA_INICIO = "A", CELDA_FIN = "A", INT_CELDAS = 1, AUTO_INCREMENTAR = true });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "TOTAL", DESCRIPCION_COLUMNA = "Total", CELDA_INICIO = "B", CELDA_FIN = "E", INT_CELDAS = 3 });


                Handlers.CreateExcelFile.CreateExcelDocument(lista.ToList(), RUTA_ARCHIVO_TEMPORAL, new Cls_Ent_Titulo { TITULO = "Reporte Ventas", TITULO_CELDA = "F", TITULO_INT = 3 }, false, NombreHoja, columnas);
                byte[] bytes = File.ReadAllBytes(RUTA_ARCHIVO_TEMPORAL);
                if (System.IO.File.Exists(RUTA_ARCHIVO_TEMPORAL))
                    System.IO.File.Delete(RUTA_ARCHIVO_TEMPORAL);
                Response.Clear();
                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", NOMBRE_ARCHIVO.Replace(",", "")));
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytes);
                Response.End();


            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                //Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
        }


    }
}