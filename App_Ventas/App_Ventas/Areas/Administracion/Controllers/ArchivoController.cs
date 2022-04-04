using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad.Administracion;
using Capa_Entidad;
using Capa_Entidad.Base;
using System.Text;
using System.IO;


namespace App_Ventas.Areas.Administracion.Controllers
{
    public class ArchivoController : Controller
    {
        //
        // GET: /Administracion/Archivo/

        public ActionResult Guardar_Temporal_Archivo(HttpPostedFileBase MifileArchivo)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            if (MifileArchivo != null)
            {
                try
                {
                    var content = new byte[MifileArchivo.ContentLength];
                    MifileArchivo.InputStream.Read(content, 0, MifileArchivo.ContentLength);

                    //string CODIGO_UNICO = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();

                    string CODIGO_UNICO = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();

                    decimal tamanio = content.Length; // OBTENEMOS EL ARCHIVO EN BYTES
                    string Nombreencriptado = CODIGO_UNICO + System.IO.Path.GetExtension(MifileArchivo.FileName).ToString();

                    Recursos.Clases.Css_Ruta.MisRuta MisRutas = new Recursos.Clases.Css_Ruta.MisRuta();
                    MisRutas = Recursos.Clases.Css_Ruta.Ruta_TemporalI();
                    //var ruta_link = System.Configuration.ConfigurationManager.AppSettings["Nombre_Sistema"].ToString() + "/" + MisRutas.RUTA + Nombreencriptado;
                    var ruta_link = @"/" + MisRutas.RUTA + Nombreencriptado;
                    var ruta = MisRutas.RUTA_COMPLETA + Nombreencriptado;
                    MifileArchivo.SaveAs(ruta);

                    //Capa_Entidad.Base.Cls_Ent_Archivo entidad_archivo = new Capa_Entidad.Base.Cls_Ent_Archivo();
                    //entidad_archivo.CODIGO_ARCHIVO = CODIGO_UNICO;
                    //entidad_archivo.NOMBRE_ARCHIVO = System.IO.Path.GetFileName(MifileArchivo.FileName).ToString();
                    ////entidad_archivo.RUTA_ARCHIVO = ruta;
                    //entidad_archivo.RUTA_LINK = ruta_link;
                    //entidad_archivo.EXTENSION = System.IO.Path.GetExtension(MifileArchivo.FileName).ToString();

                    Capa_Entidad.Base.Cls_Ent_Archivo entidad_archivo = new Capa_Entidad.Base.Cls_Ent_Archivo();

                    entidad_archivo.NOMBRE_ARCHIVO = System.IO.Path.GetFileName(MifileArchivo.FileName).ToString();
                    //entidad_archivo.RUTA_ARCHIVO = ruta;
                    entidad_archivo.RUTA_LINK = ruta_link;
                    entidad_archivo.EXTENSION = System.IO.Path.GetExtension(MifileArchivo.FileName).ToString();
                    entidad_archivo.CODIGO_ARCHIVO = CODIGO_UNICO;
                    entidad_archivo.PESO_ARCHIVO = tamanio;
                    auditoria.OBJETO = entidad_archivo;
                    auditoria.EJECUCION_PROCEDIMIENTO = true;
                }
                catch (Exception ex)
                {

                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
            }
            else
            {
                auditoria.EJECUCION_PROCEDIMIENTO = false;
                auditoria.MENSAJE_SALIDA = "No se encontró el archivo";
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

    }
}
