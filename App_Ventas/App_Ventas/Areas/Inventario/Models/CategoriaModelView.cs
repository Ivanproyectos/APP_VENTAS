using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Ventas.Areas.Inventario.Models
{
    public class CategoriaModelView
    {
       public int  ID_CATEGORIA { get; set; }
   

        [Display(Name = "Categoria: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Categoria] es obligatorio")]
        public string DESC_CATEGORIA { get; set; }

        [Display(Name = "Descripción: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Descripción] es obligatorio")]
        public string DESCRIPCION { get; set; }

        public string Accion { get; set; }
    }
}