using System;
namespace ApiCulqi
{
	public class Config
	{
		public Config()
		{
            url_api_base = "https://api.culqi.com/v2";
		}

		public string url_api_base { get; set;} 

	}
}
