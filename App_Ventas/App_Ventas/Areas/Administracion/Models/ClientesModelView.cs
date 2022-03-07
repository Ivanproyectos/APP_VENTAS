using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Ventas.Areas.Administracion.Models
{
    public class ClientesModelView
    {
        public int ID_CLIENTE { get; set; }
        public int ID_PROVEEDOR { get; set; }

        [Display(Name = "Razón social/Nombre Completo: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Razón social/Nombre Completo] es obligatorio")]
        public string NOMBRES_APE { get; set; }


        [Display(Name = "Tipo Documento: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Tipo Documento] es obligatorio")]
        public int ID_TIPO_DOCUMENTO { get; set; }
        public List<SelectListItem> Lista_Tipo_Documento{ get; set; }

        [Display(Name = "Número Documento: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Documento] es obligatorio")]
        public string NUMERO_DOCUMENTO { get; set; }

        [Display(Name = "Correo: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Documento] es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo invalido")]
        public string CORREO { get; set; }


        [Display(Name = "Dirección: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Dirección] es obligatorio")]
        public string DIRECCION { get; set; }

        [Display(Name = "Telefono: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Documento] es obligatorio")]
        public string TELEFONO { get; set; }


        [Display(Name = "Celular: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Celular] es obligatorio")]
        public string CELULAR { get; set; }

        [Display(Name = "Detalle: ")]
        [DataType(DataType.Text)]
        public string DETALLE { get; set; }
        

        [Display(Name = "Ubigeo: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Ubigeo] es obligatorio")]
        public string COD_UBIGEO { get; set; }
        public List<SelectListItem> Lista_Ubigeo { get; set; }

        public string Accion { get; set; }

    }
}