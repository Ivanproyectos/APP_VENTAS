using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Ventas.Recursos.Paginacion
{
    public class JQgrid
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public int start { get; set; }
        public Css_Row[] rows { get; set; }
    }
}