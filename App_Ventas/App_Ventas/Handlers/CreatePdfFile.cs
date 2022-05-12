using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI;
using Capa_Entidad.Base;
using Capa_Entidad;
using Capa_Entidad.Administracion;
namespace App_Ventas.Handlers
{
    public class CreatePdfFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"> List</typeparam>
        /// <param name="list"></param>
        /// <param name="PDFFilePath"></param>
        /// <param name="titulo"></param>
        /// <param name="RUTA_LOGO"></param>
        /// <param name="AutoWidth"></param>
        /// <param name="columnas"></param>
        public static void CreatePdfDocument<T>(List<T> list, string PDFFilePath, Cls_Ent_Titulo titulo, string RUTA_LOGO,bool AutoWidth, List<Cls_Ent_Columnas> columnas = null)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(ListToDataTable(list, columnas));
            WritePdfFile(ds, PDFFilePath, titulo, RUTA_LOGO, AutoWidth, columnas);
        }

        public static DataTable ListToDataTable<T>(List<T> list, List<Cls_Ent_Columnas> columnas = null)
        {
            DataTable dt = new DataTable();
            if (columnas != null)
            {
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
            }

            if (columnas != null)
            {
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

        public static void WritePdfFile(DataSet ds, string PDFFilePath, Cls_Ent_Titulo Titulo, string RUTA_LOGO, bool AutoWidth, List<Cls_Ent_Columnas> columnas)
        {
             string Datetime = DateTime.Now.ToString("dd/MM/yyyy");
             string Hora = DateTime.Now.ToString("hh:mm tt");
             Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
                try
                {
                using (var doc = new Document())
                {
                    DataTable dt = ds.Tables[0];
                    int numberOfColumns = dt.Columns.Count;
                    doc.SetMargins(20, 20, 20, 20);
                    if (numberOfColumns > 5)
                    doc.SetPageSize(PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(PDFFilePath, FileMode.Create, FileAccess.Write));
                    doc.Open();

                    BaseFont _titulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
                    iTextSharp.text.Font titulo = new iTextSharp.text.Font(_titulo, 14f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

                    BaseFont _parrafo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
                    iTextSharp.text.Font parrafo = new iTextSharp.text.Font(_parrafo, 9f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

                    #region Header
                    var table = new PdfPTable(3) { WidthPercentage = 100 };
                    var colSizes = new List<float> { 120f,150f };
                    //colSizes.Add(doc.PageSize.Width - colSizes.Sum());
                    colSizes.Insert(1, doc.PageSize.Width - colSizes.Sum());
                    table.SetWidths(colSizes.ToArray());
                    table.AddCell(new PdfPCell
                    {
                        Image = Image.GetInstance(RUTA_LOGO),
                        Border = 0,
                        BorderColorRight = BaseColor.WHITE,
                        Rowspan = 3
                    });

                    table.AddCell(new PdfPCell(CreatePhrase(Titulo.TITULO, titulo))
                    {
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Rowspan = 3
                    });
                    doc.Add(table);

                    table.AddCell(new PdfPCell(CreatePhrase("Usuario: Iperez", parrafo)) { VerticalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    table.AddCell(new PdfPCell(CreatePhrase("Fecha: " + Datetime, parrafo)) { VerticalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    table.AddCell(new PdfPCell(CreatePhrase("Hora: " + Hora, parrafo)) { VerticalAlignment = Element.ALIGN_RIGHT, Border = 0 });
                    doc.Add(table);

                    #endregion

                    #region Emails
                    //doc.Add(CreatePhrase(" "));
                    //table = new PdfPTable(1) { WidthPercentage = 100 };
                    //table.AddCell(CreateCell("ivansperezt@gmailcom", Element.ALIGN_CENTER));
                    //doc.Add(table);
                    #endregion

                    #region Matters
                    //doc.Add(CreatePhrase(" "));
                    table = new PdfPTable(numberOfColumns) { WidthPercentage = 100 };
                    colSizes = new List<float>();
                    int index = 0;
                    int PositionColumnAuto = 0; 
                    if (!AutoWidth)
                    {
                        columnas.ForEach(col =>
                        {
                            
                               if (!col.AUTO_INCREMENTAR)
                               {
                                   colSizes.Add(col.TAMANIO);
                                   index++;
                               }
                               else
                               {
                                   PositionColumnAuto = index;
                               }
                        });
                        colSizes.Insert(PositionColumnAuto, doc.PageSize.Width - colSizes.Sum());
                        table.SetWidths(colSizes.ToArray());
                    }

                    for (int colInx = 0; colInx < numberOfColumns; colInx++)
                    {
                        DataColumn col = dt.Columns[colInx];
                        var cell = new PdfPCell
                        {
                            Phrase = CreatePhrase(col.ColumnName, headerFont),
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            Padding = 5,
                            BackgroundColor = new BaseColor(238, 242, 247)
                        };
                        table.AddCell(cell);
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int colInx = 0; colInx < numberOfColumns; colInx++)
                        {
                            string cellValue = dr.ItemArray[colInx].ToString();
                            table.AddCell(CreateCell(cellValue, Element.ALIGN_CENTER));
                        }
                    }

                    doc.Add(table);
                    #endregion
                    #region Footer
                    //doc.Add(CreatePhrase("\nIf you have any questions, please contact us at info@syncids.com.\n"));
                    //doc.Add(CreatePhrase("\nThank you for using SyncIDS.com!"));
                    doc.Close();
                    writer.Close();
                    #endregion
                }
                }
                catch (Exception ex)
                {
                    //System.Diagnostics.Debug.WriteLine(objException.Message);
                    auditoria.Error(ex);
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
       
        }

        static Font headerFont = FontFactory.GetFont(BaseFont.HELVETICA, 10, Font.BOLD);
        static Font baseFont = FontFactory.GetFont(BaseFont.HELVETICA, 10);
        static PdfPCell CreateCell(string text, int? alignment, Font font = null )
        {
            var cell = new PdfPCell(CreatePhrase(text, font)) { Padding = 5 };
            if (alignment.HasValue) cell.HorizontalAlignment = alignment.Value;
            return cell;
        }

        static Phrase CreatePhrase(string str, Font font = null)
        {
            if (font == null) font = baseFont;
            return new Phrase(str, font);
        }


    }
}