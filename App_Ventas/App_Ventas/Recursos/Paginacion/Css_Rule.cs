using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Ventas.Recursos.Paginacion
{
    public class Css_Rule
    {
        private string m_op2;
        public string op2
        {
            get { return m_op2; }
            set { m_op2 = value; }
        }

        public string field
        {
            get { return m_field; }
            set { m_field = value; }
        }

        private string m_field;
        public string data
        {
            get { return m_data; }
            set { m_data = value; }
        }

        private string m_data;
        public string op
        {
            get { return m_op; }
            set { m_op = value; }
        }

        private string m_op;
    }
}