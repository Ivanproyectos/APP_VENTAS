using System.Web.Mvc;

namespace App_Ventas.Areas.Inventario
{
    public class InventarioAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Inventario";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Inventario_default",
                "Inventario/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
