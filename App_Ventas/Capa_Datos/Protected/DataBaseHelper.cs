using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;

using System.Data.SqlClient;
//using System.Data.SqlClient;
//using System.Data.SqlClient;

namespace Capa_Datos.Protected
{
    public class DataBaseHelper
    {
        string _cnSTR = string.Empty;
        string _cnSTR_Correo = string.Empty;

        public DataBaseHelper()
        {
            _cnSTR = ConfigurationManager.ConnectionStrings["Sis_Ventas"].ConnectionString;
            //_cnSTR_Correo = ConfigurationManager.ConnectionStrings["bdGeneral"].ConnectionString;
        }

        public String cnSTR
        {
            get { return _cnSTR; }
        }

        //public String cnSTR_Correo
        //{
        //    get { return _cnSTR_Correo; }
        //}

        public SqlConnection GetNewConnection()
        {
            SqlConnection cn = new SqlConnection(_cnSTR);
            cn.Open();
            return cn;
        }

        //public SqlConnection GetNewConnectionCorreo()
        //{
        //    SqlConnection cn = new SqlConnection(_cnSTR_Correo);
        //    cn.Open();
        //    return cn;
        //}

        public SqlConnection GetBeginTransaction()
        {
            SqlConnection cn = new SqlConnection(_cnSTR);
            cn.Open();
            return cn;
        }

    }
}
