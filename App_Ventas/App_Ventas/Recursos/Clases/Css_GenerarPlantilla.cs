using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using Capa_Entidad; 
using Capa_Entidad.Ventas; 
using Capa_Entidad.Administracion; 

namespace App_Ventas.Recursos.Clases
{
    public class Css_GenerarPlantilla
    {

        public static string PlantillaCorreo_NotificarCredito(Cls_Ent_Ventas Ventas, Cls_Ent_configurarEmpresa Empresa , int Total_items )
        {
            var pathHtmlTemplate = Recursos.Clases.Css_Ruta.Ruta_PlantillaCorreo() + "NotificarCredito.html";
            StringBuilder emailHtml = new StringBuilder(File.ReadAllText(pathHtmlTemplate));
            string[] NombreSplit = Ventas.Cliente.NOMBRES_APE.Split(' ');
            emailHtml.Replace("$[NOMBRE]", NombreSplit[0]); // solo nombre
            emailHtml.Replace("$[NUMERO_COMPROBANTE]", Ventas.COD_COMPROBANTE);
            emailHtml.Replace("$[SUBTOTAL]", Empresa.SIMBOLO_MONEDA + " " +Ventas.SUB_TOTAL.ToString());
            emailHtml.Replace("$[DESCUENTO]",Empresa.SIMBOLO_MONEDA + " " + Ventas.DESCUENTO);
            emailHtml.Replace("$[NOMBRE_IMPUESTO]", Empresa.NOMBRE_IMPUESTO + "(" + Convert.ToInt32(Empresa.IMPUESTO).ToString() + "%)");
            emailHtml.Replace("$[IGV]", Empresa.SIMBOLO_MONEDA + " " + Ventas.IGV);
            emailHtml.Replace("$[TOTAL]", Empresa.SIMBOLO_MONEDA + " " + Ventas.TOTAL);
            emailHtml.Replace("$[ADELANTO]", Empresa.SIMBOLO_MONEDA + " " + Ventas.ADELANTO);
            emailHtml.Replace("$[DEBE]", Empresa.SIMBOLO_MONEDA + " " + Ventas.DEBE);
            emailHtml.Replace("$[TELEFONO]", Empresa.TELEFONO);
            emailHtml.Replace("$[DIRECCION]", Empresa.DIRECCION_FISCAL);
            emailHtml.Replace("$[UBIGEO]", Empresa.DESC_UBIGEO);
            emailHtml.Replace("$[EMPRESA]", Empresa.RAZON_SOCIAL);
            emailHtml.Replace("$[TOTAL_ITEMS]", Total_items.ToString()); 

            return emailHtml.ToString();
        }

    }
}