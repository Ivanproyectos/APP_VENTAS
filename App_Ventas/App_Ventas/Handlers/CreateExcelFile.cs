﻿#define INCLUDE_WEB_FUNCTIONS

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
    public class CreateExcelFile
    {
        public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath, Cls_Ent_Titulo titulo, bool solo_columna, string NombreHoja, List<Cls_Ent_Columnas> columnas = null )
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(ListToDataTable(list, solo_columna, columnas));

            return CreateExcelDocument(ds, xlsxFilePath, titulo, solo_columna, columnas, NombreHoja);
        }



        #region HELPER_FUNCTIONS
        //  This function is adapated from: http://www.codeguru.com/forum/showthread.php?t=450171
        //  My thanks to Carl Quirion, for making it "nullable-friendly".
        public static DataTable ListToDataTable<T>(List<T> list, bool solo_columna, List<Cls_Ent_Columnas> columnas = null)
        {
            DataTable dt = new DataTable();
            if (!solo_columna)
            {
                //foreach (PropertyInfo info in typeof(T).GetProperties())
                //{
                //    bool valido = false;
                //    string nombre_columna = info.Name;
                //    if (columnas != null)
                //    {
                //        foreach (Cls_Ent_Columnas columna in columnas)
                //        {
                //            if (columna.ID_COLUMNA == info.Name)
                //            {
                //                nombre_columna = columna.DESCRIPCION_COLUMNA;
                //                valido = true;
                //                break;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        valido = true;
                //    }

                //    if (valido)
                //    {
                //        dt.Columns.Add(new DataColumn(nombre_columna, GetNullableType(info.PropertyType)));
                //    }
                //}

                foreach (Cls_Ent_Columnas columna in columnas)
                {
                    bool valido = false;
                    foreach (PropertyInfo info in typeof(T).GetProperties())
                    {
                        string nombre_columna = info.Name;
                        if (columna.ID_COLUMNA == info.Name)
                        {
                            nombre_columna = columna.DESCRIPCION_COLUMNA;
                            valido = true;
                        }
                        if (valido)
                        {
                            dt.Columns.Add(new DataColumn(nombre_columna, GetNullableType(info.PropertyType)));
                            break;
                        }
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

        public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath, Cls_Ent_Titulo titulo, bool solo_columna, List<Cls_Ent_Columnas> columnas, string NombreHoja)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            bool result = CreateExcelDocument(ds, xlsxFilePath, titulo, solo_columna, columnas, NombreHoja);
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
        public static bool CreateExcelDocument(DataTable dt, string filename, System.Web.HttpResponse Response, Cls_Ent_Titulo titulo, bool solo_columna, List<Cls_Ent_Columnas> columnas, string nombrehoja)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                CreateExcelDocumentAsStream(ds, filename, Response, titulo, solo_columna, columnas, nombrehoja);
                ds.Tables.Remove(dt);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        public static bool CreateExcelDocument<T>(List<T> list, string filename, System.Web.HttpResponse Response, Cls_Ent_Titulo titulo, bool solo_columna, List<Cls_Ent_Columnas> columnas, string nombrehoja)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(ListToDataTable(list, solo_columna));
                CreateExcelDocumentAsStream(ds, filename, Response, titulo, solo_columna, columnas, nombrehoja);
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
        public static bool CreateExcelDocumentAsStream(DataSet ds, string filename, System.Web.HttpResponse Response, Cls_Ent_Titulo titulo, bool solo_columna, List<Cls_Ent_Columnas> columnas, string nombrehoja)
        {
            try
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {
                    WriteExcelFile(ds, document, titulo, solo_columna, columnas, nombrehoja);
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
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
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
        public static bool CreateExcelDocument(DataSet ds, string excelFilename, Cls_Ent_Titulo titulo, bool solo_columna, List<Cls_Ent_Columnas> columnas, string NombreHoja)
        {
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {
                    WriteExcelFile(ds, document, titulo, solo_columna, columnas, NombreHoja);
                }
                Trace.WriteLine("Successfully created: " + excelFilename);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet, Cls_Ent_Titulo titulo, bool solo_columna, List<Cls_Ent_Columnas> columnas, string NombreHoja)
        {
            {

                //spreadsheet.Close();

                Console.WriteLine("Creating workbook");
                spreadsheet.AddWorkbookPart();
                //spreadsheet.AddWorkbookPart();
                spreadsheet.WorkbookPart.Workbook = new Workbook();
                Console.WriteLine("Creating worksheet");
                var wsPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                wsPart.Worksheet = new Worksheet();

                AddStyle(spreadsheet);

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
                sheets.AppendChild(new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(wsPart), SheetId = 1, Name = NombreHoja });

     
                //sheets.AppendChild(new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(wsPart), SheetId = 2, Name = "Hoja2" });

                //Console.WriteLine("Saving workbook");
                string ruta_temporal = "";
                if (titulo != null)
                {
                    string imagePath1 = titulo.RUTA_LOGO; 

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

            var solidGreen = new PatternFill() { PatternType = PatternValues.Solid };
            solidGreen.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("a9d08e") }; // red fill
            solidGreen.BackgroundColor = new BackgroundColor { Indexed = 66 };

            var solidRojiso = new PatternFill() { PatternType = PatternValues.Solid };
            solidRojiso.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("f4b084") }; // red fill
            solidRojiso.BackgroundColor = new BackgroundColor { Indexed = 67 };



            // create fills
            stylesPart.Stylesheet.Fills = new Fills();
            stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); // required, reserved by Excel
            stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.Gray125 } }); // required, reserved by Excel
            stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = solidRed });
            stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = solidCeleste });
            stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = solidGreen });
            stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = solidRojiso });
            stylesPart.Stylesheet.Fills.Count = 6;

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
            // Filas Custom Rojiso
            stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 1, FillId = 4, ApplyFill = true }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true });
            // Filas Custom Verde
            stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 1, FillId = 5, ApplyFill = true }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true });

            stylesPart.Stylesheet.CellFormats.Count = 6;

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