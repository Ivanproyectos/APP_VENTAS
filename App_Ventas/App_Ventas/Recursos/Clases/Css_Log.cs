using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace App_Ventas.Recursos.Clases
{
    public static class Css_Log
    { 
        public static string Guardar(string texto)
        {
            string CODIGO_LOG = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal(); // string.Format("{0}{1}{2}{3}{4}{5}{6}", DateTime.Now.Day.ToString().PadLeft(2, '0'), DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Year, DateTime.Now.Hour.ToString().PadLeft(2, '0'), DateTime.Now.Minute.ToString().PadLeft(2, '0'), DateTime.Now.Second.ToString().PadLeft(2, '0'), DateTime.Now.Millisecond.ToString().PadLeft(2, '0'));
            string Milog = Generar_RutaLog(CODIGO_LOG, 1);
            File.Create(Milog).Close();
            TextWriter tw = new StreamWriter(Milog);
            tw.WriteLine(texto);
            tw.Close();
            return CODIGO_LOG;
        }

        public static string Generar_RutaLog(string CODIGO_LOG, int cuenta)
        {
            string Milog = AppDomain.CurrentDomain.BaseDirectory + "Recursos/Log/" + CODIGO_LOG + "Log.txt";
            if (File.Exists(Milog))
            {
                CODIGO_LOG = CODIGO_LOG + cuenta.ToString();
                Milog = Generar_RutaLog(CODIGO_LOG, cuenta++);
            }
            return Milog;
        }


        public static string Mensaje(string CODIGO_LOG)
        {
            string Mensaje_salida = ConfigurationManager.AppSettings["Mensaje_Log"].ToString() + "\r\n" + " Su código de error es: " + CODIGO_LOG;
            return Mensaje_salida;
        }

        public static string Mensaje2()
        {
            string Mensaje_salida = "\r\n" + "\r\n" + ConfigurationManager.AppSettings["Mensaje_Log"].ToString();
            return Mensaje_salida;
        }

    }
}