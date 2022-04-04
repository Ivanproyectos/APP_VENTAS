using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Ventas.Areas.CuentasCobrar.Models
{
    public class CuentasCobrarModelView
    {
        public long ID_VENTA { get; set; }

     
        [Display(Name = "Sucursal: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Sucursal] es obligatorio")]
        public int ID_SUCURSAL { get; set; }
        public List<SelectListItem> Lista_Sucursal { get; set; }

          [Display(Name = "ID_CLIENTE: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Sucursal] es obligatorio")]
        public int ID_CLIENTE { get; set; }
          public List<SelectListItem> Lista_Cliente { get; set; }

        

        public string Accion { get; set; }



    }
}