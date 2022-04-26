using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Ventas.Recursos.Paginacion
{
    public class GridTable
    {
        public int draw { get; set; }
        public int rows { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public string _search { get; set; }
        public int start { get; set; }
        public string searchString { get; set; }
        public List<Css_Rule> rules { get; set; }
        public List<Css_Rule> SearchFields { get; set; }
        public string parameters { get; set; }


    }
}