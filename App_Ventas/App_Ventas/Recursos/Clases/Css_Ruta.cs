using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace App_Ventas.Recursos.Clases
{
    public static class Css_Ruta
    {
        public class MisRuta
        {
            public string RUTA_COMPLETA { get; set; }
            public string RUTA { get; set; }
        }

   
        public static string Ruta_Temporal()
        {
            string ruta = "";
            ruta = ConfigurationManager.AppSettings["Servidor_Temporal"].ToString();
            if (ruta == "")
            {
                ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Recursos\Temporales\");
            }
            return ruta;
        }

       
        public static string Ruta_Logo()
        {
            string ruta = "";
            ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Recursos\Logo_Empresa\"); 
            return ruta;
        }


        public static string Ruta_ImagenProducto()
        {
            string ruta = "";
            ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Recursos\ImagenProducto\");
            return ruta;
        }


        public static MisRuta Ruta_TemporalI()
        {
            MisRuta Mir = new MisRuta();

            Mir.RUTA_COMPLETA = AppDomain.CurrentDomain.BaseDirectory + @"Recursos/Temporales/";
            Mir.RUTA = @"Recursos/Temporales/";

            return Mir;
        }

    }
}