using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Entidad;
using Capa_Entidad.Administracion;
using Capa_Entidad.Login;
using Capa_Entidad.Base;
using App_Ventas.Areas.Login.Repositorio;
using App_Ventas.Areas.Administracion.Repositorio;
using Capa_Token;
using System.Web.Security;
using WebMatrix.WebData;
//using System.Web.Http;

namespace App_Ventas.Controllers
{
    [RoutePrefix("sistema-ventas")]
    public class HomeController : Controller
    {
        [Route("{id}")]
        public ActionResult Index(string id)
        {
            try
            {
                id = id.Replace(" ", "+");
                string DESENCRIPTADO = Recursos.Clases.Css_Encriptar.Desencriptar(id);
                Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
                bool Valido = false;
                var cook = HttpContext.Request.Cookies["IP-CyberToken"];
                string Token = "";
                if (id != null && cook != null)
                {
                    Token = cook.Value;
                    using (LoginRepositorio repositorio = new LoginRepositorio())
                    {
                        int ID_PERFIL_USUARIO = int.Parse(DESENCRIPTADO);
                        int ID_USUARIO = Cls_Api_Token.Claim_ID_USUARIO(Token);
                        Cls_Ent_Usuario Usuario = new Cls_Ent_Usuario(); 
                        Usuario.ID_USUARIO = ID_USUARIO; 
                        Usuario.Perfil_Sucursal.ID_USUARIO_PERFIL = ID_PERFIL_USUARIO;
                        Usuario = repositorio.Usuario_Sistema(Usuario, ref auditoria);

                        if (!auditoria.EJECUCION_PROCEDIMIENTO)
                        {
                            Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                        }
                        else
                        {
                            if (!auditoria.RECHAZAR)
                            {
                                if (Usuario.ID_USUARIO != 0)
                                {
                                    Valido = true;
                                    ViewBag.NameUser = Usuario.NOMBRES_APE;
                                    ViewBag.Desc_Sucursal = Usuario.Perfil_Sucursal.DESC_SUCURSAL;
                                    ViewBag.IdSucursal = Usuario.Perfil_Sucursal.ID_SUCURSAL;
                                    ViewBag.CodUsuario = Usuario.COD_USUARIO;
                                    ViewBag.IdPf = Usuario.Perfil_Sucursal.ID_PERFIL;
                                    ViewBag.ABREV_USUARIO = Usuario.ABREV_USUARIO;
                                    int RECUERDAME = Capa_Token.Cls_Api_Token.Claim_RECUERDAME(Token);
                                     Token = Capa_Token.Cls_Api_Token.Generar(Usuario.ID_USUARIO.ToString(), Usuario.COD_USUARIO,
                                                                      RECUERDAME.ToString(), Usuario.Perfil_Sucursal.ID_SUCURSAL.ToString());
                                     this.getCookie(Token);
                                     Session["SetUpLogin"] = Capa_Token.Cls_Api_Token.SetUpLogin(Token); 
                                    Cls_Ent_configurarEmpresa Empresa = new Cls_Ent_configurarEmpresa();
                                    using (ConfigurarEmpresaRepositorio Repositorio = new ConfigurarEmpresaRepositorio())
                                    {
                                        Empresa = Repositorio.configurarEmpresa_Listar(ref auditoria);
                                        if (!auditoria.EJECUCION_PROCEDIMIENTO)
                                        {
                                            string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                                            auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                                        }
                                        else {
                                            if (!auditoria.RECHAZAR)
                                            {
                                                ViewBag.SimboloMoneda = Empresa.SIMBOLO_MONEDA;
                                                ViewBag.Impuesto = Empresa.IMPUESTO;
                                                ViewBag.NombreImpuesto = Empresa.NOMBRE_IMPUESTO;
                                                ViewBag.RazonSocial = Empresa.RAZON_SOCIAL;
                                                ViewBag.Isotipo = Empresa.CODIGO_ARCHIVO_ISOTIPO + Empresa.EXTENSION_ARCHIVO_ISOTIPO;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    return RedirectToAction("Index", "Login");
                                }
                            }
                            else
                            {
                                return RedirectToAction("Index", "Login");
                            }
                        }
                    }
                }
                else
                {
                    return RedirectToAction("page401", "Home");
                }

                if (Valido)
                {
                    return View();
                }
                else {
                    return RedirectToAction("page401", "Home");
                }
              
            }
            catch (Exception ex)
            {
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                return RedirectToAction("page401", "Home");
            }
        }

        public ActionResult Logout()
        {
            var cook = HttpContext.Request.Cookies["IP-CyberToken"];
            string Token = cook.Value;
            int RECUERDAME = Cls_Api_Token.Claim_RECUERDAME(Token);
            if (RECUERDAME == 0)
            {
                HttpCookie myCookie = new HttpCookie("IP-CyberToken");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                myCookie.Value = string.Empty;
                Response.Cookies.Add(myCookie);
            }
            Session.Abandon();
            return RedirectToAction("Index", "Login", new { area = "" });
        }

        public ActionResult page401()
        {
            return View();
        }

        public void getCookie(string value) {
            HttpCookie cookie1 = new HttpCookie("IP-CyberToken", value);
            ControllerContext.HttpContext.Response.SetCookie(cookie1); 
        }
    }
}
