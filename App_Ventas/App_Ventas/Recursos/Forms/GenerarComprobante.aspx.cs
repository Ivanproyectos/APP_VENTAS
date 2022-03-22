using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using App_Ventas.Areas.Ventas.Repositorio;
using Capa_Entidad;
using Capa_Entidad.Ventas; 
using System.Data;
using System.Configuration;
using System.IO;
using System.Drawing.Imaging; 

namespace App_Ventas.Recursos.Forms
{
    public partial class GenerarComprobante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID_VENTA = int.Parse(Request.QueryString["ID_VENTA"].ToString());
                int TIPO_COMPROBANTE = int.Parse(Request.QueryString["TIPO_COMPROBANTE"].ToString());
                GenerarComprobante_PDF(ID_VENTA, TIPO_COMPROBANTE);
            }
        }

        private void GenerarComprobante_PDF(int ID_VENTA, int TIPO_COMPROBANTE)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria(); 
            Cls_Ent_Ventas entidad = new Cls_Ent_Ventas();
            Cls_Ent_Ventas_Detalle entidad2 = new Cls_Ent_Ventas_Detalle();
            entidad.ID_VENTA = ID_VENTA;
            entidad2.ID_VENTA = ID_VENTA;
            Cls_Ent_Ventas ListaCabecera = null;
            List<Cls_Ent_Ventas_Detalle> ListaDetalle = null;
            using (VentasRepositorio repositorio = new VentasRepositorio())
            {
                ListaCabecera = repositorio.Ventas_Listar_Uno(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            using (VentasRepositorio repositorio = new VentasRepositorio())
            {
                ListaDetalle = repositorio.Ventas_Detalleventas_Listar(entidad2, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.LocalReport.DataSources.Clear();
            if (ListaCabecera!= null)
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("rvTicket.rdlc");
                ReportParameter[] parameters = new ReportParameter[0];
                ReportViewer1.LocalReport.SetParameters(parameters);
                //ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("CabeceraCotiazacion", ListaCabecera));
                //ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Detalle", ListaDetalle));
            }
            this.ReportViewer1.LocalReport.Refresh();
            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;
            string format = "PDF";

            byte[] bytes = ReportViewer1.LocalReport.Render(format, null, // deviceinfo not needed for csv
            out mimeType, out encoding, out extension, out streamIds, out warnings);

            Response.Clear();

            if (format == "PDF")
            {
                Response.ContentType = "application/pdf";

                Response.AddHeader("Content-Length", bytes.Length.ToString());
            }
            else if (format == "Excel")
            {
                Response.ContentType = "application/excel";
                Response.AddHeader("Content-disposition", "filename=output.xls");
            }
            Response.OutputStream.Write(bytes, 0, bytes.Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.Flush();
            Response.Close();

        }




    }
}