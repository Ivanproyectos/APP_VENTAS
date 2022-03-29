using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using Capa_Entidad; 

namespace App_Ventas.Recursos.Clases
{
    public class Css_GenerarPlantilla
    {

        public static string PlantillaCorreo_NotificarCredito()
        {
            var pathHtmlTemplate = Recursos.Clases.Css_Ruta.Ruta_PlantillaCorreo() + "NotificarCredito.html";
            StringBuilder emailHtml = new StringBuilder(File.ReadAllText(pathHtmlTemplate));

            return emailHtml.ToString();
        }

    }
}