using System.Web.Mvc;

namespace App_Ventas.Areas.Servicios
{
    public class ServiciosAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Servicios";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Servicios_default",
                "Servicios/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
