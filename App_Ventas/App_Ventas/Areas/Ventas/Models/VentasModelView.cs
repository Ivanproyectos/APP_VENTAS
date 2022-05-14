using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Capa_Entidad.Administracion; 

namespace App_Ventas.Areas.Ventas.Models
{
    public class VentasModelView
    {
        public VentasModelView()
        {
            Cliente = new Cls_Ent_Cliente(); 
        }
        public long ID_VENTA { get; set; }
        [Display(Name = "Código Venta: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Código Venta] es obligatorio")]
        public string COD_VENTA { get; set; }

        [Display(Name = "Código Comprobante: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Código Comprobante] es obligatorio")]
        public string COD_COMPROBANTE { get; set; }

        [Display(Name = "Tipo Pago: ")]
        [DataType(DataType.Text)]
        public int ID_TIPO_PAGO { get; set; }
        [Required(ErrorMessage = "[Tipo Pago] es obligatorio")]
        public List<SelectListItem> Lista_Tipo_Pago{ get; set; }

        [Display(Name = "Cliente: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Cliente] es obligatorio")]
        public int ID_CLIENTE { get; set; }
        public List<SelectListItem> Lista_Cliente { get; set; }

        [Display(Name = "Comprobante: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Comprobante] es obligatorio")]
        public string ID_TIPO_COMPROBANTE { get; set; }
        public List<SelectListItem> Lista_Tipo_Comprobante { get; set; }

        [Display(Name = "Descuento: ")]
        [DataType(DataType.Text)]
        public decimal DESCUENTO { get; set; }

        [Display(Name = "Igv: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Igv] es obligatorio")]
        public decimal IGV { get; set; }

        [Display(Name = "Sub. Total: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Sub. Total] es obligatorio")]
        public decimal SUBTOTAL { get; set; }

        [Display(Name = "Total: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Total] es obligatorio")]
        public decimal TOTAL { get; set; }

        [Display(Name = "Detalle: ")]
        [DataType(DataType.Text)]
        public string DETALLE_VENTA { get; set; }

        [Display(Name = "Total Recibido: ")]
        [DataType(DataType.Text)]
        public decimal TOTAL_RECIBIDO { get; set; }

        [Display(Name = "Vuelto: ")]
        [DataType(DataType.Text)]
        public decimal VUELTO { get; set; }

        [Display(Name = "Adelanto: ")]
        [DataType(DataType.Text)]
        public decimal ADELANTO { get; set; }

        [Display(Name = "Fecha venta: ")]
        [DataType(DataType.Text)]
        public string FECHA_VENTA { get; set; }

        [Display(Name = "Nro. Operación: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Nro. Operación] es obligatorio")]
        public string NRO_OPERACION { get; set; }

        public string Accion { get; set; }

        public string ID_TIPO_COMPROBANTE_SEARCH { get; set; }
        public int ID_TIPO_PAGO_SEARCH { get; set; }
        
        /*CONSULTAS*/
        [Display(Name = "Usuario: ")]
        [DataType(DataType.Text)]
        public string ID_USUARIO { get; set; }
        public List<SelectListItem> Lista_Usuarios{ get; set; }

        public int ID_SUCURSAL { get; set; }
        public List<SelectListItem> Lista_Sucursal { get; set; }

        public decimal DEBE { get; set; }
        public string TIPO_GRILLA { get; set; }

        public string FECHA_INICIO { get; set; }
        public string FECHA_FIN{ get; set; }

        public Cls_Ent_Cliente Cliente { get; set; }
        public int FLG_ANULADO { get; set; }
        public int TIPO_EXPORT { get; set; }
    

    }
}