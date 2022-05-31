using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace App_Ventas.Areas.Recursiva.Clases
{
    public class CuTexto
    {
        public string ExtraerContenidoEntreTagString(string cadena, int posicion, string nombreInicio, string nombreFin, StringComparison reglaComparacion = StringComparison.Ordinal)
        {
            string respuesta = "";
            int posicionInicio = cadena.IndexOf(nombreInicio, posicion, reglaComparacion);
            if (posicionInicio > -1)
            {
                posicionInicio += nombreInicio.Length;
                if (nombreFin == null || nombreFin == "")
                    respuesta = cadena.Substring(posicionInicio, cadena.Length - posicionInicio);
                else
                {
                    int posicionFin = cadena.IndexOf(nombreFin, posicionInicio, reglaComparacion);
                    if (posicionFin > -1)
                        respuesta = cadena.Substring(posicionInicio, posicionFin - posicionInicio);
                }

            }

            return respuesta;
        }

        public string[] ExtraerContenidoEntreTag(string cadena, int posicion, string nombreInicio, string nombreFin, StringComparison reglaComparacion = StringComparison.Ordinal)
        {
            string[] arrRespuesta = null;
            int posicionInicio = cadena.IndexOf(nombreInicio, posicion, reglaComparacion);
            if (posicionInicio > -1)
            {
                posicionInicio += nombreInicio.Length;
                if (nombreFin == null || nombreFin == "")
                {
                    arrRespuesta = new string[2];
                    arrRespuesta[0] = cadena.Length.ToString();
                    arrRespuesta[1] = cadena.Substring(posicionInicio, cadena.Length - posicionInicio);
                }
                else
                {
                    int posicionFin = cadena.IndexOf(nombreFin, posicionInicio, reglaComparacion);
                    if (posicionFin > -1)
                    {
                        posicion = posicionFin + nombreFin.Length;
                        arrRespuesta = new string[2];
                        arrRespuesta[0] = posicion.ToString();
                        arrRespuesta[1] = cadena.Substring(posicionInicio, posicionFin - posicionInicio);
                    }
                }
            }

            return arrRespuesta;
        }

        public bool ValidarSoloNumero(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;
            bool esValido = false;
            int nCaracteres = valor.Length;
            int caracter = valor[0];
            // números => 0 = 48 y 9 = 57
            if (caracter > 47 && caracter < 58)
            {
                esValido = true;
                int i;
                for (i = 1; i < nCaracteres; i++)
                {
                    caracter = valor[i];
                    if (caracter < 48 || caracter > 59)
                    {
                        esValido = false;
                        break;
                    }
                }
            }
            return esValido;
        }

        public async Task<string> GuardarTexto(string nombreCompletoArchivo, string cadena, bool saltoLinea = true, bool anexarDatos = true)
        {
            if (string.IsNullOrWhiteSpace(nombreCompletoArchivo)) return "2~El parámetro nombreCompletoArchivo no debe ser nulo o vacío";
            string respuesta = "";
            try
            {
                string directorioArchivo = Path.GetDirectoryName(nombreCompletoArchivo);
                if (Directory.Exists(directorioArchivo))
                {
                    using (StreamWriter sw = new StreamWriter(nombreCompletoArchivo, anexarDatos, Encoding.Unicode))
                    {
                        if (saltoLinea)
                        {
                            await sw.WriteLineAsync(cadena);
                        }
                        else
                        {
                            await sw.WriteAsync(cadena);
                        }
                    }
                }
                else
                    respuesta = "2~No existe el directorio " + directorioArchivo;
            }
            catch (Exception ex)
            {
                respuesta = "3~" + ex.Message;
            }
            return respuesta;
        }

    }
}