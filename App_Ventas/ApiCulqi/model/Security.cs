using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
namespace ApiCulqi
{
	public class Security
	{
		public Security()
		{
            public_key = ConfigurationManager.AppSettings["Pk_key"].ToString();
            secret_key = ConfigurationManager.AppSettings["SK_Key"].ToString();
		}
		public string public_key { get; set; }
		public string secret_key { get; set; }

	}
}
