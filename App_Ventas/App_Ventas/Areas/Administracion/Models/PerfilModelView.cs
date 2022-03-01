using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace App_Ventas.Areas.Administracion.Models
{
    public class PerfilModelView
    {
        public int ID_PERFIL { get; set; }

        [Display(Name = "Perfil: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Perfil] es obligatorio")]
        public string DESC_PERFIL { get; set; }

        public string Accion { get; set; }

    }
}