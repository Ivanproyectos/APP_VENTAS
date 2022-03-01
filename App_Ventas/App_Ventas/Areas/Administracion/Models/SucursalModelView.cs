using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Ventas.Areas.Administracion.Models
{
    public class SucursalModelView
    {

        public string ID_SUCURSAL { get; set; }

        [Display(Name = "Sucursal: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Documento] es obligatorio")]
        public string DESC_SUCURSAL { get; set; }

     
        [Display(Name = "Dirección: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Documento] es obligatorio")]
        public string DIRECCION { get; set; }

        [Display(Name = "Telefono: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Documento] es obligatorio")]
        public int TELEFONO { get; set; }

        [Display(Name = "Documento: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Documento] es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo invalido")]
        public string CORREO { get; set; }


        [Display(Name = "Urbanización: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Documento] es obligatorio")]
        public string URBANIZACION { get; set; }


        [Display(Name = "Ubigeo: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Ubigeo] es obligatorio")]
        public string COD_UBIGEO { get; set; }
        public List<SelectListItem> Lista_Ubigeo { get; set; }

        public string Accion { get; set; }


    }
}