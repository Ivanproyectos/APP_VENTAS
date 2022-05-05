using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Entidad;
using System.IO;

namespace App_Ventas.Recursos.Descargas
{
    public partial class DescargarPlantillaExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DescargarExcel();
        }
        private void DescargarExcel()
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                string NOMBRE_ARCHIVO = "Plantilla_Producto.xlsx";
                string ruta_adjunto = Recursos.Clases.Css_Ruta.Ruta_Plantilla() + NOMBRE_ARCHIVO;
              
                
                byte[] bytes = File.ReadAllBytes(ruta_adjunto);
                Response.Clear();
                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", NOMBRE_ARCHIVO.Replace(",", "")));
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytes);
                Response.End();
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                //Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
        }


    }
}