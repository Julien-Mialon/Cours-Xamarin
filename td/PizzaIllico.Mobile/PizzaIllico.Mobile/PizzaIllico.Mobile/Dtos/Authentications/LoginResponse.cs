using Newtonsoft.Json;

namespace PizzaIllico.Mobile.Dtos.Authentications
{
	public class LoginResponse
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
		
		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }
		
		[JsonProperty("token_type")]
		public string TokenType { get; set; }
		
		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }
	}
}