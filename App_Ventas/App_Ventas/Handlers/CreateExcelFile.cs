#define INCLUDE_WEB_FUNCTIONS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Text;
using System.IO;
using System.Web.UI;
using Capa_Entidad.Base;
using Capa_Entidad.Administracion;
using App_Ventas.Areas.Administracion.Repositorio;

namespace App_Ventas.Handlers
{

   //
    //  March 2014
    //  http://www.mikesknowledgebase.com
    //
    //  Note: if you plan to use this in an ASP.Net application, remember to add a reference to "System.Web", and to uncomment
    //  the "INCLUDE_WEB_FUNCTIONS" definition at the top of this file.
    //
    //  Release history
    //   - Mar 2014: 
    //        Now writes the Excel data using the OpenXmlWriter classes, which are much more memory efficient.
    //   - Nov 2013: 
    //        Changed "CreateExcelDocument(DataTable dt, string xlsxFilePath)" to remove the DataTable from the DataSet after creating the Excel file.
    //        You can now create an Excel file via a Stream (making it more ASP.Net friendly)
    //   - Jan 2013: Fix: Couldn't open .xlsx files using OLEDB  (was missing "WorkbookStylesPart" part)
    //   - Nov 2012: 
    //        List<>s with Nullable columns weren't be handled properly.
    //        If a value in a numeric column doesn't have any data, don't write anything to the Excel file (previously, it'd write a '0')
    //   - Jul 2012: Fix: Some worksheets weren't exporting their numeric data properly, causing "Excel found unreadable content in '___.xslx'" errors.
    //   - Mar 2012: Fixed issue, where Microsoft.ACE.OLEDB.12.0 wasn't able to connect to the Excel files created using this class.
    //
    //
    //   (c) www.mikesknowledgebase.com 2014 
    //   
    //   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
    //   (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
    //   publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
    //   subject to the following conditions:
    //   
    //   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
    //   
    //   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    //   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    //   FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    //   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    //   
        public class CreateExcelFile
        {
            //public static bool CreateExcelDocument2<T>(List<T> list, string xlsxFilePath, Cls_Ent_Titulo titulo, bool solo_columna, List<Cls_Ent_Columnas> columnas = null)
            //{
            //    DataSet ds = new DataSet();
            //    ds.Tables.Add(ListToDataTable2(list, solo_columna, columnas));

            //    return CreateExcelDocument2(ds, xlsxFilePath, titulo, solo_columna, columnas);
            //}

            //public static bool CreateExcelDocument2(DataSet ds, string excelFilename, Cls_Ent_Titulo titulo, bool solo_columna, List<Cls_Ent_Columnas> columnas)
            //{
            //    try
            //    {
            //        using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
            //        {
            //            WriteExcelFile2(ds, document, titulo, solo_columna, columnas);
            //        }
            //        Trace.WriteLine("Successfully created: " + excelFilename);
            //        return true;
            //    }
            //    catch (Exception ex)
            //    {
            //        Trace.WriteLine("Failed, exception thrown: " + ex.Message);
            //        return false;
            //    }
            //}

            public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(ListToDataTable(list));

                return CreateExcelDocument(ds, xlsxFilePath);
            }
            #region HELPER_FUNCTIONS
            //  This function is adapated from: http://www.codeguru.com/forum/showthread.php?t=450171
            //  My thanks to Carl Quirion, for making it "nullable-friendly".

            public static DataTable ListToDataTable2<T>(List<T> list, bool solo_columna, List<Cls_Ent_Columnas> columnas = null)
            {
                DataTable dt = new DataTable();
                if (!solo_columna)
                {
                    foreach (PropertyInfo info in typeof(T).GetProperties())
                    {
                        bool valido = false;
                        string nombre_columna = info.Name;
                        if (columnas != null)
                        {
                            foreach (Cls_Ent_Columnas columna in columnas)
                            {
                                if (columna.ID_COLUMNA == info.Name)
                                {
                                    nombre_columna = columna.DESCRIPCION_COLUMNA;
                                    valido = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            valido = true;
                        }

                        if (valido)
                        {
                            dt.Columns.Add(new DataColumn(nombre_columna, GetNullableType(info.PropertyType)));
                        }
                    }
                    foreach (T t in list)
                    {
                        DataRow row = dt.NewRow();
                        foreach (PropertyInfo info in typeof(T).GetProperties())
                        {
                            bool valido = false;
                            string nombre_columna = info.Name;
                            if (columnas != null)
                            {
                                foreach (Cls_Ent_Columnas columna in columnas)
                                {
                                    if (columna.ID_COLUMNA == info.Name)
                                    {
                                        nombre_columna = columna.DESCRIPCION_COLUMNA;
                                        valido = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                valido = true;
                            }
                            if (valido)
                            {
                                if (!IsNullableType(info.PropertyType))
                                    row[nombre_columna] = info.GetValue(t, null);
                                else
                                    row[nombre_columna] = (info.GetValue(t, null) ?? DBNull.Value);
                            }
                        }
                        dt.Rows.Add(row);
                    }
                }
                else
                {
                    foreach (Cls_Ent_Columnas columna in columnas)
                    {
                        dt.Columns.Add(new DataColumn(columna.DESCRIPCION_COLUMNA));
                    }
                }
                return dt;
            }


            public static DataTable ListToDataTable<T>(List<T> list)
            {
                DataTable dt = new DataTable();

                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
                }
                foreach (T t in list)
                {
                    DataRow row = dt.NewRow();
                    foreach (PropertyInfo info in typeof(T).GetProperties())
                    {
                        if (!IsNullableType(info.PropertyType))
                            row[info.Name] = info.GetValue(t, null);
                        else
                            row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                    }
                    dt.Rows.Add(row);
                }
                return dt;
            }
            private static Type GetNullableType(Type t)
            {
                Type returnType = t;
                if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    returnType = Nullable.GetUnderlyingType(t);
                }
                return returnType;
            }
            private static bool IsNullableType(Type type)
            {
                return (type == typeof(string) ||
                        type.IsArray ||
                        (type.IsGenericType &&
                         type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
            }

            public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                bool result = CreateExcelDocument(ds, xlsxFilePath);
                ds.Tables.Remove(dt);
                return result;
            }
            #endregion

#if INCLUDE_WEB_FUNCTIONS
            /// <summary>
            /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
            /// </summary>
            /// <param name="dt">DataTable containing the data to be written to the Excel.</param>
            /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
            /// <param name="Response">HttpResponse of the current page.</param>
            /// <returns>True if it was created succesfully, otherwise false.</returns>
            public static bool CreateExcelDocument(DataTable dt, string filename, System.Web.HttpResponse Response)
            {
                try
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    CreateExcelDocumentAsStream(ds, filename, Response);
                    ds.Tables.Remove(dt);
                    return true;
                }
                catch (Exception ex)
                {

                    Recursos.Clases.Css_Log.Guardar("A2:" + ex.ToString());
                    //Recursos.Clases.Css_Log.Guardar(ex.ToString());
                    Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                    return false;
                }
            }

            public static bool CreateExcelDocument<T>(List<T> list, string filename, System.Web.HttpResponse Response)
            {
                try
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(ListToDataTable(list));
                    CreateExcelDocumentAsStream(ds, filename, Response);
                    return true;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                    return false;
                }
            }

            /// <summary>
            /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
            /// </summary>
            /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
            /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
            /// <param name="Response">HttpResponse of the current page.</param>
            /// <returns>Either a MemoryStream, or NULL if something goes wrong.</returns>
            public static bool CreateExcelDocumentAsStream(DataSet ds, string filename, System.Web.HttpResponse Response)
            {
                try
                {
                    System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                    {
                        WriteExcelFile(ds, document);
                    }


                    stream.Flush();
                    stream.Position = 0;

                    Response.ClearContent();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";

                    //  NOTE: If you get an "HttpCacheability does not exist" error on the following line, make sure you have
                    //  manually added System.Web to this project's References.

                    Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    byte[] data1 = new byte[stream.Length];
                    stream.Read(data1, 0, data1.Length);
                    stream.Close();
                    Response.BinaryWrite(data1);
                    Response.Flush();
                    Response.End();

                    return true;
                }
                catch (Exception ex)
                {
                    Recursos.Clases.Css_Log.Guardar("A3:" + ex.ToString());
                    //Recursos.Clases.Css_Log.Guardar(ex.ToString());
                    //Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                    return false;
                }
            }
#endif      //  End of "INCLUDE_WEB_FUNCTIONS" section

            /// <summary>
            /// Create an Excel file, and write it to a file.
            /// </summary>
            /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
            /// <param name="excelFilename">Name of file to be written.</param>
            /// <returns>True if successful, false if something went wrong.</returns>
            public static bool CreateExcelDocument(DataSet ds, string excelFilename)
            {
                try
                {

                    using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                    {
                        WriteExcelFile(ds, document);
                    }
                    Trace.WriteLine("Successfully created: " + excelFilename);
                    return true;
                }
                catch (Exception ex)
                {
                    Recursos.Clases.Css_Log.Guardar("A1:" + ex.ToString());
                    //Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                    return false;
                }
            }

            private static void WriteExcelFile2(DataSet ds, SpreadsheetDocument spreadsheet, Cls_Ent_Titulo titulo, bool solo_columna, List<Cls_Ent_Columnas> columnas)
            {
                //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
                //  to a file, or writing to a MemoryStream.

                //spreadsheet.AddWorkbookPart();
                //spreadsheet.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                ////  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
                //spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

                ////  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
                //WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
                //Stylesheet stylesheet = new Stylesheet();
                //workbookStylesPart.Stylesheet = stylesheet;

                ////  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
                //uint worksheetNumber = 1;
                //Sheets sheets = spreadsheet.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                //foreach (DataTable dt in ds.Tables)
                //{
                //    //  For each worksheet you want to create
                //    string worksheetName = dt.TableName;

                //    //  Create worksheet part, and add it to the sheets collection in workbook
                //    WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                //    Sheet sheet = new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart), SheetId = worksheetNumber, Name = worksheetName };
                //    sheets.Append(sheet);

                //    //  Append this worksheet's data to our Workbook, using OpenXmlWriter, to prevent memory problems
                //    WriteDataTableToExcelWorksheet(dt, newWorksheetPart);

                //    worksheetNumber++;
                //}

                //spreadsheet.WorkbookPart.Workbook.Save(); 
                //using (var spreadsheet = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {

                    //spreadsheet.Close();

                    Console.WriteLine("Creating workbook");
                    spreadsheet.AddWorkbookPart();
                    //spreadsheet.AddWorkbookPart();
                    spreadsheet.WorkbookPart.Workbook = new Workbook();
                    Console.WriteLine("Creating worksheet");
                    var wsPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                    wsPart.Worksheet = new Worksheet();

                    // create a solid red fill
                    //var solidGreen = new PatternFill() { PatternType = PatternValues.Solid };
                    //solidGreen.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("a9d08e") };
                    //solidGreen.BackgroundColor = new BackgroundColor { Indexed = 64 };


                    //var solidGreen = new PatternFill() { PatternType = PatternValues.Solid };
                    //solidGreen.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("80ff00") }; // red fill
                    //solidGreen.BackgroundColor = new BackgroundColor { Indexed = 65 };

                    //var solidYellow = new PatternFill() { PatternType = PatternValues.Solid };
                    //solidYellow.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("ffff00") }; // red fill
                    //solidYellow.BackgroundColor = new BackgroundColor { Indexed = 66 };
                    ////  solidYellow.

                    //var solidBlue = new PatternFill() { PatternType = PatternValues.Solid };
                    //solidBlue.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("0000ff") }; // red fill
                    //solidBlue.BackgroundColor = new BackgroundColor { Indexed = 67 };

                    AddStyle(spreadsheet);
                    //AddStyleRow(spreadsheet);
                    //var stylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                    //stylesPart.Stylesheet = new Stylesheet();

                    //Console.WriteLine("Creating styles");

                    //// blank font list
                    //stylesPart.Stylesheet.Fonts = new Fonts();
                    //stylesPart.Stylesheet.Fonts.Count = 1;
                    //stylesPart.Stylesheet.Fonts.AppendChild(new Font());

                    //// create fills
                    //stylesPart.Stylesheet.Fills = new Fills();
                    //stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); // required, reserved by Excel
                    //stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.Gray125 } }); // required, reserved by Excel
                    //stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = solidRed }); 
                    //stylesPart.Stylesheet.Fills.Count = 3;

                    //// blank border list
                    //stylesPart.Stylesheet.Borders = new Borders();
                    //stylesPart.Stylesheet.Borders.Count = 1;
                    //stylesPart.Stylesheet.Borders.AppendChild(new Border());

                    //// blank cell format list
                    //stylesPart.Stylesheet.CellStyleFormats = new CellStyleFormats();
                    //stylesPart.Stylesheet.CellStyleFormats.Count = 1;
                    //stylesPart.Stylesheet.CellStyleFormats.AppendChild(new CellFormat());

                    //// cell format list
                    //stylesPart.Stylesheet.CellFormats = new CellFormats();
                    //// empty one for index 0, seems to be required
                    //stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat());
                    //// cell format references style format 0, font 0, border 0, fill 2 and applies the fill
                    //stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 0, FillId = 2, ApplyFill = true }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center });
                    //stylesPart.Stylesheet.CellFormats.Count = 2;

                    //stylesPart.Stylesheet.Save();

                    Columns columns = new Columns();

                    columns.Append(new Column() { Min = 1, Max = 1, Width = 20, CustomWidth = true });
                    columns.Append(new Column() { Min = 2, Max = 2, Width = 26, CustomWidth = true });
                    columns.Append(new Column() { Min = 3, Max = 100, Width = 20, CustomWidth = true });

                    wsPart.Worksheet.Append(columns);
                    var sheetData = wsPart.Worksheet.AppendChild(new SheetData());
                    //Console.WriteLine("Adding rows / cells...");
                    if (titulo != null)
                    {
                        sheetData.AppendChild(new Row());
                        sheetData.AppendChild(new Row());
                    }
                    var row = sheetData.AppendChild(new Row());
                    MergeCells mergeCells = new MergeCells();

                    if (titulo != null)
                    {
                        mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:B3") });
                        mergeCells.Append(new MergeCell() { Reference = new StringValue("C3:" + titulo.TITULO_CELDA + "3") });
                        row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                        row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                        //row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                        row.AppendChild(new Cell() { CellValue = new CellValue(titulo.TITULO), DataType = CellValues.String, StyleIndex = 3 });
                        for (int ix = 0; ix < titulo.TITULO_INT; ix++)
                        {
                            //row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                            row.AppendChild(new Cell() { DataType = CellValues.String, StyleIndex = 3 });
                        }
                        sheetData.AppendChild(new Row());
                        sheetData.AppendChild(new Row());
                        row = sheetData.AppendChild(new Row());
                        row.Height = 35;
                        row.CustomHeight = true;
                    }
                    DataTable dt = ds.Tables[0];
                    int numberOfColumns = dt.Columns.Count;
                    bool[] IsNumericColumn = new bool[numberOfColumns];

                    string[] excelColumnNames = new string[numberOfColumns];
                    for (int n = 0; n < numberOfColumns; n++)
                        excelColumnNames[n] = GetExcelColumnName(n);

                    // Se arma columnas
                    int celda_fila = 6;
                    for (int colInx = 0; colInx < numberOfColumns; colInx++)
                    {
                        DataColumn col = dt.Columns[colInx];
                        row.AppendChild(new Cell() { CellValue = new CellValue(col.ColumnName), DataType = CellValues.String, StyleIndex = 1 });

                        if (columnas != null)
                        {
                            if (!string.IsNullOrEmpty(columnas[colInx].CELDA_INICIO))
                            {
                                mergeCells.Append(new MergeCell() { Reference = new StringValue(columnas[colInx].CELDA_INICIO + celda_fila + ":" + columnas[colInx].CELDA_FIN + celda_fila) });
                                if (columnas[colInx].CELDA_INICIO != columnas[colInx].CELDA_FIN)
                                {
                                    for (int ix = 0; ix < columnas[colInx].INT_CELDAS; ix++)
                                    {
                                        row.AppendChild(new Cell() { DataType = CellValues.String, StyleIndex = 1 });
                                    }
                                }
                            }
                        }
                        //, CellReference = new StringValue("")
                        //AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, ref writer);
                        //IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32") 
                        //|| (col.DataType.FullName == "System.Double") || (col.DataType.FullName == "System.Single");
                    }
                    celda_fila++;

                    // Se arma las filas
                    if (!solo_columna)
                    {
                        int nro = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = sheetData.AppendChild(new Row());
                            nro++;
                            for (int colInx = 0; colInx < numberOfColumns; colInx++)
                            {
                                string cellValue = dr.ItemArray[colInx].ToString();
                                if (columnas != null)
                                {
                                    if (columnas[colInx].AUTO_INCREMENTAR)
                                    {
                                        cellValue = nro.ToString();
                                    }
                                }
                                Cell micell = row.AppendChild(new Cell() { CellValue = new CellValue(cellValue), DataType = CellValues.String, StyleIndex = 2 });
                                if (columnas != null)
                                {
                                    mergeCells.Append(new MergeCell() { Reference = new StringValue(columnas[colInx].CELDA_INICIO + celda_fila + ":" + columnas[colInx].CELDA_FIN + celda_fila) });

                                    if (columnas[colInx].CELDA_INICIO != columnas[colInx].CELDA_FIN)
                                    {
                                        for (int ix = 0; ix < columnas[colInx].INT_CELDAS; ix++)
                                        {
                                            row.AppendChild(new Cell() { DataType = CellValues.String, StyleIndex = 1 });
                                        }
                                    }
                                    if (columnas[colInx].CONDICIONES != null)
                                    {
                                        foreach (Cls_Ent_Columnas_Condicion condicion in columnas[colInx].CONDICIONES)
                                        {
                                            if (condicion.ID_COLUMNA == columnas[colInx].ID_COLUMNA)
                                            {
                                                if (condicion.VALOR == cellValue)
                                                {
                                                    micell.StyleIndex = condicion.STYLE_INDEX;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            celda_fila++;
                        }
                    }
                    //Console.WriteLine("Saving worksheet");
                    wsPart.Worksheet.Save();

                    // Console.WriteLine("Creating sheet list");
                    var sheets = spreadsheet.WorkbookPart.Workbook.AppendChild(new Sheets());
                    sheets.AppendChild(new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(wsPart), SheetId = 1, Name = "Hoja1" });

                    //Console.WriteLine("Saving workbook");
                    string ruta_temporal = "";
                    if (titulo != null)
                    {
                         string imagePath1  =""; 
                         Cls_Ent_configurarEmpresa entidad = new Cls_Ent_configurarEmpresa(); 
                          Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
                          using (ConfigurarEmpresaRepositorio repositorio = new ConfigurarEmpresaRepositorio())
                          {
                              entidad = repositorio.configurarEmpresa_Listar(ref auditoria);
                              if (!auditoria.EJECUCION_PROCEDIMIENTO)
                              {
                                  string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                                  auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                              }
                              else
                              {
                                  if (entidad.Archivo_Logo != null)
                                  {
                                      string CODIGO_NOMBRE_EMPRESA = entidad.CODIGO_ARCHIVO_LOGO + entidad.EXTENSION_ARCHIVO_LOGO;
                                      //entidad.Archivo_Logo = new Cls_Ent_Archivo();
                                      imagePath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Recursos\Logo_Empresa\" + CODIGO_NOMBRE_EMPRESA);

                                  }
                                  else
                                  {
                                      imagePath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Recursos\Imagenes\tulogo.png");
                                  }
                              }
                          }

                     

                        System.Drawing.Bitmap bm = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(imagePath1);
                        System.Drawing.Bitmap resized = new System.Drawing.Bitmap(bm, new System.Drawing.Size(320, 68));

                        string CODIGO = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                        string CODIGO_NOMBRE = CODIGO + ".png";
                        ruta_temporal = Recursos.Clases.Css_Ruta.Ruta_Temporal() + @"\" + CODIGO_NOMBRE;
                        resized.Save(ruta_temporal, System.Drawing.Imaging.ImageFormat.Png);

                        Handlers.ExcelTools.AddImage(wsPart, ruta_temporal, "My first image", 1, 1); // A1
                    }
                    if (titulo != null)
                    {
                        wsPart.Worksheet.InsertAfter(mergeCells, wsPart.Worksheet.Elements<SheetData>().First());
                    }
                    wsPart.Worksheet.Save();
                    spreadsheet.WorkbookPart.Workbook.Save();
                    Console.WriteLine("Done.");
                    if (File.Exists(ruta_temporal))
                    {
                        File.Delete(ruta_temporal);
                    }
                }
            }

            private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet)
            {
                //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
                //  to a file, or writing to a MemoryStream.

                //spreadsheet.AddWorkbookPart();
                //spreadsheet.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                ////  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
                //spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

                ////  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
                //WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
                //Stylesheet stylesheet = new Stylesheet();
                //workbookStylesPart.Stylesheet = stylesheet;

                ////  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
                //uint worksheetNumber = 1;
                //Sheets sheets = spreadsheet.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                //foreach (DataTable dt in ds.Tables)
                //{
                //    //  For each worksheet you want to create
                //    string worksheetName = dt.TableName;

                //    //  Create worksheet part, and add it to the sheets collection in workbook
                //    WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                //    Sheet sheet = new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart), SheetId = worksheetNumber, Name = worksheetName };
                //    sheets.Append(sheet);

                //    //  Append this worksheet's data to our Workbook, using OpenXmlWriter, to prevent memory problems
                //    WriteDataTableToExcelWorksheet(dt, newWorksheetPart);

                //    worksheetNumber++;
                //}

                //spreadsheet.WorkbookPart.Workbook.Save(); 
                //using (var spreadsheet = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {

                    //spreadsheet.Close();

                    Console.WriteLine("Creating workbook");
                    spreadsheet.AddWorkbookPart();
                    //spreadsheet.AddWorkbookPart();
                    spreadsheet.WorkbookPart.Workbook = new Workbook();
                    Console.WriteLine("Creating worksheet");
                    var wsPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                    wsPart.Worksheet = new Worksheet();

                    // create a solid red fill
                    //var solidRed = new PatternFill() { PatternType = PatternValues.Solid };
                    //solidRed.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("FFFF0000") }; // red fill
                    //solidRed.BackgroundColor = new BackgroundColor { Indexed = 64 };


                    //var solidGreen = new PatternFill() { PatternType = PatternValues.Solid };
                    //solidGreen.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("80ff00") }; // red fill
                    //solidGreen.BackgroundColor = new BackgroundColor { Indexed = 65 };

                    //var solidYellow = new PatternFill() { PatternType = PatternValues.Solid };
                    //solidYellow.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("ffff00") }; // red fill
                    //solidYellow.BackgroundColor = new BackgroundColor { Indexed = 66 };
                    ////  solidYellow.

                    //var solidBlue = new PatternFill() { PatternType = PatternValues.Solid };
                    //solidBlue.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("0000ff") }; // red fill
                    //solidBlue.BackgroundColor = new BackgroundColor { Indexed = 67 };

                    AddStyle(spreadsheet);
                    //AddStyleRow(spreadsheet);
                    //var stylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                    //stylesPart.Stylesheet = new Stylesheet();

                    //Console.WriteLine("Creating styles");

                    //// blank font list
                    //stylesPart.Stylesheet.Fonts = new Fonts();
                    //stylesPart.Stylesheet.Fonts.Count = 1;
                    //stylesPart.Stylesheet.Fonts.AppendChild(new Font());

                    //// create fills
                    //stylesPart.Stylesheet.Fills = new Fills();
                    //stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); // required, reserved by Excel
                    //stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.Gray125 } }); // required, reserved by Excel
                    //stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = solidRed }); 
                    //stylesPart.Stylesheet.Fills.Count = 3;

                    //// blank border list
                    //stylesPart.Stylesheet.Borders = new Borders();
                    //stylesPart.Stylesheet.Borders.Count = 1;
                    //stylesPart.Stylesheet.Borders.AppendChild(new Border());

                    //// blank cell format list
                    //stylesPart.Stylesheet.CellStyleFormats = new CellStyleFormats();
                    //stylesPart.Stylesheet.CellStyleFormats.Count = 1;
                    //stylesPart.Stylesheet.CellStyleFormats.AppendChild(new CellFormat());

                    //// cell format list
                    //stylesPart.Stylesheet.CellFormats = new CellFormats();
                    //// empty one for index 0, seems to be required
                    //stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat());
                    //// cell format references style format 0, font 0, border 0, fill 2 and applies the fill
                    //stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 0, FillId = 2, ApplyFill = true }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center });
                    //stylesPart.Stylesheet.CellFormats.Count = 2;

                    //stylesPart.Stylesheet.Save();

                    Columns columns = new Columns();

                    columns.Append(new Column() { Min = 1, Max = 100, Width = 20, CustomWidth = false });
                    //columns.Append(new Column() { Min = 1, Max = 30, Width = 330, CustomWidth = true });

                    wsPart.Worksheet.Append(columns);

                    //Console.WriteLine("Creating sheet data");
                    var sheetData = wsPart.Worksheet.AppendChild(new SheetData());

                    //Console.WriteLine("Adding rows / cells...");

                    sheetData.AppendChild(new Row());
                    sheetData.AppendChild(new Row());
                    var row = sheetData.AppendChild(new Row());
                    MergeCells mergeCells = new MergeCells();

                    mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:C4") });
                    mergeCells.Append(new MergeCell() { Reference = new StringValue("D3:I3") });
                    row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                    row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                    row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                    row.AppendChild(new Cell() { CellValue = new CellValue("SISTEMA DE VENTA"), DataType = CellValues.String, StyleIndex = 3 });
                    row.AppendChild(new Cell() { DataType = CellValues.String, StyleIndex = 3 });
                    row.AppendChild(new Cell() { DataType = CellValues.String, StyleIndex = 3 });
                    row.AppendChild(new Cell() { DataType = CellValues.String, StyleIndex = 3 });
                    row.AppendChild(new Cell() { DataType = CellValues.String, StyleIndex = 3 });
                    row.AppendChild(new Cell() { DataType = CellValues.String, StyleIndex = 3 });
                    //row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                    //row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                    //row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                    //row.AppendChild(new Cell() { CellValue = new CellValue(""), DataType = CellValues.String });
                    // row.AppendChild(new MergeCell() { Reference = new StringValue("E3:P3") });

                    sheetData.AppendChild(new Row());
                    sheetData.AppendChild(new Row());
                    row = sheetData.AppendChild(new Row());


                    row.Height = 35;
                    row.CustomHeight = true;
                    DataTable dt = ds.Tables[0];
                    int numberOfColumns = dt.Columns.Count;
                    bool[] IsNumericColumn = new bool[numberOfColumns];

                    string[] excelColumnNames = new string[numberOfColumns];
                    for (int n = 0; n < numberOfColumns; n++)
                        excelColumnNames[n] = GetExcelColumnName(n);
                    for (int colInx = 0; colInx < numberOfColumns; colInx++)
                    {
                        //mergeCells.Append(new MergeCell() { Reference = new StringValue("A6:B6") });
                        DataColumn col = dt.Columns[colInx];
                        row.AppendChild(new Cell() { CellValue = new CellValue(col.ColumnName), DataType = CellValues.String, StyleIndex = 1 });

                        //AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, ref writer);
                        //IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32") || (col.DataType.FullName == "System.Double") || (col.DataType.FullName == "System.Single");
                    }
                    //
                    //  Create the Header row in our Excel Worksheet
                    //
                    //uint rowIndex = 1;
                    //for (int ix = 0; ix < 4; ix++)
                    //{
                    //    row.AppendChild(new Cell() { CellValue = new CellValue("This"), DataType = CellValues.String });
                    //    //writer.WriteStartElement(new Row { RowIndex = rowIndex });
                    //    //writer.WriteElement(new Cell { CellValue = new CellValue("xd") });
                    //    //writer.WriteEndElement();
                    //    //++rowIndex;
                    //}

                    //row.AppendChild(new Cell() { CellValue = new CellValue("This"), DataType = CellValues.String });
                    //row.AppendChild(new Cell() { CellValue = new CellValue("is"), DataType = CellValues.String });
                    //row.AppendChild(new Cell() { CellValue = new CellValue("a"), DataType = CellValues.String });
                    //row.AppendChild(new Cell() { CellValue = new CellValue("test."), DataType = CellValues.String });

                    //sheetData.AppendChild(new Row());
                    //sheetData.AppendChild(new Row());
                    //sheetData.AppendChild(new Row());
                    //sheetData.AppendChild(new Row());


                    //double cellNumericValue = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        // ...create a new row, and append a set of this row's data to it.
                        //++rowIndex;

                        row = sheetData.AppendChild(new Row());
                        //writer.WriteStartElement(new Row { RowIndex = rowIndex });

                        for (int colInx = 0; colInx < numberOfColumns; colInx++)
                        {
                            string cellValue = dr.ItemArray[colInx].ToString();
                            row.AppendChild(new Cell() { CellValue = new CellValue(cellValue), DataType = CellValues.String, StyleIndex = 2 });


                            // Create cell with data
                            //if (IsNumericColumn[colInx])
                            //{
                            //    //  For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                            //    //  If this numeric value is NULL, then don't write anything to the Excel file.
                            //    cellNumericValue = 0;
                            //    if (double.TryParse(cellValue, out cellNumericValue))
                            //    {
                            //        cellValue = cellNumericValue.ToString();
                            //        AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
                            //    }
                            //}
                            //else
                            //{
                            //    //  For text cells, just write the input data straight out to the Excel file.
                            //    //AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
                            //}
                        }
                        //writer.WriteEndElement(); //  End of Row
                    }
                    //row.AppendChild(new Cell() { CellValue = new CellValue("Value:"), DataType = CellValues.String });
                    //row.AppendChild(new Cell() { CellValue = new CellValue("123"), DataType = CellValues.Number });
                    //row.AppendChild(new Cell() { CellValue = new CellValue("Formula:"), DataType = CellValues.String });
                    //// style index = 1, i.e. point at our fill format
                    //row.AppendChild(new Cell() { CellFormula = new CellFormula("B3"), DataType = CellValues.Number, StyleIndex = 2 });

                    Console.WriteLine("Saving worksheet");
                    wsPart.Worksheet.Save();

                    Console.WriteLine("Creating sheet list");
                    var sheets = spreadsheet.WorkbookPart.Workbook.AppendChild(new Sheets());
                    sheets.AppendChild(new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(wsPart), SheetId = 1, Name = "Test" });

                    Console.WriteLine("Saving workbook");

                    //spreadsheet.WorkbookPart.Workbook.Save();

                    // var worksheetPart = Handlers.ExcelTools.GetWorksheetPartByName(spreadsheet, "Some sheet name");
                    string imagePath1 = "";
                    Cls_Ent_configurarEmpresa entidad = new Cls_Ent_configurarEmpresa();
                    Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
                    using (ConfigurarEmpresaRepositorio repositorio = new ConfigurarEmpresaRepositorio())
                    {
                        entidad = repositorio.configurarEmpresa_Listar(ref auditoria);
                        if (!auditoria.EJECUCION_PROCEDIMIENTO)
                        {
                            string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                            auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                        }
                        else
                        {
                            if (entidad.CODIGO_ARCHIVO_LOGO != null)
                            {
                                string CODIGO_NOMBRE_EMPRESA = entidad.CODIGO_ARCHIVO_LOGO + entidad.EXTENSION_ARCHIVO_LOGO;
                                //entidad.Archivo_Logo = new Cls_Ent_Archivo();
                                imagePath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Recursos\Logo_Empresa\" + CODIGO_NOMBRE_EMPRESA);

                            }
                            else
                            {
                                imagePath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Recursos\Imagenes\tulogo.png");
                            }
                        }
                    }

                     //imagePath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Recursos\Imagenes\cabecera.png");

                    System.Drawing.Bitmap bm = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(imagePath1);
                    System.Drawing.Bitmap resized = new System.Drawing.Bitmap(bm, new System.Drawing.Size(320, 90));


                    string CODIGO = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                    string CODIGO_NOMBRE = CODIGO + ".png";
                    var ruta_temporal = Recursos.Clases.Css_Ruta.Ruta_Temporal() + @"\" + CODIGO_NOMBRE;

                    resized.Save(ruta_temporal, System.Drawing.Imaging.ImageFormat.Png);
                    //resized.Save
                    //Bitmap original = (Bitmap)Image.FromFile("DSC_0002.jpg");
                    Handlers.ExcelTools.AddImage(wsPart, ruta_temporal, "My first image", 1, 1); // A1
                    wsPart.Worksheet.InsertAfter(mergeCells, wsPart.Worksheet.Elements<SheetData>().First());
                    wsPart.Worksheet.Save();
                    spreadsheet.WorkbookPart.Workbook.Save();
                    Console.WriteLine("Done.");
                }


            }

            private static void AddStyle(SpreadsheetDocument spreadsheet)
            {
                var stylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = new Stylesheet();

                Console.WriteLine("Creating styles");

                // blank font list
                stylesPart.Stylesheet.Fonts = new Fonts();
                stylesPart.Stylesheet.Fonts.Count = 2;
                stylesPart.Stylesheet.Fonts.AppendChild(new Font());
                stylesPart.Stylesheet.Fonts.AppendChild(new Font { Color = new Color { Rgb = HexBinaryValue.FromString("808080") }, FontSize = new FontSize { Val = 18 } });


                var solidRed = new PatternFill() { PatternType = PatternValues.Solid };
                solidRed.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("FFFF0000") }; // red fill
                solidRed.BackgroundColor = new BackgroundColor { Indexed = 64 };

                var solidCeleste = new PatternFill() { PatternType = PatternValues.Solid };
                solidCeleste.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("ddebf7") }; // red fill
                solidCeleste.BackgroundColor = new BackgroundColor { Indexed = 65 };

                // create fills
                stylesPart.Stylesheet.Fills = new Fills();
                stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); // required, reserved by Excel
                stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.Gray125 } }); // required, reserved by Excel
                stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = solidRed });
                stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = solidCeleste });
                stylesPart.Stylesheet.Fills.Count = 4;

                // blank border list
                stylesPart.Stylesheet.Borders = new Borders();
                stylesPart.Stylesheet.Borders.Count = 3;
                stylesPart.Stylesheet.Borders.AppendChild(new Border());
                stylesPart.Stylesheet.Borders.AppendChild(new Border
                {
                    RightBorder = new RightBorder { Style = BorderStyleValues.Thin },
                    LeftBorder = new LeftBorder { Style = BorderStyleValues.Thin },
                    TopBorder = new TopBorder { Style = BorderStyleValues.Thin },
                    BottomBorder = new BottomBorder { Style = BorderStyleValues.Thin }
                });
                stylesPart.Stylesheet.Borders.AppendChild(new Border
                {
                    RightBorder = new RightBorder { Style = BorderStyleValues.Medium },
                    LeftBorder = new LeftBorder { Style = BorderStyleValues.Medium },
                    TopBorder = new TopBorder { Style = BorderStyleValues.Medium },
                    BottomBorder = new BottomBorder { Style = BorderStyleValues.Medium }
                });

                // blank cell format list
                stylesPart.Stylesheet.CellStyleFormats = new CellStyleFormats();
                stylesPart.Stylesheet.CellStyleFormats.Count = 1;
                stylesPart.Stylesheet.CellStyleFormats.AppendChild(new CellFormat());

                // cell format list
                stylesPart.Stylesheet.CellFormats = new CellFormats();
                // empty one for index 0, seems to be required
                stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat());
                // cell format references style format 0, font 0, border 0, fill 2 and applies the fill
                // Columnas
                stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 1, FillId = 3, ApplyFill = false }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true });
                // Filas
                stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 1, FillId = 0, ApplyFill = false }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true });
                // Titulo
                stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 1, BorderId = 2, FillId = 0, ApplyFill = false }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
                stylesPart.Stylesheet.CellFormats.Count = 4;

                stylesPart.Stylesheet.Save();
            }

            private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart)
            {


                OpenXmlWriter writer = OpenXmlWriter.Create(worksheetPart);
                writer.WriteStartElement(new Worksheet());
                writer.WriteStartElement(new SheetData());

                //var stylesPart = worksheetPart.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                string cellValue = "";

                //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
                //
                //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
                //  cells of data, we'll know if to write Text values or Numeric cell values.
                int numberOfColumns = dt.Columns.Count;
                bool[] IsNumericColumn = new bool[numberOfColumns];

                string[] excelColumnNames = new string[numberOfColumns];
                for (int n = 0; n < numberOfColumns; n++)
                    excelColumnNames[n] = GetExcelColumnName(n);

                //
                //  Create the Header row in our Excel Worksheet
                //
                uint rowIndex = 1;
                for (int ix = 0; ix < 4; ix++)
                {
                    writer.WriteStartElement(new Row { RowIndex = rowIndex });
                    writer.WriteElement(new Cell { CellValue = new CellValue("xd") });
                    writer.WriteEndElement();
                    ++rowIndex;
                }
                // writer.WriteString("<tr>");
                //writer.WriteStartElement(new Row { RowIndex = rowIndex });
                //writer.WriteStartDocument(true);
                //writer.WriteElement(new string { "daasa" });
                //writer.WriteString("<tr>");
                //writer.WriteString("<tr>");
                //writer.WriteString("<td colspan='2' text-align='center' valign='middle'><img src='" + System.Configuration.ConfigurationManager.AppSettings["urlImagencargo"].ToString() + "assets/Fondo_Minem.png" + "' width ='400' height='90'/>   </td> ");
                //writer.WriteString("</tr>");

                //writer.WriteString("<td colspan='16' text-align='center'>");
                //writer.WriteString("<table  ><tr> <td colspan='24' align='center' valign='middle'><h3>Listado de Usuarios</h3> </td></tr></table> ");
                //writer.WriteString("</td>");
                //writer.WriteString("</tr>");
                //writer.WriteString("</table> ");
                //writer.WriteEndElement();


                writer.WriteStartElement(new Row { RowIndex = rowIndex });
                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    DataColumn col = dt.Columns[colInx];
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, ref writer);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32") || (col.DataType.FullName == "System.Double") || (col.DataType.FullName == "System.Single");
                }
                writer.WriteEndElement();   //  End of header "Row"

                //
                //  Now, step through each row of data in our DataTable...
                //
                double cellNumericValue = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    // ...create a new row, and append a set of this row's data to it.
                    ++rowIndex;

                    writer.WriteStartElement(new Row { RowIndex = rowIndex });

                    for (int colInx = 0; colInx < numberOfColumns; colInx++)
                    {
                        cellValue = dr.ItemArray[colInx].ToString();

                        // Create cell with data
                        if (IsNumericColumn[colInx])
                        {
                            //  For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                            //  If this numeric value is NULL, then don't write anything to the Excel file.
                            cellNumericValue = 0;
                            if (double.TryParse(cellValue, out cellNumericValue))
                            {
                                cellValue = cellNumericValue.ToString();
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
                            }
                        }
                        else
                        {
                            //  For text cells, just write the input data straight out to the Excel file.
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
                        }
                    }
                    writer.WriteEndElement(); //  End of Row
                }
                writer.WriteEndElement(); //  End of SheetData
                writer.WriteEndElement(); //  End of worksheet

                writer.Close();
            }

            private static void AppendTextCell(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
            {
                //  Add a new Excel Cell to our Row 
                writer.WriteElement(new Cell { CellValue = new CellValue(cellStringValue), CellReference = cellReference, DataType = CellValues.String });
            }

            private static void AppendNumericCell(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
            {
                //  Add a new Excel Cell to our Row 
                writer.WriteElement(new Cell { CellValue = new CellValue(cellStringValue), CellReference = cellReference, DataType = CellValues.Number });
            }

            private static string GetExcelColumnName(int columnIndex)
            {
                //  Convert a zero-based column index into an Excel column reference  (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
                //
                //  eg  GetExcelColumnName(0) should return "A"
                //      GetExcelColumnName(1) should return "B"
                //      GetExcelColumnName(25) should return "Z"
                //      GetExcelColumnName(26) should return "AA"
                //      GetExcelColumnName(27) should return "AB"
                //      ..etc..
                //
                if (columnIndex < 26)
                    return ((char)('A' + columnIndex)).ToString();

                char firstChar = (char)('A' + (columnIndex / 26) - 1);
                char secondChar = (char)('A' + (columnIndex % 26));

                return string.Format("{0}{1}", firstChar, secondChar);
            
        }
    }
}