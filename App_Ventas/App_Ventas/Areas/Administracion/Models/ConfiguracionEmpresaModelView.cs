using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Ventas.Areas.Administracion.Models
{
    public class ConfiguracionEmpresaModelView
    {
        public int ID_CONFIGURACION { get; set; }

        [Display(Name = "Ruc: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Ruc] es obligatorio")]
        public string RUC { get; set; }

        [Display(Name = "Razón Social: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Razón Social] es obligatorio")]
        public string RAZON_SOCIAL { get; set; }

        [Display(Name = "Nombre Comercial: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Nombre Comercial] es obligatorio")]
        public string NOMBRE_COMERCIAL { get; set; }


        [Display(Name = "Urbanización: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Urbanización] es obligatorio")]
        public string URBANIZACION { get; set; }

        [Display(Name = "Dirección: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Dirección] es obligatorio")]
        public string DIRECCION_FISCAL { get; set; }

        [Display(Name = "Telefono: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Telefono] es obligatorio")]
        public string TELEFONO { get; set; }



        [Display(Name = "Nombre Impuesto: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Nombre Impuesto] es obligatorio")]
        public string NOMBRE_IMPUESTO { get; set; }


        [Display(Name = "Impuesto: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Impuesto] es obligatorio")]
        public decimal IMPUESTO { get; set; }



        [Display(Name = "¿Imprimir despues de cada venta?: ")]
        [DataType(DataType.Text)]
        public bool FLG_IMPRIMIR { get; set; }


        [Display(Name = "¿Mostrar Impuestos?: ")]
        [DataType(DataType.Text)]
        public bool FLG_IMPUESTO { get; set; }

        [Display(Name = "Correo: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Correo] es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo invalido")]
        public string CORREO { get; set; }


        [Display(Name = "Ubigeo: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Ubigeo] es obligatorio")]
        public string COD_UBIGEO { get; set; }
        public List<SelectListItem> Lista_Ubigeo { get; set; }


    }
}