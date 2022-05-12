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
    public partial class DescargarProductoPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID_SUCURSAL = int.Parse(Request.QueryString["ID_SUCURSAL"].ToString());
                int FLG_SERVICIO = int.Parse(Request.QueryString["FLG_SERVICIO"].ToString());
                DescargarProductosPDF(ID_SUCURSAL, FLG_SERVICIO);
            }
        }


        private void DescargarProductosPDF(int ID_SUCURSAL, int FLG_SERVICIO)
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
                string NOMBRE_ARCHIVO = "Productos.pdf";
                string RUTA_LOGO = Recursos.Clases.Css_Ruta.Ruta_Logo() + Empresa.CODIGO_ARCHIVO_LOGO + Empresa.EXTENSION_ARCHIVO_LOGO; 
                string CODIGO_TEMPORAL = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                string RUTA_TEMPORAL = Recursos.Clases.Css_Ruta.Ruta_Temporal();
                string RUTA_ARCHIVO_TEMPORAL = string.Format("{0}{1}", RUTA_TEMPORAL, CODIGO_TEMPORAL + ".pdf");

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
                // columnas
                List<Cls_Ent_Columnas> columnas = new List<Cls_Ent_Columnas>();
                //foreach (Cls_Ent_Campo campo in ListaCampo)
                //{
                //    columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = campo.COD_CAMPO, DESCRIPCION_COLUMNA = campo.DESCRIPCION_CAMPO });

                //}
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "DESC_PRODUCTO", DESCRIPCION_COLUMNA = "Producto" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "COD_PRODUCTO", DESCRIPCION_COLUMNA = "Código" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "COD_UNIDAD_MEDIDA", DESCRIPCION_COLUMNA = "U. Medida" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "STOCK", DESCRIPCION_COLUMNA = "Stock" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "PRECIO_COMPRA", DESCRIPCION_COLUMNA = "Pre. Compra" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "PRECIO_VENTA", DESCRIPCION_COLUMNA = "Pre. Venta" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "FECHA_VENCIMIENTO", DESCRIPCION_COLUMNA = "Fecha Vencimento" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "MARCA", DESCRIPCION_COLUMNA = "Marca" });
                columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "MODELO", DESCRIPCION_COLUMNA = "Modelo" });

                Handlers.CreatePdfFile.CreatePdfDocument(lista.ToList(), RUTA_ARCHIVO_TEMPORAL, new Cls_Ent_Titulo { TITULO = "Listado Productos" }, RUTA_LOGO,true, columnas);
                this.DescargarArchivo(NOMBRE_ARCHIVO, RUTA_ARCHIVO_TEMPORAL); 

            }
            catch (Exception ex)
            {
                string mensaje = Recursos.Clases.Css_Log.Guardar(ex.Message.ToString()); 
                //Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
        }

        private void DescargarArchivo(string NOMBRE_ARCHIVO, string Path)
        {
            byte[] bytes = File.ReadAllBytes(Path);
            if (System.IO.File.Exists(Path))
                System.IO.File.Delete(Path);
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", NOMBRE_ARCHIVO.Replace(",", "")));
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(bytes);
            Response.End();   
        }


    }
}