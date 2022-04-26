using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Ventas.Recursos.Paginacion
{
    public class JQgrid
    {
        public int recordsFiltered { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int start { get; set; }
        public object data { get; set; }
    }
}