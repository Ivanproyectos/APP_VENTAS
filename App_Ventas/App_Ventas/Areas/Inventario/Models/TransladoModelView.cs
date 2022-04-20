using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Capa_Entidad.Base; 

namespace App_Ventas.Areas.Inventario.Models
{
    public class TransladoModelView
    {
       
        public long ID_PRODUCTO { get; set; }
   
        [Display(Name = "Buscar Producto: ")]
        [DataType(DataType.Text)]
        public string SEARCH_PRODUCTO { get; set; }

        

        [Display(Name = "Código: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Código] es obligatorio")]
        public string COD_PRODUCTO { get; set; }

        [Display(Name = "Producto: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Producto] es obligatorio")]
        public string DESC_PRODUCTO { get; set; }

        [Display(Name = "Almacén Origen: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Almacen] es obligatorio")]
        public int ID_SUCURSAL_ORIGEN { get; set; }
        public List<SelectListItem> Lista_Sucursal { get; set; }


        [Display(Name = "Almacén Destino: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Almacen] es obligatorio")]
        public int ID_SUCURSAL_DESTINO { get; set; }

        [Display(Name = "Cantidad: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Cantidad] es obligatorio")]
        public string CANTIDAD { get; set; }

        [Display(Name = "Detalle: ")]
        [DataType(DataType.Text)]
        public string DETALLE { get; set; }

    }
}