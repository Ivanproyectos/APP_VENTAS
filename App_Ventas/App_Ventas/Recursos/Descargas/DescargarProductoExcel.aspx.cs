using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using Capa_Entidad.Inventario;
using Capa_Entidad.CargaExcel;
using Capa_Entidad;
using System.IO;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Areas.Inventario.Repositorio;
using App_Ventas.Areas.Recursiva.Repositorio;

namespace App_Ventas.Recursos.Descargas
{
    public partial class DescargarProductoExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID_SUCURSAL = int.Parse(Request.QueryString["ID_SUCURSAL"].ToString());
                int FLG_SERVICIO = int.Parse(Request.QueryString["FLG_SERVICIO"].ToString());
                DescargarProductosExcel(ID_SUCURSAL, FLG_SERVICIO);
            }
        }

        private void DescargarProductosExcel(int ID_SUCURSAL, int FLG_SERVICIO)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                List<Cls_Ent_Producto> lista = new List<Cls_Ent_Producto>();
                using (ProductoRepositorio repositorio = new ProductoRepositorio())
                {
                    lista = repositorio.Producto_Listar(new Cls_Ent_Producto { ID_SUCURSAL = ID_SUCURSAL, FLG_ESTADO = 2, FLG_SERVICIO = FLG_SERVICIO }, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }
                string NOMBRE_ARCHIVO = "Plantilla_Producto.xlsx";
                string NombreHoja = "PlantillaProducto"; 
                string CODIGO_TEMPORAL = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                string RUTA_TEMPORAL = Recursos.Clases.Css_Ruta.Ruta_Temporal();
                string RUTA_ARCHIVO_TEMPORAL = string.Format("{0}/{1}", RUTA_TEMPORAL, CODIGO_TEMPORAL + ".xlsx");

                List<Cls_Ent_Campo> ListaCampo = new List<Cls_Ent_Campo>();
                using (CargaExcelRepostiorio Repositorio = new CargaExcelRepostiorio())
                {
                    ListaCampo = Repositorio.Campo_Listar(ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }

                // PLANTILLA 1 productos 
                List<Cls_Ent_Columnas> columnas = new List<Cls_Ent_Columnas>();
                foreach (Cls_Ent_Campo campo in ListaCampo)
                {
                    columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = campo.COD_CAMPO, DESCRIPCION_COLUMNA = campo.DESCRIPCION_CAMPO });
                }
                Handlers.CreateExcelFile.CreateExcelDocument(lista.ToList(), RUTA_ARCHIVO_TEMPORAL, null, false,NombreHoja, columnas);
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