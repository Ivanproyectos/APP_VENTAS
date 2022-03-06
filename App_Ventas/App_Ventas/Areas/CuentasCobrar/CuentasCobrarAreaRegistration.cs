using System.Web.Mvc;

namespace App_Ventas.Areas.CuentasCobrar
{
    public class CuentasCobrarAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CuentasCobrar";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CuentasCobrar_default",
                "CuentasCobrar/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
