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
        public ActionResult ConsultaRuc(Cls_Ent_Cliente entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
            string DATA = "{pRucConsulta:" + entidad.NUMERO_DOCUMENTO + "}";
            string URL = "https://snirh.ana.gob.pe/consultaspide/wsgetSunat.asmx/consultaSunat";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-";
            request.ContentLength = DATA.Length;
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            requestWriter.Write(DATA);
            requestWriter.Close();

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                //var jsonStringResult = JsonConvert.SerializeObject(response);
                auditoria.OBJETO = response;
                responseReader.Close();
            }
            catch (Exception e)
            {
                auditoria.Rechazar("Servicio fuera de linea, ingrese datos manualmente.");
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);

        }


        public ActionResult CreateCharge()
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
            try
            {

                var resp = new ApiCulqi.Payments.Css_OnlinePay().OnlinePay_CreateCharge();
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
