using Newtonsoft.Json;

namespace PizzaIllico.Mobile.Dtos.Authentications
{
	public class RefreshRequest
	{
		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }
		
		[JsonProperty("client_id")]
		public string ClientId { get; set; }
		
		[JsonProperty("client_secret")]
		public string ClientSecret { get; set; }
	}
}