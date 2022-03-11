﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Capa_Entidad.Base; 


namespace App_Ventas.Areas.Ventas.Models
{
    public class ProductoModelView
    {
        public long ID_PRODUCTO { get; set; }


        [Display(Name = "Código: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Código] es obligatorio")]
        public string COD_PRODUCTO { get; set; }

        [Display(Name = "Producto: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Producto] es obligatorio")]
        public string DESC_PRODUCTO { get; set; }

        [Display(Name = "Sucursal: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Sucursal] es obligatorio")]
        public int ID_SUCURSAL { get; set; }
        public List<SelectListItem> Lista_Sucursal { get; set; }


        [Display(Name = "Unidad Medida: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Unidad Medida] es obligatorio")]
        public int ID_UNIDAD_MEDIDA { get; set; }
        public List<SelectListItem> Lista_Unidad_Medida { get; set; }

        [Display(Name = "Categoria: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Categoria] es obligatorio")]
        public int ID_CATEGORIA { get; set; }
        public List<SelectListItem> Lista_Categoria { get; set; }


        [Display(Name = "Precio Compra: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Precio Compra] es obligatorio")]
        public string PRECIO_COMPRA { get; set; }

        [Display(Name = "Precio Venta: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Precio Venta] es obligatorio")]
        public string PRECIO_VENTA { get; set; }

        [Display(Name = "Stock: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Stock] es obligatorio")]
        public int STOCK { get; set; }


        [Display(Name = "Stock: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Stock] es obligatorio")]
        public int STOCK_MINIMO { get; set; }


        [Display(Name = "¿Producto Vence?: ")]
        [DataType(DataType.Text)]
        public bool FLG_VENCE { get; set; }

        public bool FLG_SERIVICIO { get; set; }


        [Display(Name = "Fecha Vencimiento: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Fecha Vencimiento] es obligatorio")]
        public string FECHA_VENCIMIENTO { get; set; }


        [Display(Name = "Marca: ")]
        [DataType(DataType.Text)]
        public string MARCA { get; set; }

        [Display(Name = "Modelo: ")]
        [DataType(DataType.Text)]
        public string MODELO { get; set; }


        [Display(Name = "Código: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Código] es obligatorio")]
        public string COD_PRODUCTO_SERVICIO { get; set; }

        [Display(Name = "Servicio: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Servicio] es obligatorio")]
        public string DESC_SERVICIO { get; set; }

        [Display(Name = "Precio Venta: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Precio Venta] es obligatorio")]
        public string PRECIO_VENTA_SERVICIO { get; set; }

        [Display(Name = "Unidad Medida: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Unidad Medida] es obligatorio")]
        public int ID_UNIDAD_MEDIDA_SERVICIO { get; set; }

        [Display(Name = "Detalle: ")]
        [DataType(DataType.Text)]
        public string DETALLE { get; set; }



        [Display(Name = "Total: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Total] es obligatorio")]
        public string TOTAL { get; set; }

          [Display(Name = "Buscar Producto: ")]
        public string SEARCH_PRODUCTO { get; set; }

        public string GrillaCarga { get; set; }


        [Display(Name = "Cantidad: ")]
        [DataType(DataType.Text)]
        public int CANTIDAD { get; set; }

        public Cls_Ent_Archivo MiArchivo { get; set; }

    }
}