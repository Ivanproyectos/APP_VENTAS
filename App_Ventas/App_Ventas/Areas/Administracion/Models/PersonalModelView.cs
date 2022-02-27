using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Ventas.Areas.Administracion.Models
{
    public class PersonalModelView
    {
        [Display(Name = "Documento: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Documento] es obligatorio")]
        public string NRO_DOCUMENTO { get; set; }

    }
}