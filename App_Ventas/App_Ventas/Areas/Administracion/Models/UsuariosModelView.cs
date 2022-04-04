using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Ventas.Areas.Administracion.Models
{
    public class UsuariosModelView
    {
        public int ID_USUARIO { get; set; }

        [Display(Name = "Número Documento: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Documento] es obligatorio")]
        public string DNI { get; set; }

        [Display(Name = "Nombre: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Nombre] es obligatorio")]
        public string NOMBRE { get; set; }

        [Display(Name = "Apellido Paterno: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Apellido Paterno] es obligatorio")]
        public string APE_PATERNO { get; set; }

        [Display(Name = "Apellido Materno: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Apellido Materno] es obligatorio")]
        public string APE_MATERNO { get; set; }


        [Display(Name = "Tipo Documento: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Tipo Documento] es obligatorio")]
        public int ID_TIPO_DOCUMENTO { get; set; }
        public List<SelectListItem> Lista_Tipo_Documento { get; set; }



        [Display(Name = "Celular: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Apellido Materno] es obligatorio")]
        public string CELULAR { get; set; }

        [Display(Name = "Telefono: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Telefono] es obligatorio")]
        public string TELEFONO { get; set; }

        [Display(Name = "Correo: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Correo] es obligatorio")]
        public string CORREO { get; set; }

        [Display(Name = "¿Aministrador Super?: ")]
        [DataType(DataType.Text)]
        public bool FLG_ADMIN { get; set; }

        [Display(Name = "Usuario: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Usuario] es obligatorio")]
        public string COD_USUARIO { get; set; }

        [Display(Name = "Contraseña: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Contraseña] es obligatorio")]
        public string CLAVE_USUARIO { get; set; }



        [Display(Name = "Sucursal: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Sucursal] es obligatorio")]
        public int ID_SUCURSAL { get; set; }
        public List<SelectListItem> Lista_Sucursal{ get; set; }

        [Display(Name = "Perfil: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Perfil] es obligatorio")]
        public int ID_PERFIL { get; set; }
        public List<SelectListItem> Lista_Perfil { get; set; }

        [Display(Name = "Fecha Activación: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Fecha Activación] es obligatorio")]
        public string FEC_ACTIVACION { get; set; }


        [Display(Name = "Fecha Desactivación: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Fecha Desactivación] es obligatorio")]
        public string FEC_DESACTIVACION { get; set; }  



        public string Accion { get; set; }
        
    }
}