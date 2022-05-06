using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using App_Ventas.Areas.Inventario.Models;
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
                long ID_SUCURSAL = long.Parse(forms["ID_SUCURSAL"].ToString());
                var content = new byte[fileArchivo.ContentLength];
                fileArchivo.InputStream.Read(content, 0, fileArchivo.ContentLength);
                string CODIGO_UNICO = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                string Nombreencriptado = CODIGO_UNICO + System.IO.Path.GetExtension(fileArchivo.FileName).ToString();
                Recursos.Clases.Css_Ruta.MisRuta MisRutas = new Recursos.Clases.Css_Ruta.MisRuta();
                MisRutas = Recursos.Clases.Css_Ruta.Ruta_TemporalI();
                var ruta_link = @"/" + MisRutas.RUTA + Nombreencriptado;
                string ruta = MisRutas.RUTA_COMPLETA + Nombreencriptado;
                fileArchivo.SaveAs(ruta);
                //string RutaBase = Recursos.Clases.Css_Ruta.Ruta_Temporal() + Nombreencriptado; 
                  ProcesarHojaCalculo(ruta, ID_SUCURSAL, ref auditoria); 
               
            
            }
            else
            {
                auditoria.Rechazar("No se encontró ningun archivo, seleccione alguno");
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }


        private void ProcesarHojaCalculo(string rutaBase, long ID_SUCURSAL, ref Cls_Ent_Auditoria auditoria)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(rutaBase) == true)
                {
                    auditoria.Rechazar("No se encontró ningun archivo, seleccione alguno");
                }
                List<Cls_Ent_Producto> items = new List<Cls_Ent_Producto>();
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(rutaBase, true))
                {
                    var user = HttpContext.Request.Cookies["MEF-CODUSER-MIGUEL"];
                    Workbook libro = document.WorkbookPart.Workbook;
                    IEnumerable<Sheet> hojas = libro.Descendants<Sheet>();
                    string hojaId = hojas.First(s => s.LocalName == @"sheet").Id;
                    WorksheetPart hoja = (WorksheetPart)document.WorkbookPart.GetPartById(hojaId);
                    SharedStringTable tabla = document.WorkbookPart.SharedStringTablePart.SharedStringTable;
                    //items = ProcesarHojaCalculo(hoja.Worksheet, tabla, Tipo);
                    //if (Tipo == "1")
                    //{
                    //    C_BancoPregunta_DTO ent_banco = new C_BancoPregunta_DTO();
                    //    int registro = items.Count();
                    //    ent_banco.NR_REGISTRO = registro;
                    //    ent_banco.USU_INGRESO = user.Value.ToString();
                    //    ent_banco.IP_PC = Request.UserHostAddress.ToString().Trim();
                    //    C_BancoPregunta_DTO ent = new BancoPreguntaRepositorio().GrabarProcesoCarga(ent_banco);
                    //    foreach (var item in items)
                    //    {
                    //        PreguntasDTO ent_P = new PreguntasDTO();
                    //        ent_P.CODIGO_PUESTO = item.CODIGO_PUESTO;
                    //        ent_P.CODIGO_PREGUNTA = item.CODIGO_PREGUNTA;
                    //        ent_P.ID_NIVEL = item.ID_NIVEL;
                    //        ent_P.ID_TEMA = item.ID_TEMA;
                    //        ent_P.TIPO_PREGUNTA = item.TIPO_PREGUNTA;
                    //        ent_P.ENUNCIADO = item.ENUNCIADO;
                    //        ent_P.USU_INGRESO = user.Value.ToString();
                    //        ent_P.IP_PC = Request.UserHostAddress.ToString().Trim();
                    //        PreguntasDTO XX = new BancoPreguntaRepositorio().CargaPreguntaTemporal(ent_P, ent.ID_CONTROL_CARGA);
                    //    }
                    //}
                    //else
                    //{
                    //    foreach (var item in items)
                    //    {
                    //        PreguntasDTO ent_P = new PreguntasDTO();
                    //        ent_P.CODIGO_PREGUNTA = item.CODIGO_PREGUNTA;
                    //        ent_P.ID_ID_ALTERNATIVA = item.ID_ID_ALTERNATIVA;
                    //        ent_P.ENUNCIADO = item.ENUNCIADO;
                    //        ent_P.RESPUESTA = item.RESPUESTA;
                    //        ent_P.USU_INGRESO = user.Value.ToString();
                    //        ent_P.IP_PC = Request.UserHostAddress.ToString().Trim();
                    //        PreguntasDTO XX = new BancoPreguntaRepositorio().CargaAlternativaTemporal(ent_P);
                    //    }
                    //}
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
        //public List<PreguntasDTO> ProcesarHojaCalculo(Worksheet hoja, SharedStringTable tabla, string Tipo)
        //{
        //    try
        //    {
        //        IEnumerable<Row> registros = this.GetRowsGreaterEqualThan(hoja, 2);
        //        List<PreguntasDTO> items = new List<PreguntasDTO>();
        //        int reg = 0;
        //        //   var listaPersona = _personaService.GetTodos()
        //        string NOM = "";
        //        List<ListaCombosDTO> ListaNivel = new BancoPreguntaRepositorio().ListaNivelPregunta(NOM);
        //        List<ListaCombosDTO> ListaTema = new BancoPreguntaRepositorio().ListaTemaPregunta(NOM);
        //        List<ListaCombosDTO> ListaPuesto = new BancoPreguntaRepositorio().ListaPuesto_Mpp(NOM);
        //        List<ListaCombosDTO> ListaPuestoTemp = new BancoPreguntaRepositorio().ListaPuesto_Temporal(NOM);
        //        List<PreguntasDTO> ListaAlternativaTemp = new BancoPreguntaRepositorio().ListaAlternativa_Temporal(NOM);

        //        foreach (Row registro in registros)
        //        {
        //            reg++;
        //            String[] valores = GetRowValue(tabla, registro);
        //            if (valores.Length == 0) continue;
        //            if (Tipo == "1")
        //            {
        //                if (valores.Length != 7)
        //                {
        //                    throw new Exception("El archivo cargado no tiene el número de columnas requeridas, El formato debe contener siete columnas Código Pregunta,Código Puesto, Tema General, Nivel, Tipo Pregunta, Pregunta y Autor</br>NO acepta campos vacios,la columna Autor puede ser rellenado con (0)");
        //                }
        //            }
        //            else
        //            {
        //                if (valores.Length != 4)
        //                {
        //                    throw new Exception("El archivo cargado no tiene el número de columnas requeridas, El formato debe contener tres columnas Código Pregunta, Correlativo, Alternativa y Respuesta.");
        //                }
        //            }
        //            if (Tipo == "1")
        //            {
        //                string cod_pregunta = Convert.ToString(valores[0]);
        //                if (!string.IsNullOrEmpty(cod_pregunta))
        //                {
        //                    cod_pregunta = cod_pregunta.Trim();
        //                }
        //                if (ListaAlternativaTemp != null)
        //                {
        //                    var Objeto_alternativas_Temp = ListaAlternativaTemp.Where(p => p.CODIGO_PREGUNTA == cod_pregunta.ToUpper()).FirstOrDefault();
        //                    if (Objeto_alternativas_Temp != null)
        //                    {
        //                        throw new Exception("El Código de Pregunta <b>" + cod_pregunta + "</b> ya se encuentra registrado.");
        //                    }
        //                }
        //                string cod_puesto = Convert.ToString(valores[1]);
        //                if (!string.IsNullOrEmpty(cod_puesto))
        //                {
        //                    cod_puesto = cod_puesto.Trim();
        //                }
        //                var Objeto_Puesto = ListaPuesto.Where(p => p.DESCRIPCION == cod_puesto.ToUpper()).FirstOrDefault();
        //                if (Objeto_Puesto == null)
        //                {
        //                    throw new Exception("El Código de Puesto <b>" + cod_puesto + "</b> no se encuentra registrado en el Sistema.");
        //                }
        //                else
        //                {
        //                    cod_puesto = Objeto_Puesto.DESCRIPCION;
        //                }
        //                if (ListaPuestoTemp != null) { 
        //                var Objeto_Puesto_Temp = ListaPuestoTemp.Where(p => p.DESCRIPCION == cod_puesto.ToUpper()).FirstOrDefault();
        //                if (Objeto_Puesto_Temp != null)
        //                {
        //                    throw new Exception("El Código de Puesto <b>" + cod_puesto + "</b> ya fue registrado.");
        //                }
        //                }
        //                string tema_general = Convert.ToString(valores[2]);
        //                if (!string.IsNullOrEmpty(tema_general))
        //                {
        //                    tema_general = tema_general.Trim();
        //                }
        //                var Objeto_Tema = ListaTema.Where(p => p.DESCRIPCION == tema_general.ToUpper()).FirstOrDefault();
        //                if (Objeto_Tema == null)
        //                {
        //                    throw new Exception("El tema <b>" + tema_general + "</b> no se encuentra registrado en el Sistema.");
        //                }
        //                else {
        //                    tema_general = Objeto_Tema.ID;
        //                }

        //                string nivel = Convert.ToString(valores[3]);
        //                if (!string.IsNullOrEmpty(nivel))
        //                {
        //                    nivel = nivel.Trim();
        //                }
        //                var Objeto_Nivel = ListaNivel.Where(p => p.DESCRIPCION == nivel.ToUpper()).FirstOrDefault();
        //                if (Objeto_Nivel == null)
        //                {
        //                    throw new Exception("El nivel <b>" + nivel + "</b> no se encuentra registrado en el Sistema.");
        //                }
        //                else
        //                {
        //                    nivel = Objeto_Nivel.ID;
        //                }

        //                string tipo_pregunta = Convert.ToString(valores[4]);
        //                int resultado = 0;
        //                bool esNumerico = Int32.TryParse(tipo_pregunta, out resultado);
        //                if (!esNumerico)
        //                {
        //                    throw new Exception("Ingresar correctamente la columna <b>TIPO PREGUNTA</b><br> (1) Opción Múltiple - (2) Verdadero/Falso");
        //                }
        //                if (tipo_pregunta !="1" && tipo_pregunta != "2")
        //                {
        //                    throw new Exception("Ingresar correctamente la columna <b>TIPO PREGUNTA</b><br> (1) Opción Múltiple - (2) Verdadero/Falso");
        //                }
        //                string pregunta = Convert.ToString(valores[5]);
        //                if (!string.IsNullOrEmpty(pregunta))
        //                {
        //                    pregunta = pregunta.Trim();
        //                }
        //                string autor = Convert.ToString(valores[6]);
        //                if (!string.IsNullOrEmpty(autor))
        //                {
        //                    autor = autor.Trim();
        //                }
        //                var item = new PreguntasDTO();
        //                item.CODIGO_PUESTO = cod_puesto;
        //                item.CODIGO_PREGUNTA = cod_pregunta;
        //                item.ID_NIVEL = int.Parse(nivel);
        //                item.ID_TEMA = int.Parse(tema_general);
        //                item.TIPO_PREGUNTA = tipo_pregunta;
        //                item.ENUNCIADO = pregunta;
        //                item.AUTOR = autor;
        //                items.Add(item);
        //            }
        //            else
        //            {
        //                string cod_pregunta = Convert.ToString(valores[0]);
        //                if (!string.IsNullOrEmpty(cod_pregunta))
        //                {
        //                    cod_pregunta = cod_pregunta.Trim();
        //                }
        //                var Objeto_Puesto_Temp = ListaPuestoTemp.Where(p => p.CODIGO_PREGUNTA == cod_pregunta.ToUpper()).FirstOrDefault();
        //                if (Objeto_Puesto_Temp == null)
        //                {
        //                    throw new Exception("El Código de Pregunta <b>" + cod_pregunta + "</b> no se encuentra asignado a una pregunta temporal.");
        //                }
        //                if (ListaAlternativaTemp != null) { 
        //                var Objeto_alternativas_Temp = ListaAlternativaTemp.Where(p => p.CODIGO_PREGUNTA == cod_pregunta.ToUpper()).FirstOrDefault();
        //                if (Objeto_alternativas_Temp != null)
        //                {
        //                    throw new Exception("Las alternativas del Código de Pregunta <b>" + cod_pregunta + "</b> ya fueron registrados.");
        //                }
        //                }
        //                string correlativo = Convert.ToString(valores[1]);
        //                int resultado_ = 0;
        //                bool esNumerico_ = Int32.TryParse(correlativo, out resultado_);
        //                if (!esNumerico_)
        //                {
        //                    throw new Exception("Ingresar correctamente la columna <b>CORRELATIVO</b> <br>  solo se permite valores numericos 1,2,3,4,5,6 correlativo de la pregunta.");
        //                }
        //                string alternativa = Convert.ToString(valores[2]);
        //                if (!string.IsNullOrEmpty(alternativa))
        //                {
        //                    alternativa = alternativa.Trim();
        //                }
        //                string respuesta = Convert.ToString(valores[3]);
        //                int resultado = 0;
        //                bool esNumerico = Int32.TryParse(respuesta, out resultado);
        //                if (!esNumerico)
        //                {
        //                    throw new Exception("Ingresar correctamente la columna <b>RESPUESTA</b> <br>  (1) Respuesta Correcta - (0) No es la respuesta");
        //                }
        //                if (respuesta != "1" && respuesta != "0")
        //                {
        //                    throw new Exception("Ingresar correctamente la columna <b>RESPUESTA</b> <br>  (1) Respuesta Correcta - (0) No es la respuesta");
        //                }
        //                var item = new PreguntasDTO();
        //                item.CODIGO_PREGUNTA = cod_pregunta;
        //                item.ID_ID_ALTERNATIVA = int.Parse(correlativo);
        //                item.ENUNCIADO = alternativa.ToUpper();// int.Parse(nivel);
        //                item.RESPUESTA = respuesta;
        //                items.Add(item);
        //            }
        //        }

        //        if (items.Count <= 0)
        //        {
        //            throw new Exception("No hay registros válidos para el registro");
        //        }
        //        return items;
        //    }
        //    catch (Exception ex)
        //    {
        //       // Log.CreateLogger(ex.Message);
        //        throw new Exception(ex.Message, ex.InnerException);
        //    }
        //}

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
