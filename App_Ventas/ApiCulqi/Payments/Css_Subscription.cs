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
   public  class Css_Subscription
    {
         Security security = new Security();

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
        protected static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }
		public void ValidCreateToken()
		{
			string data = CreateToken();
			var json_object = JObject.Parse(data);
			Assert.AreEqual("token",(string)json_object["object"]);
		}

        public string OnlinePay_CreateSubscription()
		{	
			string data = CreateToken();
            var json_token = JObject.Parse(data);
            
            Dictionary<string, object> metadata = new Dictionary<string, object>
            {
	            {"alias", "plan-test"}
            };

            // crear plan
            Dictionary<string, object> plan = new Dictionary<string, object>
            {
	            {"amount", 1000},
	            {"currency_code", "PEN"},
	            {"interval", "meses"},
	            {"interval_count", 1},
	            {"limit", 12},
	            {"metadata", metadata},
	            {"name", "plan-culqi-"+GetRandomString()},
                //{"trial_days", 1}
            };
           string plan_created = new Plan(security).Create(plan);

            // crear cliente 
           Dictionary<string, object> customer = new Dictionary<string, object>
            {
	            {"address", "Av Lima 123"},
	            {"address_city", "Lima"},
	            {"country_code", "PE"},
	            {"email", "test"+GetRandomString()+"@culqi.com"},
	            {"first_name", "Test"},
	            {"last_name", "Culqi"},
	            {"phone_number", 99004356}
            };
           string customer_created = new Customer(security).Create(customer);

          // crear tarjeta 
           var json_customer = JObject.Parse(customer_created);
           Dictionary<string, object> card = new Dictionary<string, object>
            {
	            {"customer_id", (string)json_customer["id"]},
	            {"token_id", (string)json_token["id"]}
            };
           string card_created = new Card(security).Create(card);

            // crear subcripcion
           var json_plan = JObject.Parse(plan_created);
           var json_card = JObject.Parse(card_created);
           Dictionary<string, object> subscription = new Dictionary<string, object>
            {
	            {"card_id", (string)json_card["id"]},
	            {"plan_id", (string)json_plan["id"]}
            };
            return  new Subscription(security).Create(subscription);

		}

    }
}
