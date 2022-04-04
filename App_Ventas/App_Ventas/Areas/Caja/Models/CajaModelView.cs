using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Ventas.Areas.Caja.Models
{
    public class CajaModelView
    {

     
        [DataType(DataType.Text)]
        public string ID_USUARIO { get; set; }
        public List<SelectListItem> Lista_Usuario { get; set; }


        
        [DataType(DataType.Text)]
        public string ID_SUCURSAL { get; set; }
        public List<SelectListItem> Lista_Sucursal { get; set; }

    }
}