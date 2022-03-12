using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_Entidad.Login;
using Capa_Entidad;
using Capa_Negocio.Login; 

namespace App_Ventas.Areas.Login.Repositorio
{
    public class ModulosRepositorio : IDisposable
    {

        private Cls_Rule_Login _rule = new Cls_Rule_Login();

        public List<Cls_Ent_Modulo> Usuario_Sistema_Modulo(long ID_PERFIL, ref Cls_Ent_Auditoria auditoria)
        {
            List<Cls_Ent_Modulo> lista = new List<Cls_Ent_Modulo>();
            try
            {
                lista = _rule.Usuario_Sistema_Modulo(ID_PERFIL, ref auditoria);
                return Menu_Ordenar(lista);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Modulo>();
                return lista;
            }

        }

        private List<Cls_Ent_Modulo> Menu_Ordenar(List<Cls_Ent_Modulo> MenuHijos)
        {
            List<Cls_Ent_Modulo> Menu_Retorno = new List<Cls_Ent_Modulo>();
            List<Cls_Ent_Modulo> MenuHijos_Base = MenuHijos.Where(item => item.ID_MODULO_PADRE == 0).ToList();

            foreach (Cls_Ent_Modulo item in MenuHijos_Base)
            {
                Menu_Retorno.Add(item);
                Menu_Ordenar_Hijos(MenuHijos, item);
            }
            MenuHijos_Base.Clear();
            MenuHijos.Clear();
            return Menu_Retorno;
        }

        private void Menu_Ordenar_Hijos(List<Cls_Ent_Modulo> MenuHijos, Cls_Ent_Modulo MenuPadre)
        {
            MenuPadre.Modulos_Hijos = new List<Cls_Ent_Modulo>();

            foreach (Cls_Ent_Modulo MenuHijos_ in MenuHijos)
            {
                if (MenuHijos_.ID_MODULO_PADRE == MenuPadre.ID_MODULO)
                {
                    MenuPadre.Modulos_Hijos.Add(MenuHijos_);
                }
            }
        }
        int index = 0;

        public void Generar_Vista(List<Cls_Ent_Modulo> Menu_Lista, ref string menu, int nivel)
        {
            foreach (Cls_Ent_Modulo _menu in Menu_Lista)
            {

                index++;
                if (_menu.NIVEL == 1)
                {
                    if (_menu.FLG_SINGRUPO == 1)
                    {
                        menu += "<li><a class=\"has-arrow not-before\" name=\"" + _menu.URL_MODULO  + "\" onclick=\"_LayoutInvocar(this)\" id=\"" +  _menu.ID_A +"\" aria-expanded=\"false\">";
                        menu += "<i class=\"" + _menu.IMAGEN + "\"></i><span class=\"nav-text\">" + _menu.DESC_MODULO  +"</span></a>";
                        menu += "<ul aria-expanded=\"false\">";
                    }
                    else {
                        menu += " <li><a class=\"has-arrow\" href=\"javascript:void()\" aria-expanded=\"false\">";
                        menu += "<i class=\"" + _menu.IMAGEN + "\"></i><span class=\"nav-text\">" + _menu.DESC_MODULO + "</span></a>";
                        menu += "<ul aria-expanded=\"false\">";
                    }

                }
                else
                {
                    menu += "<li></li>";
                    if (_menu.FLG_LINK == 1)
                        menu += "<a  target=\"_blank\" href=\"" + _menu.URL_MODULO + "\" id=\"" + _menu.ID_A + "\" tabindex=\"" + index + "\" style=\"padding: 5px\">" + _menu.DESC_MODULO + "</a>";
                    else
                        menu += "<a  name=\"" + _menu.URL_MODULO + "\" onclick =\"_LayoutInvocar(this)\" id=\"" + _menu.ID_A + "\" tabindex=\"" + index + "\" >" + _menu.DESC_MODULO + "</a>";
                    menu += "</li>";
                }

                if (_menu.NIVEL == 1)
                {
                    if (_menu.Modulos_Hijos != null)
                        if (_menu.Modulos_Hijos.Count > 0)
                        {
                            Generar_Vista(_menu.Modulos_Hijos, ref menu, 2);
                        }

                    menu += "</ul>";
                    menu += "</li>" + Convert.ToChar(13);
                }
            }
            menu += "</ul>" + Convert.ToChar(13);


        }














        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}