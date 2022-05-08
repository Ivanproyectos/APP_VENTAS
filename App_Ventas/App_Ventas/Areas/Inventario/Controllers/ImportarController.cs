using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Entidad.Base; 
using Capa_Entidad.Administracion;
using Capa_Negocio.Inventario;
using Capa_Negocio.Listados_Combos; 
using App_Ventas.Areas.Inventario.Models;
using App_Ventas.Areas.Inventario.Repositorio;
using App_Ventas.Areas.Ventas.Models;
using Capa_Entidad.Inventario;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet; 


namespace App_Ventas.Areas.Inventario.Controllers
{
    public class ImportarController : Controller
    {
        //
        // GET: /Inventario/Importar_Producto/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult View_Importar(int ID_SUCURSAL, string DESC_SUCURSAL)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            BuscarProductoModelView model = new BuscarProductoModelView();
            model.ID_SUCURSAL = ID_SUCURSAL;
            model.DESC_SUCURSAL = DESC_SUCURSAL;
            return View(model);
        }
        public ActionResult Importar_DescagarPlantilla()
        {
            return View();
        }
        public ActionResult Importar_CargarExcel(HttpPostedFileBase fileArchivo, FormCollection forms)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            if (fileArchivo != null)
            {
                int ID_SUCURSAL = int.Parse(forms["IMP_ID_SUCURSAL"].ToString());
                var content = new byte[fileArchivo.ContentLength];
                fileArchivo.InputStream.Read(content, 0, fileArchivo.ContentLength);
                string CODIGO_UNICO = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                string Nombreencriptado = CODIGO_UNICO + System.IO.Path.GetExtension(fileArchivo.FileName).ToString();
                Recursos.Clases.Css_Ruta.MisRuta MisRutas = new Recursos.Clases.Css_Ruta.MisRuta();
                MisRutas = Recursos.Clases.Css_Ruta.Ruta_TemporalI();
                var ruta_link = @"/" + MisRutas.RUTA + Nombreencriptado;
                string ruta = MisRutas.RUTA_COMPLETA + Nombreencriptado;
                fileArchivo.SaveAs(ruta);
                ProcesarHojaCalculo(ruta, ID_SUCURSAL, ref auditoria); 

            }
            else
            {
                auditoria.Rechazar("No se encontró ningun archivo, seleccione alguno");
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }
        private void ProcesarHojaCalculo(string rutaBase, int ID_SUCURSAL, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rutaBase) == true)
                {
                    auditoria.Rechazar("No se encontró ningun archivo, seleccione alguno");
                }
                if(!auditoria.RECHAZAR){
                List<Cls_Ent_Producto> items = new List<Cls_Ent_Producto>();
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(rutaBase, true))
                {
                    var user = HttpContext.Request.Cookies["MEF-CODUSER-MIGUEL"];
                    Workbook libro = document.WorkbookPart.Workbook;
                    IEnumerable<Sheet> hojas = libro.Descendants<Sheet>();
                    string hojaId = hojas.First(s => s.LocalName == @"sheet").Id;
                    WorksheetPart hoja = (WorksheetPart)document.WorkbookPart.GetPartById(hojaId);
                    SharedStringTable tabla = document.WorkbookPart.SharedStringTablePart.SharedStringTable;
                    ValidarCarga(hoja.Worksheet, tabla,ID_SUCURSAL, ref auditoria);
                    if (!auditoria.RECHAZAR) {  //  validar carga correcto
                        items = ObtenerRegistros(hoja.Worksheet, tabla, ref auditoria);
                        if (items.Count > 0)
                        {
                            foreach (Cls_Ent_Producto entidad in items)
                            {
                                using (ProductoRepositorio Productorepositorio = new ProductoRepositorio())
                                {
                                    entidad.ID_SUCURSAL = ID_SUCURSAL;
                                    entidad.USU_CREACION = "iperez";
                                    Productorepositorio.Producto_Insertar(entidad, ref auditoria);
                                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                                    {
                                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                                        break;
                                    }
                                }
                            }
                            auditoria.OBJETO = items.Count(); 
                        }
                        else {
                            auditoria.Rechazar("No se encontre registros para procesar."); 
                        }
                    }
                  }
                }
            }
            catch (Exception ex)
            {
                string mensaje = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                auditoria.Rechazar(mensaje);
            }
            finally
            {
                if (string.IsNullOrWhiteSpace(rutaBase) == false)
                {
                    System.IO.File.Delete(rutaBase);
                }

            }

        }
        public List<Cls_Ent_Producto> ObtenerRegistros(Worksheet hoja, SharedStringTable tabla, ref Cls_Ent_Auditoria auditoria)
        {
                    List<Cls_Ent_Producto> ListaProductos = new List<Cls_Ent_Producto>();
                Cls_Ent_Producto item = new Cls_Ent_Producto(); 
            try
            {
                IEnumerable<Row> registros = this.GetRowsGreaterEqualThan(hoja, 2);
                foreach (Row registro in registros)
                {                 
                    String[] valores = GetRowValue(tabla, registro);
                    if (valores.Length == 0) continue;
                        if (!auditoria.RECHAZAR)
                        {
                             item.COD_PRODUCTO = Convert.ToString(valores[0]);                   
                             item.DESC_PRODUCTO = Convert.ToString(valores[1]);
                             item.ID_CATEGORIA =  Convert.ToInt32(valores[2]);
                             item.ID_UNIDAD_MEDIDA = Convert.ToInt32(valores[3]);
                             item.PRECIO_COMPRA = Convert.ToDecimal(valores[4]);
                             item.PRECIO_VENTA = Convert.ToDecimal(valores[5]);
                             item.STOCK = Convert.ToInt32(valores[6]);
                             item.STOCK_MINIMO = Convert.ToInt32(valores[7]);
                             item.FLG_SERVICIO = Convert.ToInt32(valores[8]);
                             if (valores.Length == 10)
                             {
                                 item.FLG_VENCE = 1;
                                 item.FECHA_VENCIMIENTO = Convert.ToString(valores[9]);
                             }
                            if (valores.Length == 11)
                                item.MARCA = Convert.ToString(valores[10]);
                            if (valores.Length == 12)
                                item.MODELO = Convert.ToString(valores[11]);
                            if (valores.Length == 13)
                                item.DETALLE = Convert.ToString(valores[12]);
                            ListaProductos.Add(item); 
                        }   
                }
  
            }
            catch (Exception ex)
            {
                string mensaje = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                auditoria.Rechazar(mensaje);
            }

            return ListaProductos; 
        }
        public void ValidarCarga(Worksheet hoja, SharedStringTable tabla,int ID_SUCURSAL, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                IEnumerable<Row> registros = this.GetRowsGreaterEqualThan(hoja, 2);
                List<Cls_Ent_ErroresExcel> ListaErrores = new List<Cls_Ent_ErroresExcel>();
                Cls_Ent_ErroresExcel errores = null; 
                List<Cls_Ent_Unidad_Medida> ListaUnidadMedida = new Cls_Rule_Listados().Unidad_Medida_Listar(ref auditoria);
                List<Cls_Ent_Categoria> ListaCategoria = new Cls_Rule_Categoria().Categoria_Listar(new Cls_Ent_Categoria { FLG_ESTADO = 1 }, ref auditoria);
                int filaExcel = 2; // primara fila de registros
                bool ColumIncomplete = false; 
                foreach (Row registro in registros)
                {             
                    String[] valores = GetRowValue(tabla, registro);
                    if (valores.Length == 0) continue;
                    if (!(valores.Length >= 9)) // numero columnas requeridas
                        {
                            ColumIncomplete = true; 
                            auditoria.Rechazar("El archivo cargado no tiene el número de columnas requeridas.");
                        }
                        if (!auditoria.RECHAZAR)
                        {
                            string COD_PRODUCTO = Convert.ToString(valores[0]);
                            if (!string.IsNullOrEmpty(COD_PRODUCTO))
                            {
                                COD_PRODUCTO = COD_PRODUCTO.Trim();
                                var Objeto_ExitCodigo = new Cls_Rule_Producto().Producto_Buscar_Listar(new Cls_Ent_Producto { 
                                                                                                      ID_SUCURSAL = ID_SUCURSAL,
                                                                                                       COD_PRODUCTO = COD_PRODUCTO
                                                                                                        }, ref auditoria);
                                if (Objeto_ExitCodigo.Count > 0) {
                                    ListaErrores.Add(this.ErrorEcxel(filaExcel, "[COD_PRODUCTO] " + COD_PRODUCTO + " ya se encuentra registrado."));
                                }
                            }
                            else {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[COD_PRODUCTO] es obligatorio")); 
                            }           
                            string DESC_PRODUCTO = Convert.ToString(valores[1]);
                            if (!string.IsNullOrEmpty(DESC_PRODUCTO))
                            {
                                DESC_PRODUCTO = DESC_PRODUCTO.Trim();
                            }
                            else
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[DESC_PRODUCTO] es obligatorio")); 
                            }
                            string ID_CATEROGIRA = Convert.ToString(valores[2]);
                            if (Recursos.Clases.Css_Validators.ValidarNumber(ID_CATEROGIRA))
                            {
                                var Objeto_Categoria = ListaCategoria.Where(p => p.ID_CATEGORIA == int.Parse(ID_CATEROGIRA)).FirstOrDefault();
                                if (Objeto_Categoria == null)
                                {
                                    ListaErrores.Add(this.ErrorEcxel(filaExcel, "[ID_CATEGORIA] " + ID_CATEROGIRA + " no se encuentra registrado en el Sistema.")); 
                                }            
                            }
                            else {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[ID_CATEGORIA] debe ser de tipo numerico")); 
                            }
                           
                            string ID_UNIDAD_MEDIDA = Convert.ToString(valores[3]);
                            if (Recursos.Clases.Css_Validators.ValidarNumber(ID_UNIDAD_MEDIDA))
                            {
                                var Objeto_UnidadMedida = ListaUnidadMedida.Where(p => p.ID_UNIDAD_MEDIDA == int.Parse(ID_UNIDAD_MEDIDA)).FirstOrDefault();
                                if (Objeto_UnidadMedida == null)
                                {
                                    ListaErrores.Add(this.ErrorEcxel(filaExcel, "[ID_UNIDAD_MEDIDA]  " + ID_UNIDAD_MEDIDA + " no se encuentra registrado en el Sistema.")); 
                                }
                            }
                            else
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[ID_UNIDAD_MEDIDA] debe ser un número")); 
                            }
                            string PRECIO_COMPRA = Convert.ToString(valores[4]);
                            if (!Recursos.Clases.Css_Validators.ValidarDecimal(PRECIO_COMPRA))
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[PRECIO_COMPRA] debe ser de tipo numerico o decimal")); 
                            }

                            string PRECIO_VENTA = Convert.ToString(valores[5]);
                            if (!Recursos.Clases.Css_Validators.ValidarDecimal(PRECIO_VENTA))
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[PRECIO_VENTA] debe ser de tipo numerico o decimal"));
                            }

                            string STOCK = Convert.ToString(valores[6]);
                            if (!Recursos.Clases.Css_Validators.ValidarNumber(STOCK))
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[STOCK] debe ser un número.")); 
                            }
                            string STOCK_MINIMO= Convert.ToString(valores[7]);
                            if (!Recursos.Clases.Css_Validators.ValidarNumber(STOCK_MINIMO))
                            {

                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[STOCK_MINIMO] debe ser un número.")); 
                            }
                            string FLG_SERVICIO = Convert.ToString(valores[8]);
                            if (!Recursos.Clases.Css_Validators.ValidarNumber(FLG_SERVICIO))
                            {
                                ListaErrores.Add(this.ErrorEcxel(filaExcel, "[FLG_SERVICIO] debe ser un número, 0/1.")); 
                            }

                            if (valores.Length == 10)
                            {
                                string FECHA_VENCIMIENTO = Convert.ToString(valores[9]);
                                if (!Recursos.Clases.Css_Validators.ValidarFecha(FECHA_VENCIMIENTO))
                                {
                                    ListaErrores.Add(this.ErrorEcxel(filaExcel, "[FECHA_VENCIMIENTO] no tiene el formato correto dd/mm/yyy.")); 
                                }
                            }                       
                            filaExcel++;
                        }   
                }

                if (!ColumIncomplete)
                {
                    if (ListaErrores.Count > 0)
                    {
                        auditoria.Rechazar("Se encontraron errores en el archivo cargado, por favor revisar los detalles y volver a subir el archivo.");
                        auditoria.OBJETO = ListaErrores;
                    }
                    else
                    {
                        auditoria.Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                auditoria.Rechazar(mensaje);
            }
        }
        private Cls_Ent_ErroresExcel ErrorEcxel(int NRO_FILA, string DESCRIPCION) {
            Cls_Ent_ErroresExcel errores = new Cls_Ent_ErroresExcel();
            errores.NRO_FILA = NRO_FILA;
            errores.DESCRIPCION = DESCRIPCION; 
            return errores; 
        }
        protected IEnumerable<Row> GetRowsGreaterEqualThan(Worksheet sheet, int index)
        {
            IEnumerable<Row> rows = from row in sheet.Descendants<Row>()
                                    where row.RowIndex >= index //The table begins on line 5
                                    select row;
            return rows;
        }
        protected string[] GetRowValue(SharedStringTable table, Row row)
        {
            List<String> values = new List<string>();
            foreach (Cell cell in from cell in row.Descendants<Cell>() where cell.CellValue != null select cell)
            {
                if (cell.DataType != null && cell.DataType.HasValue && cell.DataType == CellValues.SharedString)
                { values.Add(table.ChildElements[int.Parse(cell.CellValue.InnerText)].InnerText); }
                else
                { values.Add(cell.CellValue.InnerText); }
            }

            return values.ToArray();
        }



    }
}
