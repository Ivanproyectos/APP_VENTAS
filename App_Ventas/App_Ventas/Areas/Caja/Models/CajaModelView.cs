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
        public int ID_TIPO_MOVIMIENTO { get; set; }
     
        [DataType(DataType.Text)]
        public string ID_USUARIO { get; set; }
        public List<SelectListItem> Lista_Usuario { get; set; }


        [DataType(DataType.Text)]
        public int ID_SUCURSAL_SEARCH { get; set; }

        [Display(Name = "Sucursal: ")]
        [DataType(DataType.Text)]
        public int ID_SUCURSAL { get; set; }
        public List<SelectListItem> Lista_Sucursal { get; set; }


        [Display(Name = "Tipo: ")]
        [DataType(DataType.Text)]
        public bool FLG_TIPO { get; set; }

        [Display(Name = "Descripción: ")]
        [DataType(DataType.Text)]
        public string DESC_MOVIMIENTO { get; set; }


        [Display(Name = "Monto: ")]
        [DataType(DataType.Text)]
        public decimal MONTO { get; set; }

        public string Accion { get; set; }
        
    }
}