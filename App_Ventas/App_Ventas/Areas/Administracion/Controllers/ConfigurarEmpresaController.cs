using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Entidad.Base; 
using Capa_Entidad.Administracion; 
using App_Ventas.Areas.Administracion.Models;
using App_Ventas.Areas.Administracion.Repositorio;
using App_Ventas.Recursos;

namespace App_Ventas.Areas.Administracion.Controllers
{
    public class ConfigurarEmpresaController : Controller
    {
        //
        // GET: /Administracion/ConfigurarEmpresa/

        public ActionResult Index()
        {
            Cls_Ent_configurarEmpresa entidad = new Cls_Ent_configurarEmpresa(); 
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            ConfiguracionEmpresaModelView model = new ConfiguracionEmpresaModelView();

            //model.Lista_Ubigeo = new List<SelectListItem>();
            //model.Lista_Ubigeo.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            using (Listado_CombosRepositorio RepositorioUbigeo = new Listado_CombosRepositorio())
            {
                model.Lista_Ubigeo = RepositorioUbigeo.Ubigeo_Listar(ref auditoria).Select(x => new SelectListItem()
                {
                    Text = x.DESC_UBIGEO,
                    Value = x.ID_UBIGEO.ToString()
                }).ToList();
                model.Lista_Ubigeo.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            using (ConfigurarEmpresaRepositorio repositorio= new ConfigurarEmpresaRepositorio())
            {
                entidad = repositorio.configurarEmpresa_Listar( ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
                else
                {
                    if (entidad != null)
                    {
                        entidad.Archivo_Logo = new Cls_Ent_Archivo();
                        entidad.Archivo_Isotipo = new Cls_Ent_Archivo(); 

                        model.ID_CONFIGURACION = entidad.ID_CONFIGURACION;
                        model.RUC = entidad.RUC;
                        model.RAZON_SOCIAL = entidad.RAZON_SOCIAL;
                        model.NOMBRE_COMERCIAL = entidad.NOMBRE_COMERCIAL;
                        model.URBANIZACION = entidad.URBANIZACION;
                        model.DIRECCION_FISCAL = entidad.DIRECCION_FISCAL;
                        model.COD_UBIGEO = entidad.COD_UBIGEO;
                        model.TELEFONO = entidad.TELEFONO;
                        model.CORREO = entidad.CORREO;
                        model.NOMBRE_IMPUESTO = entidad.NOMBRE_IMPUESTO;
                        model.IMPUESTO = Convert.ToString(entidad.IMPUESTO);
                        model.SIMBOLO_MONEDA = entidad.SIMBOLO_MONEDA;

                        model.FLG_IMPRIMIR = entidad.FLG_IMPRIMIR == 0? false:true ;
                        model.FLG_IMPUESTO = entidad.FLG_IMPUESTO == 0 ? false : true;

                        if (entidad.CODIGO_ARCHIVO_LOGO != null)
                        {
                            entidad.Archivo_Logo.CODIGO_ARCHIVO = entidad.CODIGO_ARCHIVO_LOGO; 
                            entidad.Archivo_Logo.EXTENSION = entidad.EXTENSION_ARCHIVO_LOGO;
                            model.Archivo_Logo = entidad.Archivo_Logo; 
                        }

                        if (entidad.CODIGO_ARCHIVO_ISOTIPO != null)
                        {
                        
                            entidad.Archivo_Isotipo.CODIGO_ARCHIVO = entidad.CODIGO_ARCHIVO_ISOTIPO; 
                            entidad.Archivo_Isotipo.EXTENSION =  entidad.EXTENSION_ARCHIVO_ISOTIPO;
                            model.Archivo_Isotipo = entidad.Archivo_Isotipo; 
                        }



                    }
                }
            }
            return View(model);
        }



        public ActionResult ConfigurarEmpresa_Insertar(Cls_Ent_configurarEmpresa entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            var IP = Recursos.Clases.Css_IP.ObtenerIp();
            try
            {
                using (ConfigurarEmpresaRepositorio Plantillarepositorio = new ConfigurarEmpresaRepositorio())
                {

                    string ruta_temporal_logo = "";
                    string ruta_temporal_isologo= "";

                    if (entidad.Archivo_Logo != null)
                    {
                        string _archivo = entidad.Archivo_Logo.CODIGO_ARCHIVO + entidad.Archivo_Logo.EXTENSION;
                        ruta_temporal_logo = Recursos.Clases.Css_Ruta.Ruta_Temporal() + @"" + _archivo;
                        string ruta_Logo = Recursos.Clases.Css_Ruta.Ruta_Logo() + @"" + _archivo;
                        System.IO.File.Create(ruta_Logo).Close();
                        System.IO.File.WriteAllBytes(ruta_Logo, System.IO.File.ReadAllBytes(ruta_temporal_logo));

                    }

                    if (entidad.Archivo_Isotipo != null)
                    {
                        string _archivo = entidad.Archivo_Isotipo.CODIGO_ARCHIVO + entidad.Archivo_Isotipo.EXTENSION;
                        ruta_temporal_isologo = Recursos.Clases.Css_Ruta.Ruta_Temporal() + @"" + _archivo;
                        string ruta_Logo = Recursos.Clases.Css_Ruta.Ruta_Logo() + @"" + _archivo;
                        System.IO.File.Create(ruta_Logo).Close();
                        System.IO.File.WriteAllBytes(ruta_Logo, System.IO.File.ReadAllBytes(ruta_temporal_isologo));

                    }

                    entidad.IP_CREACION = IP;
                    Plantillarepositorio.ConfigurarEmpresa_Insertar(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        if (!auditoria.RECHAZAR)
                        {
                            if (System.IO.File.Exists(ruta_temporal_logo))
                                System.IO.File.Delete(ruta_temporal_logo);

                            if (System.IO.File.Exists(ruta_temporal_isologo))
                                System.IO.File.Delete(ruta_temporal_isologo);  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ConfigurarEmpresa_Actualizar(Cls_Ent_configurarEmpresa entidad)
        {
            Capa_Entidad.Cls_Ent_Auditoria auditoria = new Capa_Entidad.Cls_Ent_Auditoria();
            try
            {
                using (ConfigurarEmpresaRepositorio Plantillarepositorio = new ConfigurarEmpresaRepositorio())
                {

                    string ruta_temporal_logo = "";
                    string ruta_temporal_isologo= "";

                    if (entidad.Archivo_Logo != null)
                    {
                        string _archivo = entidad.Archivo_Logo.CODIGO_ARCHIVO + entidad.Archivo_Logo.EXTENSION;
                        ruta_temporal_logo = Recursos.Clases.Css_Ruta.Ruta_Temporal() + @"" + _archivo;
                        string ruta_Logo = Recursos.Clases.Css_Ruta.Ruta_Logo() + @"" + _archivo;
                        System.IO.File.Create(ruta_Logo).Close();
                        System.IO.File.WriteAllBytes(ruta_Logo, System.IO.File.ReadAllBytes(ruta_temporal_logo));

                    }

                    if (entidad.Archivo_Isotipo != null)
                    {
                        string _archivo = entidad.Archivo_Isotipo.CODIGO_ARCHIVO + entidad.Archivo_Isotipo.EXTENSION;
                        ruta_temporal_isologo = Recursos.Clases.Css_Ruta.Ruta_Temporal() + @"" + _archivo;
                        string ruta_Logo = Recursos.Clases.Css_Ruta.Ruta_Logo() + @"" + _archivo;
                        System.IO.File.Create(ruta_Logo).Close();
                        System.IO.File.WriteAllBytes(ruta_Logo, System.IO.File.ReadAllBytes(ruta_temporal_isologo));

                    }

                    //entidad.IP_CREACION = IP;
                    Plantillarepositorio.ConfigurarEmpresa_Actualizar(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                    }
                    else
                    {
                        if (!auditoria.RECHAZAR)
                        {
                            if (System.IO.File.Exists(ruta_temporal_logo))
                                System.IO.File.Delete(ruta_temporal_logo);

                            if (System.IO.File.Exists(ruta_temporal_isologo))
                                System.IO.File.Delete(ruta_temporal_isologo);  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        

        




    }
}
