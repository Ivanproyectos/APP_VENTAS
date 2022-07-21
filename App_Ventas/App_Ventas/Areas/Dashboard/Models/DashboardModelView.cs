using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Ventas.Areas.Caja.Models
{
    public class DashboardModelView
    {
  
     
        [DataType(DataType.Text)]
        public int ID_SUCURSAL { get; set; }
        public List<SelectListItem> Lista_Sucursal { get; set; }


        [DataType(DataType.Text)]
        public int ID_ANIO { get; set; }
        public List<SelectListItem> Lista_Anio { get; set; }

       [DataType(DataType.Text)]
        public int ID_USUARIO_INDEX3 { get; set; }
       public List<SelectListItem> Lista_Usuario { get; set; }

       public int ID_USUARIO_INDEX4 { get; set; }
      
        public string Accion { get; set; }
        
    }
}