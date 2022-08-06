using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Capa_Entidad.Administracion; 

namespace App_Ventas.Areas.Administracion.Models
{
    public class NotificacioModelView
    {
        public string MENSAJE { get; set; }
        public string HORA { get; set; }
        public string FECHA_REGISTRO { get; set; }
        public string IMAGE { get; set; }
        public string COLOR { get; set; }
        public List<Cls_Ent_Notificacion> Lista { get; set; }

    }
}