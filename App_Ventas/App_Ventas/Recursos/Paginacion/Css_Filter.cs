using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Ventas.Recursos.Paginacion
{
    public class Css_Filter
    {
        //public class Filter

        public string groupOp
        {
            get { return m_groupOp; }
            set { m_groupOp = value; }
        }

        private string m_groupOp;
        public List<Css_Rule> rules
        {
            get { return m_rules; }
            set { m_rules = value; }
        }
        private List<Css_Rule> m_rules;


    }
}