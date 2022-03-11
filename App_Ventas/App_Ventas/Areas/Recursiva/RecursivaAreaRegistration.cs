using System.Web.Mvc;

namespace App_Ventas.Areas.Recursiva
{
    public class RecursivaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Recursiva";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Recursiva_default",
                "Recursiva/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
