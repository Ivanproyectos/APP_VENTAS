using System.Web.Mvc;

namespace App_Ventas.Areas.Ventas
{
    public class VentasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Ventas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Ventas_default",
                "Ventas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
