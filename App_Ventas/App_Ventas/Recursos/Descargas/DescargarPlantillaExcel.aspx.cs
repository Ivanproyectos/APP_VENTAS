﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using Capa_Entidad.Inventario;
using Capa_Entidad; 
using System.IO;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Areas.Inventario.Repositorio;

namespace App_Ventas.Recursos.Descargas
{
    public partial class DescargarPlantillaExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DescargarExcel();
        }
        private void DescargarExcel()
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {

                List<Cls_Ent_Unidad_Medida> List_Unidad = new List<Cls_Ent_Unidad_Medida>(); 
                using (Listado_CombosRepositorio Repositorio = new Listado_CombosRepositorio())
                {
                    List_Unidad = Repositorio.Unidad_Medida_Listar(ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                }


                List<Cls_Ent_Categoria> List_Categoria = new List<Cls_Ent_Categoria>();
                using (CategoriaRepositorio Repositorio = new CategoriaRepositorio())
                {
                    List_Categoria = Repositorio.Categoria_Listar(new Cls_Ent_Categoria{ FLG_ESTADO = 1},ref auditoria);
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

                string NOMBRE_ARCHIVO = "Plantilla_Producto.xlsx";
                //string ruta_adjunto = Recursos.Clases.Css_Ruta.Ruta_PlantillaExcel() + NOMBRE_ARCHIVO;
                string RUTA_LOGO = Recursos.Clases.Css_Ruta.Ruta_Logo() + @"\" + Empresa.CODIGO_ARCHIVO_LOGO + Empresa.EXTENSION_ARCHIVO_LOGO; 
                string CODIGO_TEMPORAL = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                string RUTA_TEMPORAL = Recursos.Clases.Css_Ruta.Ruta_Temporal();
                string RUTA_ARCHIVO_TEMPORAL = string.Format("{0}/{1}", RUTA_TEMPORAL, CODIGO_TEMPORAL + ".xlsx");

                //List<Cls_Ent_Columnas> columnas = new List<Cls_Ent_Columnas>();
                //columnas.Add(new Cls_Ent_Columnas { ID_COLUMNA = "COD_PRODUCTO", DESCRIPCION_COLUMNA = "COD_PRODUCTO" });
                //Handlers.CreateExcelFile.CreateExcelDocument(new List<Cls_Ent_Columnas>(), RUTA_ARCHIVO_TEMPORAL, null, false, columnas);


                 List<Cls_Ent_MultiSheets> ListaHojas = new List<Cls_Ent_MultiSheets>(); 

           

                // PLANTILLA 1
                 Cls_Ent_MultiSheets<Cls_Ent_Unidad_Medida> Sheet1 = new Cls_Ent_MultiSheets<Cls_Ent_Unidad_Medida>();
                Sheet1.COLUMNS.Add(new Cls_Ent_Columnas { ID_COLUMNA = "COD_PRODUCTO", DESCRIPCION_COLUMNA = "Código" });
                Sheet1.COLUMNS.Add(new Cls_Ent_Columnas { ID_COLUMNA = "DESC_PRODUCTO", DESCRIPCION_COLUMNA = "Producto" });
                Sheet1.ONLYCOLUMN = true;
                Sheet1.NAME_SHEET = "PlantillaProducto";
                Sheet1.ORDEN_INDEX = 2;
                Sheet1.dt = Handlers.CreateExcelFileMS.ListToDataTableMS<Cls_Ent_Columnas>(new List<Cls_Ent_Columnas>(), Sheet1.ONLYCOLUMN, Sheet1.COLUMNS);
                ListaHojas.Add(Sheet1);

                // PLANTILLA 2
                Cls_Ent_MultiSheets Sheet2 = new Cls_Ent_MultiSheets();
                Sheet2.COLUMNS.Add(new Cls_Ent_Columnas { ID_COLUMNA = "ID_UNIDAD_MEDIDA", DESCRIPCION_COLUMNA = "Código" });
                Sheet2.COLUMNS.Add(new Cls_Ent_Columnas { ID_COLUMNA = "DESC_UNIDAD_MEDIDA", DESCRIPCION_COLUMNA = "Descripción" });
                Sheet2.ONLYCOLUMN = false;
                Sheet2.NAME_SHEET = "UnidadMedida";
                Sheet2.ORDEN_INDEX = 1;
                Sheet2.dt = Handlers.CreateExcelFileMS.ListToDataTableMS<Cls_Ent_Unidad_Medida>(List_Unidad, Sheet2.ONLYCOLUMN, Sheet2.COLUMNS);
                ListaHojas.Add(Sheet2);

                // PLANTILLA 3
                Cls_Ent_MultiSheets Sheet3 = new Cls_Ent_MultiSheets();
                Sheet3.COLUMNS.Add(new Cls_Ent_Columnas { ID_COLUMNA = "ID_CATEGORIA", DESCRIPCION_COLUMNA = "Código" });
                Sheet3.COLUMNS.Add(new Cls_Ent_Columnas { ID_COLUMNA = "DESC_CATEGORIA", DESCRIPCION_COLUMNA = "Categoria" });
                Sheet3.COLUMNS.Add(new Cls_Ent_Columnas { ID_COLUMNA = "DESCRIPCION", DESCRIPCION_COLUMNA = "Descripción" });
                Sheet3.ONLYCOLUMN = false;
                Sheet3.NAME_SHEET = "Categorias";
                Sheet3.ORDEN_INDEX = 1;
                Sheet3.dt = Handlers.CreateExcelFileMS.ListToDataTableMS<Cls_Ent_Categoria>(List_Categoria, Sheet2.ONLYCOLUMN, Sheet3.COLUMNS);
                ListaHojas.Add(Sheet2);


                Handlers.CreateExcelFileMS.CreateExcelDocumentMS(ListaHojas, RUTA_ARCHIVO_TEMPORAL, RUTA_LOGO);
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