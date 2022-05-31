using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using App_Ventas.Areas.Recursiva.Models;
using Capa_Entidad;
using System.Text.RegularExpressions;

namespace App_Ventas.Areas.Recursiva.Clases
{
    public class ConsultaSunat
    {
        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }


        private Cls_Ent_SunatRuc ObtenerDatos(string contenidoHTML)
        {
            CuTexto oCuTexto = new CuTexto();
            Cls_Ent_SunatRuc oCls_Ent_SunatRuc = new Cls_Ent_SunatRuc();
            string nombreInicio = "<HEAD><TITLE>";
            string nombreFin = "</TITLE></HEAD>";
            string contenidoBusqueda = oCuTexto.ExtraerContenidoEntreTagString(contenidoHTML, 0, nombreInicio, nombreFin);
            if (contenidoBusqueda == ".:: Pagina de Mensajes ::.")
            {
                nombreInicio = "<p class=\"error\">";
                nombreFin = "</p>";
                oCls_Ent_SunatRuc.TipoRespuesta = 2;
                oCls_Ent_SunatRuc.MensajeRespuesta = oCuTexto.ExtraerContenidoEntreTagString(contenidoHTML, 0, nombreInicio, nombreFin);
            }
            else if (contenidoBusqueda == ".:: Pagina de Error ::.")
            {
                nombreInicio = "<p class=\"error\">";
                nombreFin = "</p>";
                oCls_Ent_SunatRuc.TipoRespuesta = 3;
                oCls_Ent_SunatRuc.MensajeRespuesta = oCuTexto.ExtraerContenidoEntreTagString(contenidoHTML, 0, nombreInicio, nombreFin);
            }
            else
            {
                oCls_Ent_SunatRuc.TipoRespuesta = 2;
                nombreInicio = "<div class=\"list-group\">";
                nombreFin = "<div class=\"panel-footer text-center\">";
                contenidoBusqueda = oCuTexto.ExtraerContenidoEntreTagString(contenidoHTML, 0, nombreInicio, nombreFin);
                if (contenidoBusqueda == "")
                {
                    nombreInicio = "<strong>";
                    nombreFin = "</strong>";
                    oCls_Ent_SunatRuc.MensajeRespuesta = oCuTexto.ExtraerContenidoEntreTagString(contenidoHTML, 0, nombreInicio, nombreFin);
                    if (oCls_Ent_SunatRuc.MensajeRespuesta == "")
                        oCls_Ent_SunatRuc.MensajeRespuesta = "No se encuentra las cabeceras principales del contenido HTML";
                }
                else
                {
                    contenidoHTML = contenidoBusqueda;
                    oCls_Ent_SunatRuc.MensajeRespuesta = "Mensaje del inconveniente no especificado";
                    nombreInicio = "<h4 class=\"list-group-item-heading\">";
                    nombreFin = "</h4>";
                    int resultadoBusqueda = contenidoHTML.IndexOf(nombreInicio, 0, StringComparison.OrdinalIgnoreCase);
                    if (resultadoBusqueda > -1)
                    {
                        // Modificar cuando el estado del Contribuyente es "BAJA DE OFICIO", porque se agrega un elemento con clase "list-group-item"
                        resultadoBusqueda += nombreInicio.Length;
                        string[] arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, resultadoBusqueda,
                            nombreInicio, nombreFin);
                        if (arrResultado != null)
                        {
                            oCls_Ent_SunatRuc.RazonSocial = arrResultado[1];
                            //Match Ruc = Regex.Match(oCls_Ent_SunatRuc.RazonSocial, "(\\d+)"); // obtener solo ruc
                            oCls_Ent_SunatRuc.NumeroRUC = oCls_Ent_SunatRuc.RazonSocial.Substring(0, 11); //obtener solo ruc
                            oCls_Ent_SunatRuc.RazonSocial = oCls_Ent_SunatRuc.RazonSocial.Remove(0, 14); // remover ruc y los espacion blanco
                            // Tipo Contribuyente
                            nombreInicio = "<p class=\"list-group-item-text\">";
                            nombreFin = "</p>";
                            arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                nombreInicio, nombreFin);
                            if (arrResultado != null)
                            {
                                oCls_Ent_SunatRuc.TipoContribuyente = arrResultado[1];

                                // Nombre Comercial
                                if (oCls_Ent_SunatRuc.TipoContribuyente == "PERSONA NATURAL SIN NEGOCIO")
                                    arrResultado[0] = "1425";

                                arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                    nombreInicio, nombreFin);
                                if (arrResultado != null)
                                {
                                    oCls_Ent_SunatRuc.NombreComercial = arrResultado[1].Replace("\r\n", "").Replace("\t", "").Trim();

                                    // Fecha de Inscripción
                                    arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                        nombreInicio, nombreFin);
                                    if (arrResultado != null)
                                    {
                                        oCls_Ent_SunatRuc.FechaInscripcion = arrResultado[1];

                                        // Fecha de Inicio de Actividades: 
                                        arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                            nombreInicio, nombreFin);
                                        if (arrResultado != null)
                                        {
                                            oCls_Ent_SunatRuc.FechaInicioActividades = arrResultado[1];

                                            // Estado del Contribuyente
                                            arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                            nombreInicio, nombreFin);
                                            if (arrResultado != null)
                                            {
                                                oCls_Ent_SunatRuc.EstadoContribuyente = arrResultado[1].Trim();

                                                // Condición del Contribuyente
                                                arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                    nombreInicio, nombreFin);
                                                if (arrResultado != null)
                                                {
                                                    oCls_Ent_SunatRuc.CondicionContribuyente = arrResultado[1].Trim();

                                                    // Domicilio Fiscal
                                                    arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                        nombreInicio, nombreFin);
                                                    if (arrResultado != null)
                                                    {
                                                        string Direccion = arrResultado[1].Trim();
                                                        var ArryDomicilio = Direccion.Split('-');
                                                        string Departamento = ArryDomicilio[0].TrimEnd();
                                                        //string Provincia = ArryDomicilio[1].Trim();
                                                        //string Distrito = ArryDomicilio[2].Trim();

                                                        oCls_Ent_SunatRuc.DomicilioFiscal = Departamento;
                                                        // Actividad(es) Económica(s)
                                                        nombreInicio = "<tbody>";
                                                        nombreFin = "</tbody>";
                                                        arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                            nombreInicio, nombreFin);
                                                        if (arrResultado != null)
                                                        {
                                                            oCls_Ent_SunatRuc.ActividadesEconomicas = arrResultado[1].Replace("\r\n", "").Replace("\t", "").Trim();

                                                            // Comprobantes de Pago c/aut. de impresión (F. 806 u 816)
                                                            arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                                nombreInicio, nombreFin);
                                                            if (arrResultado != null)
                                                            {
                                                                oCls_Ent_SunatRuc.ComprobantesPago = arrResultado[1].Replace("\r\n", "").Replace("\t", "").Trim();

                                                                // Sistema de Emisión Electrónica
                                                                arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                                    nombreInicio, nombreFin);
                                                                if (arrResultado != null)
                                                                {
                                                                    oCls_Ent_SunatRuc.SistemaEmisionComprobante = arrResultado[1].Replace("\r\n", "").Replace("\t", "").Trim();

                                                                    // Afiliado al PLE desde
                                                                    nombreInicio = "<p class=\"list-group-item-text\">";
                                                                    nombreFin = "</p>";
                                                                    arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                                        nombreInicio, nombreFin);
                                                                    if (arrResultado != null)
                                                                    {
                                                                        oCls_Ent_SunatRuc.AfiliadoPLEDesde = arrResultado[1];

                                                                        // Padrones 
                                                                        nombreInicio = "<tbody>";
                                                                        nombreFin = "</tbody>";
                                                                        arrResultado = oCuTexto.ExtraerContenidoEntreTag(contenidoHTML, Convert.ToInt32(arrResultado[0]),
                                                                            nombreInicio, nombreFin);
                                                                        if (arrResultado != null)
                                                                        {
                                                                            oCls_Ent_SunatRuc.Padrones = arrResultado[1].Replace("\r\n", "").Replace("\t", "").Trim();
                                                                        }
                                                                    }

                                                                    oCls_Ent_SunatRuc.TipoRespuesta = 1;
                                                                    oCls_Ent_SunatRuc.MensajeRespuesta = "Ok";
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return oCls_Ent_SunatRuc;
        }

        public async Task<Cls_Ent_SunatRuc> ConsultarRuc(string ruc)
        {
            int tipoRespuesta = 2;
            string mensajeRespuesta;
            Cls_Ent_SunatRuc oCls_Ent_SunatRuc = new Cls_Ent_SunatRuc();
            CuTexto oCuTexto = new CuTexto();
            Stopwatch oCronometro = new Stopwatch();
            //oCronometro.Start();
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler controladorMensaje = new HttpClientHandler();
            controladorMensaje.CookieContainer = cookies;
            controladorMensaje.UseCookies = true;
            using (HttpClient cliente = new HttpClient(controladorMensaje))
            {
                cliente.DefaultRequestHeaders.Add("Host", "e-consultaruc.sunat.gob.pe");
                cliente.DefaultRequestHeaders.Add("sec-ch-ua",
                    " \" Not A;Brand\";v=\"99\", \"Chromium\";v=\"90\", \"Google Chrome\";v=\"90\"");
                cliente.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                cliente.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
                cliente.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
                cliente.DefaultRequestHeaders.Add("Sec-Fetch-Site", "none");
                cliente.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                cliente.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                cliente.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
                                                       SecurityProtocolType.Tls12;
                await Task.Delay(100);

                string url = "https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias";
                using (HttpResponseMessage resultadoConsulta = await cliente.GetAsync(new Uri(url)))
                {
                    if (resultadoConsulta.IsSuccessStatusCode)
                    {
                        await Task.Delay(100);
                        cliente.DefaultRequestHeaders.Remove("Sec-Fetch-Site");

                        cliente.DefaultRequestHeaders.Add("Origin", "https://e-consultaruc.sunat.gob.pe");
                        cliente.DefaultRequestHeaders.Add("Referer", url);
                        cliente.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");

                        string numeroDNI = "12345678"; // cualquier número DNI pero que exista en SUNAT.
                        var lClaveValor = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("accion", "consPorTipdoc"),
                            new KeyValuePair<string, string>("razSoc", ""),
                            new KeyValuePair<string, string>("nroRuc", ""),
                            new KeyValuePair<string, string>("nrodoc", numeroDNI), 
                            new KeyValuePair<string, string>("contexto", "ti-it"),
                            new KeyValuePair<string, string>("modo", "1"),
                            new KeyValuePair<string, string>("search1", ""),
                            new KeyValuePair<string, string>("rbtnTipo", "2"),
                            new KeyValuePair<string, string>("tipdoc", "1"),
                            new KeyValuePair<string, string>("search2", numeroDNI),
                            new KeyValuePair<string, string>("search3", ""),
                            new KeyValuePair<string, string>("codigo", ""),
                        };
                        FormUrlEncodedContent contenido = new FormUrlEncodedContent(lClaveValor);

                        url = "https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias";
                        using (HttpResponseMessage resultadoConsultaRandom = await cliente.PostAsync(url, contenido))
                        {
                            if (resultadoConsultaRandom.IsSuccessStatusCode)
                            {
                                await Task.Delay(100);
                                string contenidoHTML = await resultadoConsultaRandom.Content.ReadAsStringAsync();
                                string numeroRandom = oCuTexto.ExtraerContenidoEntreTagString(contenidoHTML, 0, "name=\"numRnd\" value=\"", "\">");

                                lClaveValor = new List<KeyValuePair<string, string>>
                                {
                                    new KeyValuePair<string, string>("accion", "consPorRuc"),
                                    new KeyValuePair<string, string>("actReturn", "1"),
                                    new KeyValuePair<string, string>("nroRuc", ruc),
                                    new KeyValuePair<string, string>("numRnd", numeroRandom),
                                    new KeyValuePair<string, string>("modo", "1")
                                };

                                int cConsulta = 0;
                                int nConsulta = 3;
                                HttpStatusCode codigoEstado = HttpStatusCode.Unauthorized;
                                while (cConsulta < nConsulta && codigoEstado == HttpStatusCode.Unauthorized)
                                {
                                    contenido = new FormUrlEncodedContent(lClaveValor);
                                    using (HttpResponseMessage resultadoConsultaDatos =
                                    await cliente.PostAsync(url, contenido))
                                    {
                                        codigoEstado = resultadoConsultaDatos.StatusCode;
                                        if (resultadoConsultaDatos.IsSuccessStatusCode)
                                        {
                                            contenidoHTML = await resultadoConsultaDatos.Content.ReadAsStringAsync();
                                            contenidoHTML = WebUtility.HtmlDecode(contenidoHTML);

                                            #region Obtener los datos del RUC
                                            oCls_Ent_SunatRuc = ObtenerDatos(contenidoHTML);
                                            if (oCls_Ent_SunatRuc.TipoRespuesta == 1)
                                            {
                                                tipoRespuesta = 1;
                                                oCls_Ent_SunatRuc.MensajeRespuesta =
                                                    string.Format("Se realizó exitosamente la consulta del número de RUC {0}",
                                                        ruc);
                                                //auditoria.OBJETO = oCls_Ent_SunatRuc; 
                                            }
                                            else
                                            {
                                                tipoRespuesta = oCls_Ent_SunatRuc.TipoRespuesta;
                                                oCls_Ent_SunatRuc.MensajeRespuesta = string.Format(
                                                    "No se pudo realizar la consulta del número de RUC {0}.\r\nDetalle: {1}",
                                                    ruc,
                                                    oCls_Ent_SunatRuc.MensajeRespuesta);
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            mensajeRespuesta = await resultadoConsultaDatos.Content.ReadAsStringAsync();
                                            oCls_Ent_SunatRuc.MensajeRespuesta =
                                                string.Format(
                                                    "Ocurrió un inconveniente al consultar los datos del RUC {0}.\r\nDetalle:{1}",
                                                    ruc, mensajeRespuesta);
                                        }
                                    }

                                    cConsulta++;
                                }


                            }
                            else
                            {
                                mensajeRespuesta = await resultadoConsultaRandom.Content.ReadAsStringAsync();
                                mensajeRespuesta =
                                    string.Format(
                                        "Ocurrió un inconveniente al consultar el número random del RUC {0}.\r\nDetalle:{1}",
                                        ruc, mensajeRespuesta);
                            }
                        }
                    }
                    else
                    {
                        mensajeRespuesta = await resultadoConsulta.Content.ReadAsStringAsync();
                        mensajeRespuesta =
                            string.Format(
                                "Ocurrió un inconveniente al consultar la página principal {0}.\r\nDetalle:{1}",
                                ruc, mensajeRespuesta);
                    }
                }
            }
            return oCls_Ent_SunatRuc;
        }

    }
}