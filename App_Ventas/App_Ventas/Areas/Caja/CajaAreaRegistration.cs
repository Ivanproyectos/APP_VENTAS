﻿using System.Web.Mvc;

namespace App_Ventas.Areas.Caja
{
    public class CajaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Caja";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Caja_default",
                "Caja/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
