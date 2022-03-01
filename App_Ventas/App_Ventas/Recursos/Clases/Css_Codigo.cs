using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Ventas.Recursos.Clases
{
    public class Css_Codigo
    {
        public static string Generar_Codigo_Temporal()
        {
            string CODIGO_TEMPORAL = Guid.NewGuid().ToString();/* string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                          new Random(700).Next().ToString() + new Random(100).Next().ToString(),
                          DateTime.Now.Day.ToString().PadLeft(2, '0'),
                          DateTime.Now.Month.ToString().PadLeft(2, '0'),
                          DateTime.Now.Year,
                          DateTime.Now.Hour.ToString().PadLeft(2, '0'),
                          DateTime.Now.Minute.ToString().PadLeft(2, '0'),
                          DateTime.Now.Second.ToString().PadLeft(2, '0'),
                          DateTime.Now.Millisecond.ToString().PadLeft(2, '0'),
                          new Random().Next().ToString(),
                          new Random().Next().ToString(),
                          "A");*/
            return CODIGO_TEMPORAL;
        }
    }
}