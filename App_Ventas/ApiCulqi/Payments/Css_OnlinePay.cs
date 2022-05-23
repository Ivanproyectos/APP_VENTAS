using System;
using System.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCulqi;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace ApiCulqi.Payments
{
    public class Css_OnlinePay
    {
        Security security  = new Security();

		protected static string GetRandomString()
		{
			string path = Path.GetRandomFileName();
			path = path.Replace(".", "");
			return path;
		}
		protected string CreateToken()
		{	
			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"card_number", "4111111111111111"},
				{"cvv", "123"},
				{"expiration_month", 9},
				{"expiration_year", 2025},
				{"email", "ivansperezt@gmail.com"}
			};
			return new Token(security).Create(map);
		}
		public void ValidCreateToken()
		{
			string data = CreateToken();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("token",(string)json_object["object"]);
		}
        public string OnlinePay_CreateCharge()
		{	
			string data = CreateToken();
			var json_object = JObject.Parse(data);
			Dictionary<string, object> metadata = new Dictionary<string, object>
			{
				{"order_id", "777"}
			};
			Dictionary<string, object> map = new Dictionary<string, object>
			{	
				{"amount", 1000},
				{"capture", true},
				{"currency_code", "PEN"},
				{"description", "Venta de prueba"},
				{"email", "ivansperezt@gmail.com"},
				{"installments", 0},
				{"metadata", metadata},
				{"source_id", (string)json_object["id"]}
			};
			return new Charge(security).Create(map);

		}

    }
}
