using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Ventas.Areas.Compras.Models
{
    public class ComprasModelView
    {


        public long ID_COMPRA { get; set; }

        [Display(Name = "Códgo Documento: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Códgo Documento] es obligatorio")]
        public string COD_DOCUMENTO { get; set; }

        [Display(Name = "Fecha Documento: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Fecha Documento] es obligatorio")]
        public string FCHA_DOCUMENTO { get; set; }


        [Display(Name = "Sucursal: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Sucursal] es obligatorio")]
        public int ID_SUCURSAL { get; set; }
        public List<SelectListItem> Lista_Sucursal { get; set; }

        [Display(Name = "Comprobante: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Comprobante] es obligatorio")]
        public int ID_TIPO_COMPROBANTE { get; set; }
        public List<SelectListItem> Lista_Tipo_Comprobante { get; set; }

        [Display(Name = "Proveedor: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Proveedor] es obligatorio")]
        public int ID_PROVEEDOR { get; set; }
        public List<SelectListItem> Lista_Proveedor { get; set; }


        [Display(Name = "Descuento: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Descuento] es obligatorio")]
        public decimal DESCUENTO { get; set; }


        [Display(Name = "Igv: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Igv] es obligatorio")]
        public decimal IGV { get; set; }

        [Display(Name = "Total: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Total] es obligatorio")]
        public decimal TOTAL { get; set; }

        [Display(Name = "Detalle: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Detalle] es obligatorio")]
        public string DETALLE { get; set; }



        public string Accion { get; set; }


      


    }
}