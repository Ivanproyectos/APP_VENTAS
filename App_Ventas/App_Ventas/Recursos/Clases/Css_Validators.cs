using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace App_Ventas.Recursos.Clases
{
    public class Css_Validators
    {

        public static bool ValidarNumber(string Number)
        {
            bool valido = false; 
            if (Number.All(char.IsDigit))
            {
                valido = true; 
            }
            return valido; 
        }

        public static bool ValidarDecimal(string Number)
        {
            bool valido = false;
            decimal Decimal = 0;
            bool validDecimal = decimal.TryParse(Number, out Decimal);

            int Entero = 0;
            bool ValidEntero = Int32.TryParse(Number, out Entero); //i now = 108  

            if (validDecimal || validDecimal) {
                valido = true; 
            }

            return valido; 
        }


        public static bool ValidarFecha(string Fecha)
        {
            var culture = CultureInfo.CreateSpecificCulture("es-PE");
            var styles = DateTimeStyles.None;
            DateTime dt1 = DateTime.Now;
            bool fechaValida = DateTime.TryParse(Fecha, culture, styles, out dt1); 
            return fechaValida;
        }


    
    }
}