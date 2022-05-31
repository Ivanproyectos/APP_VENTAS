using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using ApiCulqi;
using System.Threading.Tasks;
using App_Ventas.Areas.Recursiva.Models;
using App_Ventas.Areas.Recursiva.Clases;
using System.Configuration; 

namespace App_Ventas.Areas.Recursiva.Controllers
{
    public class ServiciosWebController : Controller
    {
        //
        // GET: /Recursiva/ServiciosWeb/

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Service_ConsultaRuc(string Ruc)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                auditoria.Limpiar();
                ConsultaSunat Consulta = new ConsultaSunat();
                Cls_Ent_SunatRuc Sunat = await Consulta.ConsultarRuc(Ruc);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
                else {
                    auditoria.OBJETO = Sunat; 
                }
            }
            catch (Exception ex)
            {
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
            }

            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Service_ConsultaDni(string DNI)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                auditoria.Limpiar();
                string URL = "";
                string tokenApiDni = ConfigurationManager.AppSettings["TokenConsultaDni"];
                URL = String.Format("https://dniruc.apisperu.com/api/v1/dni/{0}?token={1}", DNI, tokenApiDni);
                var client = new WebClient();
                var content = client.DownloadString(URL);
                var JsonConvert = new JavaScriptSerializer();
                auditoria.OBJETO = JsonConvert.Serialize(content);
            }
            catch (Exception ex)
            {
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateCharge()
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
            try
            {

                var resp = new ApiCulqi.Payments.Css_Subscription().OnlinePay_CreateSubscription();
                auditoria.OBJETO = resp; 
            }
            catch (Exception e)
            {
                auditoria.Rechazar("Servicio fuera de linea, ingrese datos manualmente.");
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);

        }
        


    }
}
