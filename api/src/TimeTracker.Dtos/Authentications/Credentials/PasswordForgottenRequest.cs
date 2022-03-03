using Newtonsoft.Json;

namespace TimeTracker.Dtos.Authentications.Credentials
{
	public class PasswordForgottenRequest
	{
		[JsonProperty("email")]
		public string Email { get; set; }
	}
}