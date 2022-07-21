using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Entidad.Base;
using Capa_Entidad.CargaExcel;
using Capa_Entidad.Administracion;
using Capa_Negocio.Inventario;
using Capa_Negocio.Listados_Combos; 
using App_Ventas.Areas.Inventario.Models;
using App_Ventas.Areas.Inventario.Repositorio;
using App_Ventas.Areas.Recursiva.Repositorio;
using App_Ventas.Areas.Ventas.Models;
using Capa_Entidad.Inventario;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Data;

namespace App_Ventas.Areas.Inventario.Controllers
{
    public class ImportarController : Controller
    {
        //
        // GET: /Inventario/Importar_Producto/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult View_Importar(int ID_SUCURSAL, string DESC_SUCURSAL)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            BuscarProductoModelView model = new BuscarProductoModelView();
            model.ID_SUCURSAL = ID_SUCURSAL;
            model.DESC_SUCURSAL = DESC_SUCURSAL;
            return View(model);
        }
        public ActionResult Importar_DescagarPlantilla()
        {
            return View();
        }

        public ActionResult Importar_CargarExcel(HttpPostedFileBase fileArchivo, FormCollection forms)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            if (fileArchivo != null)
            {
                    int ID_SUCURSAL = int.Parse(forms["IMP_ID_SUCURSAL"].ToString());
                    var content = new byte[fileArchivo.ContentLength];
                    fileArchivo.InputStream.Read(content, 0, fileArchivo.ContentLength);
                    string EXTENSION = Path.GetExtension(fileArchivo.FileName);
                    if (EXTENSION.Equals(".xls") || EXTENSION.Equals(".xlsx"))
                    {
                        string CODIGO_UNICO = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                        string Nombreencriptado = CODIGO_UNICO + System.IO.Path.GetExtension(fileArchivo.FileName).ToString();
                        Recursos.Clases.Css_Ruta.MisRuta MisRutas = new Recursos.Clases.Css_Ruta.MisRuta();
                        MisRutas = Recursos.Clases.Css_Ruta.Ruta_TemporalI();
                        var ruta_link = @"/" + MisRutas.RUTA + Nombreencriptado;
                        string ruta = MisRutas.RUTA_COMPLETA + Nombreencriptado;
                        fileArchivo.SaveAs(ruta);
                         new Css_Importar().Procesar_CargaMasiva(ruta, ID_SUCURSAL, ref auditoria);
                    }
                    else {
                        auditoria.Rechazar("La extensión del archivo debe ser (.xls) o (.xlsx)");
                    }
            }
            else
            {
                auditoria.Rechazar("No se encontró ningun archivo, seleccione alguno");
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

    

    }
}
